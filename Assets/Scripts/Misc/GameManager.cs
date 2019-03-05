using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    public static GameObject GetPlayer() {
        return player;
    }
}
