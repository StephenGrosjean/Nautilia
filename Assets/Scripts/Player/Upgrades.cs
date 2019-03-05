using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Upgrade script
public class Upgrades : MonoBehaviour
{
    private GameObject player;
    private Collider2D baseCollider;
    private Vector2 basePos;
    
    private void Start()
    {
        basePos = transform.position;
        player = GameManager.GetPlayer();
        baseCollider = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance < 1)
        {
            baseCollider.enabled = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, Vector2.down * 200, Time.deltaTime * 2);
    }
    
    
}
