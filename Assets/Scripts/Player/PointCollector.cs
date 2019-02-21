using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollector : MonoBehaviour
{
    [SerializeField] private float collisionRadius;
    [SerializeField] private int scorePerPoint;
    [SerializeField] private LayerMask mask;
    public Collider2D[] hitColliders;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, collisionRadius, mask);

        foreach(Collider2D col in hitColliders) {
            ScoreManager.AddScore(scorePerPoint);
            col.gameObject.SetActive(false);
        }
    }
}
