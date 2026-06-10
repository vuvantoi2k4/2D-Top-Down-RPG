using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        animator.SetFloat("moveX", playerInput.Movement.x);
        animator.SetFloat("moveY", playerInput.Movement.y);
    }
}