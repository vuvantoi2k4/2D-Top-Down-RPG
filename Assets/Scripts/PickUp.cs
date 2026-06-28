using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        } else {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * moveDir * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            DetectPickUpType();
            Destroy(gameObject);
        }
    }

    private void DetectPickUpType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                Debug.Log("GoldCoin");
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("HealthGlobe");
                break;
            case PickUpType.StaminaGlobe:
                Debug.Log("StaminaGlobe");
                break;
        }
    }
}
