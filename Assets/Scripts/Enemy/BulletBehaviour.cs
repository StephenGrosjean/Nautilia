using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration;
    public float Acceleration {
        set { acceleration = value; }
    }
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vel = rigid.velocity;
        rigid.AddForce(vel * acceleration/50, ForceMode2D.Force);
    }
}
