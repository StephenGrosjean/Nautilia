using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public float shootDelay = 0.2f;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerShoot();
        }
    }

    private void PlayerShoot()
    {
        shootDelay -= Time.deltaTime;
        if (shootDelay <= 0)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            shootDelay = 0.2f;

        }
    }
}
