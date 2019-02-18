﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration; //Acceleration of the bullet
    [SerializeField] private bool isSine; //Animation wave toggle
    [SerializeField] private bool isArround; //Animation form toggle
    [SerializeField] private float waveSpeed; //Speed of the wave animation
    [SerializeField] private GameObject initiator; //Who spawned the bullet?

    [SerializeField] private GameObject point;

    //Public variables (Get/Set)
    public float Acceleration {
        set { acceleration = value; }
    }
    public bool IsSine {
        set { isSine = value; }
    }
    public bool IsArround {
        set { isArround = value; }
    }
    public float WaveSpeed {
        set { waveSpeed = value; }
    }
    public GameObject Initiator {
        get { return initiator; }
        set { initiator = value; }
    }

    //Private Variables
    private float speed;
    private Rigidbody2D rigid;
    private Vector2 vel;

    //FailSafe
    private bool canCheckInitiator;


    void Awake()
    {
        //Get rigidbody component
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Start() {
        //Set animation states
        GetComponentInChildren<Animator>().SetBool("isSine", isSine);
        GetComponentInChildren<Animator>().SetBool("isArround", isArround);
        GetComponentInChildren<Animator>().SetFloat("Speed", waveSpeed);
        canCheckInitiator = true;
    }

    void FixedUpdate()
    {
        vel = rigid.velocity; //Set the velocity
        speed = vel.magnitude; //Set the speed

        if(initiator == null && canCheckInitiator) {
            Invoke("MakePoint",0);
        }

        //Enable acceleration
        if (acceleration > 0) {
            rigid.AddForce(vel * acceleration / 50, ForceMode2D.Force);
        }

        //Rotate the bullet toward the velocity vector
        Vector3 vectorToTarget = vel * 5 - (Vector2)transform.position;
        float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);

    }

    //Set the velocity (used by the bullet spawner)
    public void SetVel(Vector2 pos, float speed, float divider = 1) {
        if (speed > 0.0f) {
            rigid.velocity = pos * speed / divider;
        }
    }

    //Destroy the bullet when offscreen
   public void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void MakePoint() {
        float random = Random.Range(0.0f, 100.0f);
        if (random > 80) {
            Instantiate(point, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
