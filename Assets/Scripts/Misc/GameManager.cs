using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public GameObject[] bullets;
    public int bulletsNumber;

    public bool check;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        if (check) {
            GetBullets();
        }

        if (bullets.Length > 0) {
            bulletsNumber = bullets.Length;
        }
    }

    public static GameObject GetPlayer() {
        return player;
    }

    void GetBullets() {
        bullets = null;
        bullets = GameObject.FindGameObjectsWithTag("Bullet") ;
    }
}
