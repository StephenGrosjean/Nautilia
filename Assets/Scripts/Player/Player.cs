using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject[] doubleShotPoint; 
    [SerializeField] private GameObject[] quadShotPoint;
    [SerializeField] private GameObject singleShotPoint;
    
    public float accelleration  = 11.0f;

    private void DoubleShotUpgrade()
    {
        for (int i = 0; i < doubleShotPoint.Length; i++)
        {
            doubleShotPoint[i].SetActive(true);
        }
        
        singleShotPoint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            DoubleShotUpgrade();
        }
    }
}
