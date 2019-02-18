using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 100; //Life script 

    //Decrease life function
    public void DecreaseLife(int value) {
        life -= value;
    }

    //Increase life function
    public void IncreaseLife(int value) {
        life += value;
    }


    //Return the life variable
    public int GetLife() {
        return life;
    }
}
