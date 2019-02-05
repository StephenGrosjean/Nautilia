    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration;
    public float Acceleration {
        set { acceleration = value; }
    }

    [SerializeField] private bool isSine;
    public bool IsSine {
        set { isSine = value; }
    }

    [SerializeField] private bool isArround;
    public bool IsArround {
        set { isArround = value; }
    }

    private Rigidbody2D rigid;
    private Vector2 vel;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        vel = rigid.velocity;

        GetComponentInChildren<Animator>().SetBool("isSine", isSine);
        GetComponentInChildren<Animator>().SetBool("isArround", isArround);

        Vector3 vectorToTarget = vel - (Vector2)transform.position;
        float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
    }
    void FixedUpdate()
    {
        vel = rigid.velocity;

        rigid.AddForce(vel * acceleration/50, ForceMode2D.Force);


    }

}
