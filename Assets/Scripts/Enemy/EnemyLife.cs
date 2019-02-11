using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseLife(int value) {
        life -= value;
    }

    public void IncreaseLife(int value) {
        life += value;
    }

    public int GetLife() {
        return life;
    }
}
