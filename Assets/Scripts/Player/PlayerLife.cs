using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int life = 5; //Life script 
    public int Life => life;

    //Difficulty base lifes
    private int babyLifes = 15;
    private int normalLifes = 10;
    private int hardLifes = 5;
    private int impossibleLifes = 2;
    //

    private float difficulty;

    private void Start() {
        XMLSave.instance.Load();
        difficulty = XMLSave.instance.dataBase.firstDB[7].value; //Get difficulty from XML
        SetLife();
    }

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

    //Set life based on difficulty
    private void SetLife() {
        switch (difficulty) {
            case 0:
                life = babyLifes;
                break;
            case 1:
                life = normalLifes;
                break;
            case 2:
                life = hardLifes;
                break;
            case 3:
                life = impossibleLifes;
                break;
        }
    }
}
