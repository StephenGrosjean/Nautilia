using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life; //Life script 

    private bool isImortal;
    public bool IsImortal {
        get { return isImortal; }
        set { isImortal = value; }
    }
    //Decrease life function
    public void DecreaseLife(int value)
    {
        if (!isImortal) {
            life -= value;
        }
    }

    //Increase life function
    public void IncreaseLife(int value)
    {
        life += value;
    }


    //Return the life variable
    public int GetLife()
    {
        return life;
    }
}
