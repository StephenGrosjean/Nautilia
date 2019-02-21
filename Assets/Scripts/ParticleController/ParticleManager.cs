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
    private ParticleSystem.Particle[] particlesArray;
    private EnemyControls enemyControls;
    private bool called;

    void Start() {
        enemyControls = transform.parent.GetComponent<EnemyControls>();
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents);

        foreach(ParticleCollisionEvent colEvent in collisionEvents) {
            if (colEvent.colliderComponent.gameObject.CompareTag("PlayerHitBox")) {
                if (!playerScript.IsInvincible) {
                    playerScript.Hit();
                }
            }
            else if(colEvent.colliderComponent.gameObject.CompareTag("Enemy")) {
                colEvent.colliderComponent.gameObject.GetComponent<EnemyControls>().Hit();
            }
        }
    }

    private void Update() {
        if (enemyControls.isDestroying && !called) {
            called = true;
            TransformParticles();
        }
        part.GetParticles(particlesArray);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

   void TransformParticles() {
        Debug.Log("Called");
        foreach (ParticleSystem.Particle particles in particlesArray) {
            Debug.Log("hey");
        }    
    }

}
