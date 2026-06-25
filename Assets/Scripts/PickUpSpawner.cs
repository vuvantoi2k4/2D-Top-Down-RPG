using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goilCoinPrefab;

    public void DropItems()
    {
        Instantiate(goilCoinPrefab, transform.position, Quaternion.identity);
    }
}
