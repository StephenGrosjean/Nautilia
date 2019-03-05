using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParticleManager : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private Player playerScript;
    
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isPlayer, pingPongValues;
    [SerializeField] private float rotationAngle;
    [SerializeField] private GameObject hitParticle;

    private float baseOffset;
    private bool increase = true;


    void Start() {
        baseOffset = transform.localEulerAngles.z; //Get base rotation
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    //Detect collision particle
    void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents); 

        foreach(ParticleCollisionEvent colEvent in collisionEvents) {
            if (colEvent.colliderComponent) {
                //IF the particle is from the enemy and hit the player
                if (colEvent.colliderComponent.gameObject.CompareTag("PlayerHitBox") && !isPlayer) {
                    if (!playerScript.IsInvincible) {
                        playerScript.Hit();
                    }
                }
                //IF the particle is from the player and hit an enemy
                else if (colEvent.colliderComponent.gameObject.CompareTag("Enemy") && isPlayer) {
                    colEvent.colliderComponent.gameObject.GetComponent<EnemyControls>().Hit();
                    float rdmn = Random.Range(0.0f, 100.0f);
                    if (rdmn < 80) {
                        Instantiate(hitParticle, colEvent.intersection, hitParticle.transform.rotation);//Spawn hit effect
                    }
                }
                //IF the particle is from the player and hit the boss
                else if (colEvent.colliderComponent.gameObject.CompareTag("Boss") && isPlayer) {
                    colEvent.colliderComponent.gameObject.GetComponent<BossControls>().Hit();
                    float rdmn = Random.Range(0.0f, 100.0f);
                    if (rdmn < 80 && !colEvent.colliderComponent.gameObject.GetComponent<BossControls>().IsImortal) {
                        Instantiate(hitParticle, colEvent.intersection, hitParticle.transform.rotation); //Spawn hit effect
                    }
                }
            }
        }
    }

    
    void Update() {
        //ping pong between two angles
        if (pingPongValues) {
            float angle = Mathf.Sin(Time.time * rotationSpeed / 10) * rotationAngle;
            transform.localRotation = Quaternion.AngleAxis(angle + Mathf.Abs(baseOffset), Vector3.forward);
        }
        else {
                transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
    }

}
