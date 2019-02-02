using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{

    private enum modifier { circle, player};
    private Transform player;

    [Header("Spawn Objects")]
    [SerializeField] private Transform shootPoint, shootDirection;
    [SerializeField] private GameObject shootObject;

    [Space(15)]
    [Header("Spawn Settings")]
    [Range(0, 75)]
    [SerializeField] private int number;
    [Range(0.0f, 50.0f)]
    [SerializeField] private float speed;
    [Range(-360, 360)]
    [SerializeField] private int rotationSpeed;
    [Range(0.0f, 5.0f)]
    [SerializeField] private float spawnSpeed;
    [Range(0.0f, 3.0f)]
    [SerializeField] private float dispersionAngle;

    [Space(15)]
    [Header("Modifiers")]
    [SerializeField] private modifier mode;

    //Variables for the shoot at player mode
    private float dispersionValue = 0;
    private bool decrease;

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        for(int i = 0; i < number; i++) {
            float angle = (i * (Mathf.PI*2 / number) - Mathf.PI * 2) + transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(shootPoint.position.x + 0.1f * Mathf.Cos(angle), shootPoint.position.y + 0.1f * Mathf.Sin(angle));
            Gizmos.DrawLine(transform.position, pos * 10);
        }
    }

    void Start()
    {
        dispersionValue = -dispersionAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnParticles", 0, spawnSpeed);
    }

    void Update()
    {
        if (rotationSpeed != 0) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        if(mode == modifier.player) {
            Vector3 vectorToTarget = player.position - transform.position;
            float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 15);
        }

    }

    void SpawnParticles() {
        if (mode == modifier.circle) {
            for (int i = 1; i < number + 1; i++) {
                float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.eulerAngles.z * Mathf.Deg2Rad;
                Vector2 pos = new Vector2(shootPoint.position.x + 0.1f * Mathf.Cos(angle), shootPoint.position.y + 0.1f * Mathf.Sin(angle));
                GameObject bullet = Instantiate(shootObject, pos, transform.rotation);
                Vector2 vel = (Vector2)shootPoint.position - pos;
                bullet.GetComponent<Rigidbody2D>().velocity = pos * speed;
            }
        }
        else if(mode == modifier.player) {
            if(dispersionValue > dispersionAngle) {
                decrease = true;
            }

            if (dispersionValue < -dispersionAngle){
                decrease = false;
            }

            if (decrease) {
                dispersionValue -= 0.1f;
            }
            else {
                dispersionValue += 0.1f;
            }

            float offset = dispersionValue;
            Vector2 pos = new Vector2(shootDirection.position.x+dispersionValue, shootDirection.position.y);
            GameObject bullet = Instantiate(shootObject, shootPoint.transform.position, transform.rotation);
            Vector2 vel = (Vector2)pos - (Vector2)shootPoint.position;
            bullet.GetComponent<Rigidbody2D>().velocity = pos * speed;
        }
    }
}

