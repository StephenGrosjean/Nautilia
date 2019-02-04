using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject doubleShotPoint; 
    [SerializeField] private GameObject quadShotPoint;
    [SerializeField] private GameObject singleShotPoint;

    public int shootMode = 0;
    private void DoubleShotUpgrade()
    {
        doubleShotPoint.SetActive(true);
        
        singleShotPoint.SetActive(false);
    }
    
    private void QuadShotUpgrade()
    {
        quadShotPoint.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            shootMode++;
            ShootingMode();
            GetComponent<Shoot>().shootDelay = 0.2f;
        }
    }
    
    private void ShootingMode()
    {
        if (shootMode == 1)
        {
            DoubleShotUpgrade();
        }
        else if(shootMode == 2)
        {
            QuadShotUpgrade();
        }
    }
}
