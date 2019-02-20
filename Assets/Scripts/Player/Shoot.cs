using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    [SerializeField] private GameObject bulletPrefab;
    
    public float shootDelay = 0.05f;

    private void Start() {
        pool = GameObject.FindGameObjectWithTag("PoolPlayer").GetComponent<BulletPooler>();
    }

    private void FixedUpdate()
    {
        if (shootDelay <= 0)
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

    
    [SerializeField] private GameObject bulletPrefab;

    private BulletPooler pool;
            if (shootDelay <= 0)
            {
                //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                GameObject bullet = pool.GetBullet();
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                shootDelay = 0.05f;
            }