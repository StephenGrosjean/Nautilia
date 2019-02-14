using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PointCollector")) {
            collision.transform.parent.GetComponent<Points>().AddScore(1000);
            Destroy(gameObject);
        }
    }

    
}
