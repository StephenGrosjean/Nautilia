using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Collectable points
/// </summary>
public class Point : MonoBehaviour
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

    private void Update()
    {
        //Enable collider if close to player (Save some FPS)
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 1)
        {
            baseCollider.enabled = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 6);
    }

}
