using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{

    public enum modifier { circle, player, burst, oneTime};

    [System.Serializable]
    private class neededObjects {
        [HideInInspector] public Transform shootPoint;
        public GameObject shootObject;
        [HideInInspector] public Transform bulletContainer;
    }

    [SerializeField] private neededObjects requiredObjects;
    [Space(15)]

    public modifier mode;

    //All modes variables
    [HideInInspector] public int number;
    public float speed;
    [HideInInspector] public float accelleration;
    [HideInInspector] public bool waveMode, arroundMode;
    [HideInInspector] public float waveSpeed;

    //Circle Mode Variables
    [HideInInspector] public int rotationSpeed;
    [HideInInspector] public float spawnTime;

    //Player Mode Variables
    [HideInInspector] public float dispersionAngle; // not implemented

    //Burst Mode Variables
    [HideInInspector] public int numberByBurst;
    [HideInInspector] public float timeBetweenBursts;
    [HideInInspector] public float timeBetweenBulletsInBurst;

    
    //Variables for the shoot at player mode
    private bool decrease;
    private Transform player;
    private modifier prevMode;

    private void OnDrawGizmosSelected() {
        requiredObjects.shootPoint = transform;
        if (mode != modifier.player) {
            Gizmos.color = Color.red;
            for (int i = 0; i < number; i++) {
                float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.eulerAngles.z * Mathf.Deg2Rad;
                Vector2 pos = new Vector2(requiredObjects.shootPoint.position.x + 1f * Mathf.Cos(angle), requiredObjects.shootPoint.position.y + 1f * Mathf.Sin(angle));
                Gizmos.DrawLine(requiredObjects.shootPoint.position, pos);
            }
        }
    }

    private void OnDrawGizmos () {
        requiredObjects.shootPoint = transform;
        if (mode != modifier.player) {
            Gizmos.color = Color.green;
            for (int i = 0; i < number; i++) {
                float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.eulerAngles.z* Mathf.Deg2Rad;
                Vector2 pos = new Vector2(requiredObjects.shootPoint.position.x + 1f* Mathf.Cos(angle), requiredObjects.shootPoint.position.y + 1f * Mathf.Sin(angle));
                Gizmos.DrawLine(requiredObjects.shootPoint.position, pos);
            }
        }
    }

    void Start()
    {
        requiredObjects.bulletContainer = GameObject.FindGameObjectWithTag("Container").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        requiredObjects.shootPoint = transform;
        Invoke("SetMode", 0.2f);
    }

    void SetMode() {
        CancelInvoke();

        if (mode != modifier.burst && mode != modifier.oneTime) {
            InvokeRepeating("SpawnParticles", 0, spawnTime);
        }
        else if(mode == modifier.burst){
            StartCoroutine("BurstSpawn");
        }
        else if(mode == modifier.oneTime) {
            StartCoroutine("SingleShot");

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
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }

    }

    void SpawnParticles() {
        if (mode == modifier.circle) {
            for (int i = 1; i < number + 1; i++) {
                SpawnBullet(i);
            }
        }
        else if (mode == modifier.player) {
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

    IEnumerator SingleShot() {
        for (int i = 1; i < number + 1; i++) {
            SpawnBullet(i);
        }
        yield return new WaitForSeconds(spawnTime);
    }

    void SpawnBullet(int i = 1, float divider = 1) {
        if (mode != modifier.player) {
            float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.position, Quaternion.Euler(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            bullet.transform.parent = requiredObjects.bulletContainer;
            SetBulletParams(bullet, pos, divider);
        }
        else {
            Vector2 pos = new Vector2(player.position.x + dispersionAngle, player.position.y) -(Vector2) requiredObjects.shootPoint.position;
            GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.transform.position, transform.rotation);
            bullet.transform.parent = requiredObjects.bulletContainer;
            SetBulletParams(bullet, pos, 100);
        }
    }

    void SetBulletParams(GameObject bullet, Vector2 pos, float divider = 1) {

        bullet.GetComponent<BulletBehaviour>().Acceleration = accelleration;
        bullet.GetComponent<BulletBehaviour>().IsSine = waveMode;
        bullet.GetComponent<BulletBehaviour>().IsArround = arroundMode;
        bullet.GetComponent<BulletBehaviour>().SetVel(pos, speed, divider);
        bullet.GetComponent<BulletBehaviour>().WaveSpeed = waveSpeed;

    }
}

