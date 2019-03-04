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

    private float baseOffset;
    private bool increase = true;

    void OnDrawGizmosSelected () {

    }

    void Start() {
        baseOffset = transform.localEulerAngles.z;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents);

        foreach(ParticleCollisionEvent colEvent in collisionEvents) {
            if (colEvent.colliderComponent) {
                if (colEvent.colliderComponent.gameObject.CompareTag("PlayerHitBox") && !isPlayer) {
                    if (!playerScript.IsInvincible) {
                        playerScript.Hit();
                    }
                }
                else if (colEvent.colliderComponent.gameObject.CompareTag("Enemy") && isPlayer) {
                    colEvent.colliderComponent.gameObject.GetComponent<EnemyControls>().Hit();
                }
                else if (colEvent.colliderComponent.gameObject.CompareTag("Boss") && isPlayer) {
                    colEvent.colliderComponent.gameObject.GetComponent<BossControls>().Hit();
                }
            }
        }
    }

    void Update() {

        if (pingPongValues) {

            /*if (WrapAngle(transform.eulerAngles.z)  >= maximumRotation) {
                increase = false;
            }
            if (WrapAngle(transform.eulerAngles.z)  <= minimumRotation) {
                increase = true;
            }

            if (increase) {
                rotationSpeed = rotationIncrement;
            }
            else {
                rotationSpeed = -rotationIncrement;
            }*/
            float angle = Mathf.Sin(Time.time * rotationSpeed / 10) * rotationAngle;
            transform.localRotation = Quaternion.AngleAxis(angle + Mathf.Abs(baseOffset), Vector3.forward);
        }
        else {
                transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
            
        
    }
    private float WrapAngle(float angle) {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

}
