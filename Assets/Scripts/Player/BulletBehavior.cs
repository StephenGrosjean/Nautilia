using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _bulletRb;
    private void Start()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _bulletRb.velocity = new  Vector2(0.0f,speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 0.5f);
    }
}
