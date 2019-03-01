using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControls : MonoBehaviour {
    [SerializeField] private Color blinkColor;
    [SerializeField] private GameObject particlesContainer;

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
            particlesContainer.transform.SetParent(null);
            Destroy(gameObject);
        }

    }

    //Blink Ienumerator (Need optimisation)
    IEnumerator Blink() {
        float time = 0.03f;
        spriteRendererComponent.color = blinkColor;
        yield return new WaitForSeconds(time);
        spriteRendererComponent.color = Color.white;
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }
}


