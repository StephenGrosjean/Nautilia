using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
    }
}
