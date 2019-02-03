using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDraw : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Color color;

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
