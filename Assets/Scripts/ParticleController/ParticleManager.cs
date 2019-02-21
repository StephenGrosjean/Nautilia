using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParticleManager : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private Player playerScript;

    void Start() {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(part, other, collisionEvents);

        foreach(ParticleCollisionEvent colEvent in collisionEvents) {
            if (!playerScript.IsInvincible) {
                if (colEvent.colliderComponent.gameObject.CompareTag("PlayerHitBox")) {
                    playerScript.Hit();
                }
            }
        }
    }

    void HitPlayer(ParticleCollisionEvent particleCollisionEvent) {

    }
}
