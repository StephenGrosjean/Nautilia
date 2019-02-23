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
        baseOffet = transform.eulerAngles.z;
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
            }
        }
    }

   void Update() {
        if (pingPongValues) {
            if (transform.eulerAngles.z - baseOffet >= maximumRotation) {
                increase = false;
            }
            if (transform.eulerAngles.z - baseOffet <= minimumRotation) {
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


}
