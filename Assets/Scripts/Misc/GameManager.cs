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
    }

    public static GameObject GetPlayer() {
        return player;
    }
}
