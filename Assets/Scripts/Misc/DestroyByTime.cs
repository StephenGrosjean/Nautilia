using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float destroyIn;

    void Start()
    {
        Destroy(gameObject, destroyIn);
    }
}
