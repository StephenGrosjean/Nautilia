using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private float speedUpgrade = 4.0f ;
    public PlayerMovement player;
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.playerSpeed += speedUpgrade;
            Destroy(gameObject);
        }
    }
}
