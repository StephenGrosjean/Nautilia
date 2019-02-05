﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
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
    public Vector2 vel;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Start() {

        GetComponentInChildren<Animator>().SetBool("isSine", isSine);
        GetComponentInChildren<Animator>().SetBool("isArround", isArround);
    }

    void FixedUpdate()
    {
        vel = rigid.velocity;
        speed = vel.magnitude;
        rigid.AddForce(vel * acceleration/50, ForceMode2D.Force);
        Vector3 vectorToTarget = vel * 5 - (Vector2)transform.position;
        float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);

    }
    public void SetVel(Vector2 pos, float speed, float divider = 1) {
        rigid.velocity = pos * speed / divider;
    }
}
