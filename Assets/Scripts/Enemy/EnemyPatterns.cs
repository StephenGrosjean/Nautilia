using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatterns : MonoBehaviour
{
    //Modifiers
    public enum modifier { circle, player, burst, oneTime};
    public modifier mode;

    //Objects needed for the script (Class)
    [System.Serializable]
    private class neededObjects {
        [HideInInspector] public Transform shootPoint;
        public GameObject shootObject;
        [HideInInspector] public Transform bulletContainer;
    }

    //Objects needed for the script (Variable)
    [SerializeField] private neededObjects requiredObjects;
    [Space(15)]

    //All modes variables
    [HideInInspector] public int number;
    [HideInInspector] public float speed;
    [HideInInspector] public float acceleration;
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
    private BulletPooler objectPool;

    #region DebugGizmo
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
    #endregion


    void Start()
    {
//        objectPool = GameObject.FindGameObjectWithTag("Pool").GetComponent<BulletPooler>();
        requiredObjects.bulletContainer = GameObject.FindGameObjectWithTag("Container").transform; //Get the container for the bullet
        player = GameObject.FindGameObjectWithTag("Player").transform; //Get the player object
        requiredObjects.shootPoint = transform; //Get the transform reference
        Invoke("SetMode", 0.2f); //Set the mode of the enemy
    }


    void SetMode() {
        CancelInvoke(); //Cancel all previous invoke (In case of change of mode in game)

        //Set the mode and invoke a function according to the mode
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
        //Rotate the point
        if (rotationSpeed != 0) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }

        //Check if the mode is changed
        if(prevMode != mode) {
            SetMode();
        }
        prevMode = mode;

        //If the mode is target player, rotate toward the player
        if (mode == modifier.player) {
            number = 1;
            Vector3 vectorToTarget = player.position - transform.position;
            float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }

    }

    //Spawn bullets function ((Caller)
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

    //Spawn bullets in a burst style (Caller)
    IEnumerator BurstSpawn() {
        while (mode == modifier.burst) {

            for (int ii = 1; ii < numberByBurst + 1; ii++) {
                for (int i = 1; i < number + 1; i++) {
                    SpawnBullet(i);
                }
                float t = Time.time;
                while (Time.time < t + timeBetweenBulletsInBurst) { yield return null; }
                t = 0;
                //yield return new WaitForSeconds(timeBetweenBulletsInBurst);
            }
            float tt = Time.time;
            while (Time.time < tt + timeBetweenBursts) { yield return null; }
            tt = 0;
            //yield return new WaitForSeconds(timeBetweenBursts);
        }
    }


    //Spawn bullets in single shot (Caller)
    IEnumerator SingleShot() {
        for (int i = 1; i < number + 1; i++) {
            SpawnBullet(i);
        }
        yield return new WaitForSeconds(spawnTime);
    }


    //Actual spawn bullet function
    void SpawnBullet(int i = 1, float divider = 1) {
        //If mode isn't target player, spawn bullets in a circle
        if (mode != modifier.player) {
            float angle = (i * (Mathf.PI * 2 / number) - Mathf.PI * 2) + transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            //GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.position, Quaternion.Euler(Mathf.Cos(angle), Mathf.Sin(angle), 0));
            GameObject bullet = objectPool.GetBullet();
            bullet.transform.position = requiredObjects.shootPoint.position;
            bullet.transform.rotation = Quaternion.Euler(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            SetBulletParams(bullet, pos, divider);
        }
        //If it's targeting player, shoot on a single axis toward player (Rotated in update function)
        else {
            Vector2 pos = new Vector2(player.position.x + dispersionAngle, player.position.y) - (Vector2) requiredObjects.shootPoint.position;
            GameObject bullet = Instantiate(requiredObjects.shootObject, requiredObjects.shootPoint.transform.position, transform.rotation);
            bullet.transform.parent = requiredObjects.bulletContainer;
            SetBulletParams(bullet, pos, 100);
        }
    }

    //Set the params for the bullet
    void SetBulletParams(GameObject bullet, Vector2 pos, float divider = 1) {
        BulletBehaviour bbh = bullet.GetComponent<BulletBehaviour>();
        bullet.SetActive(true);
        bbh.Acceleration = acceleration;
        bbh.IsSine = waveMode;
        bbh.IsArround = arroundMode;
        //bullet.GetComponent<BulletBehaviour>().SetVel(pos, speed, divider);
        bbh.target = pos * 10;
        bbh.WaveSpeed = waveSpeed;
        bbh.Initiator = gameObject;
    }
}

