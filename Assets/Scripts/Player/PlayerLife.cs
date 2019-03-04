using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int life = 5; //Life script 
    public int Life => life;

    //Decrease life function
    public void DecreaseLife(int value)
    {
        life -= value;
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
