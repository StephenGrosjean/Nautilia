using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{

    public enum modifier { circle, player, burst};

    [System.Serializable]
    private class neededObjects {
        public Transform shootPoint;
        public Transform shootDirection;
        public GameObject shootObject;
    }

    [SerializeField] private neededObjects requiredObjects;
    [Space(15)]

    public modifier mode;

    //All modes variables
    [HideInInspector] public int number;
    [HideInInspector] public float speed;
    [HideInInspector] public float accelleration;
    [HideInInspector] public bool waveMode, arroundMode;

    //Circle Mode Variables
    [HideInInspector] public int rotationSpeed;
    [HideInInspector] public float spawnTime;

    //Player Mode Variables
    [HideInInspector] public float dispersionAngle;

    //Burst Mode Variables
    [HideInInspector] public int numberByBurst;
    [HideInInspector] public float timeBetweenBursts;
    [HideInInspector] public float timeBetweenBulletsInBurst;

    
    //Variables for the shoot at player mode
    private float dispersionValue = 0; 
    private bool decrease;
    private Transform player;
    private modifier prevMode;


    private void OnDrawGizmosSelected () {
        if (mode != modifier.player) {
            Gizmos.color = Color.red;
            for (int i = 0; i < number; i++) {
                float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.localEulerAngles.z * Mathf.Deg2Rad;
                Vector2 pos = new Vector2(requiredObjects.shootPoint.localPosition.x + 1f* Mathf.Cos(angle), requiredObjects.shootPoint.localPosition.y + 1f * Mathf.Sin(angle));
                Gizmos.DrawLine(requiredObjects.shootPoint.localPosition, pos);
            }
        }
    }

    void Start()
    {
        dispersionValue = -dispersionAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        SetMode();
    }

    void SetMode() {
        CancelInvoke();

        if (mode != modifier.burst) {
            InvokeRepeating("SpawnParticles", 0, spawnTime);
        }
        else {
            StartCoroutine("BurstSpawn");
        }
    }

    void Update()
    {
        if (rotationSpeed != 0) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        if(prevMode != mode) {
            SetMode();
        }
        prevMode = mode;

        if (mode == modifier.player) {
            number = 1;
            Vector3 vectorToTarget = player.position - transform.position;
            float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 15);
        }

    }

    void SpawnParticles() {
        if (mode == modifier.circle) {
            for (int i = 1; i < number + 1; i++) {
                SpawnBullet(i);
            }
        }
        else if (mode == modifier.player) {

            if (dispersionValue > dispersionAngle) {
                decrease = true;
            }

            if (dispersionValue < -dispersionAngle) {
                decrease = false;
            }

            if (decrease) {
                dispersionValue -= 0.5f;
            }
            else {
                dispersionValue += 0.5f;
            }

            SpawnBullet();

        }
    }

    IEnumerator BurstSpawn() {
        while (mode == modifier.burst) {

            for (int ii = 1; ii < numberByBurst + 1; ii++) {
                for (int i = 1; i < number + 1; i++) {
                    SpawnBullet(i);
                }
                yield return new WaitForSeconds(timeBetweenBulletsInBurst);
            }
            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
  
    void SpawnBullet(int i = 1) {
        if (mode != modifier.player) {
            float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.localEulerAngles.z * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.position, Quaternion.Euler(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            SetBulletParams(bullet, pos);
        }
        else {
            Vector2 pos = new Vector2(requiredObjects.shootDirection.position.x + dispersionValue, requiredObjects.shootDirection.position.y);
            GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.transform.position, transform.rotation);
            SetBulletParams(bullet, pos, 100);
        }
    }

    void SetBulletParams(GameObject bullet, Vector2 pos, int divider = 1) {

        bullet.GetComponent<BulletBehaviour>().Acceleration = accelleration;
        bullet.GetComponent<BulletBehaviour>().IsSine = waveMode;
        bullet.GetComponent<BulletBehaviour>().IsArround = arroundMode;
        bullet.GetComponent<BulletBehaviour>().SetVel(pos, speed, divider);

    }
}

