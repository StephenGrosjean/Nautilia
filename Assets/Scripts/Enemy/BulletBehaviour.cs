using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public bool CanBePulled = true;

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
    public Vector2 target;
    private Transform player;
    private Collider2D collider;
    private BulletPooler currentPooler;

    //FailSafe
    private bool canCheckInitiator;
    

    void Awake()
    {
        currentPooler = BulletPooler.current;
        //Get rigidbody component
        //rigid = GetComponent<Rigidbody2D>();
    }


    private void Start() {
        player = GameManager.player.transform;
    }

    private void OnEnable() {
        canCheckInitiator = true;

        //Set animation states
        /*GetComponentInChildren<Animator>().SetBool("isSine", isSine);
        GetComponentInChildren<Animator>().SetBool("isArround", isArround);
        GetComponentInChildren<Animator>().SetFloat("Speed", waveSpeed);*/

        collider = GetComponentInChildren<Collider2D>();


        //disable animator if not used
        if (!isSine && !isArround) {
            //GetComponentInChildren<Animator>().enabled = false;
        }
    }

    void Update()
    {
       // vel = rigid.velocity; //Set the velocity
        speed = vel.magnitude; //Set the speed

        //Enable collision detection by distance
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < 1 && collider.enabled == false) {
            collider.enabled = true;
        }else if(distance > 1 && collider.enabled == true) {
            collider.enabled = false;
        }

        //Transform to point if the initiator is destroyed
        if(initiator == null && canCheckInitiator) {
            Invoke("MakePoint",0);
        }

        //Enable acceleration
        /*if (acceleration > 0) {
            rigid.AddForce(vel * acceleration / 50, ForceMode2D.Force);
        }*/

        /* //Rotate the bullet toward the velocity vector
         Vector3 vectorToTarget = vel * 5 - (Vector2)transform.position;
         float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
         Quaternion q = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
         transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);*/

        if (target != (Vector2)currentPooler.OffPos.position) {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * 1f);
        }
    }

    //Set the velocity (used by the bullet spawner)
    public void SetVel(Vector2 pos, float speed, float divider = 1) {
        if (speed > 0.0f) {
            rigid.velocity = pos * speed / divider;
        }
    }

    //Destroy the bullet when offscreen
   public void OnBecameInvisible() {
        Invoke("Destroy", 0);

    }

    void MakePoint() {
        float random = Random.Range(0.0f, 100.0f);
        if (random > 40) {
            Instantiate(point, transform.position, Quaternion.identity);
        }
        Invoke("Destroy", 0);
    }

    void Destroy() {
        canCheckInitiator = false;
        collider.enabled = false;
        resetParams();
        currentPooler.MoveOff(gameObject);
    }
    private void OnDisable() {
        CancelInvoke();
    }

    private void resetParams() {
        acceleration = 0;
        isSine = false;
        isArround = false;
        target = currentPooler.OffPos.position;
        waveSpeed = 0;
        initiator = null;
        CanBePulled = true;
    }
}
