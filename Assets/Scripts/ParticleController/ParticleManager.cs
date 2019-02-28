using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParticleManager : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private Player playerScript;
    [SerializeField] private float rotationIncrement;
    [SerializeField] private bool isPlayer, pingPongValues;
    [SerializeField] private float minimumRotation, maximumRotation;

    private float rotationSpeed, baseOffet;
    private bool increase = true;

    void Start() {
        baseOffet = WrapAngle(transform.localEulerAngles.z);
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
            if (WrapAngle(transform.localEulerAngles.z) - baseOffet >= maximumRotation) {
                increase = false;
            }
            if (WrapAngle(transform.localEulerAngles.z) - baseOffet <= minimumRotation) {
                increase = true;
            }

            if (increase) {
                rotationSpeed = rotationIncrement;
            }
            else {
                rotationSpeed = -rotationIncrement;
            }
        }
        else {
            rotationSpeed = rotationIncrement;
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    private float WrapAngle(float angle) {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

}
