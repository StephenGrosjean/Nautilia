using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    [SerializeField] private GameObject bulletPrefab;
    public float shootDelay = 0.2f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (shootDelay <= 0)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                shootDelay = 0.2f;
            }
        }
        shootDelay -= Time.deltaTime;
    }
}
