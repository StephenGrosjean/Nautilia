using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed {
        set { bulletSpeed = value; }
    }

    private Rigidbody2D bulletRb;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bulletRb.velocity = new Vector2(0, bulletSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
