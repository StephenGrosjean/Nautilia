using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControls : MonoBehaviour { 


    private EnemyLife lifeScript;
    private bool isBlinking;
    private SpriteRenderer spriteRendererComponent;


    void Start()
    {
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
        lifeScript = GetComponent<EnemyLife>(); //Get life script 
    }

    public void Hit() {
        if (!lifeScript.IsImortal) {
            lifeScript.DecreaseLife(1); //Decrease life
            if (!isBlinking) {
                isBlinking = true;
                StartCoroutine("Blink");
            }
        }
    }
    
    void FixedUpdate()
    {


        //Destroy the object if life is lower than 0
        if (lifeScript.GetLife() <= 0)
        {
            Destroy(gameObject);
        }

    }

    //Blink Ienumerator (Need optimisation)
    IEnumerator Blink() {
        float time = 0.03f;
        spriteRendererComponent.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        spriteRendererComponent.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }
}


