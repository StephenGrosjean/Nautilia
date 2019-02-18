using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    [SerializeField] private GameObject bulletPrefab;
    
    public float shootDelay = 0.05f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            if (shootDelay <= 0)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                shootDelay = 0.05f;
            }
        }
        shootDelay -= Time.deltaTime;
    }
}
