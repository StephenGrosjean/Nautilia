using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public float BulletSpeed {
        set { bulletSpeed = value; }
    }

   // private Rigidbody2D bulletRb;

    private void Start()
    {
        //bulletRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Vector2.up*50, Time.deltaTime * bulletSpeed* 10);
        //bulletRb.velocity = new Vector2(0, bulletSpeed);
    }

    private void OnBecameInvisible()
    {
        Invoke("Destroy", 0);
    }

    public void Destroy() {
        gameObject.SetActive(false);
    }
}
