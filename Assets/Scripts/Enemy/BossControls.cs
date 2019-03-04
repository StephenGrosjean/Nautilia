using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControls : MonoBehaviour {
    [SerializeField] private Color blinkColor;
    [SerializeField] private GameObject particlesContainer;

    private EnemyLife lifeScript;
    private bool isBlinking;
    private SpriteRenderer spriteRendererComponent;
    private bool defeated;

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
            Destroy(gameObject, 5);
            transform.position = Vector2.MoveTowards(transform.position, -transform.up*500, Time.deltaTime);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInChildren<SpriteAnimation>().enabled = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(60, Vector3.forward), 1 * Time.deltaTime);
            if (!defeated) {
                StartCoroutine(Fade());
            }
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

    IEnumerator Fade() {
        defeated = true;
        for (float i = 0; i < 1.1f; i += 0.1f) {
            GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}


