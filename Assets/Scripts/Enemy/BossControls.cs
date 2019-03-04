using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControls : MonoBehaviour {
    [SerializeField] private Color blinkColor;
    [SerializeField] private GameObject particlesContainer;
    [SerializeField] private GameObject powerup;
    private EnemyLife lifeScript;
    private int maxLife;
    private bool isBlinking;
    private SpriteRenderer spriteRendererComponent;
    private bool defeated;
    private bool firstPowerupSpawned, secondPowerupSpawned;

    private bool isImortal;
    public bool IsImortal => isImortal;

    void Start()
    {
        spriteRendererComponent = transform.Find("EnemySprite").GetComponent<SpriteRenderer>();
        lifeScript = GetComponent<EnemyLife>(); //Get life script 
        maxLife = lifeScript.GetLife();
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
        isImortal = lifeScript.IsImortal;
        if(lifeScript.GetLife() < maxLife / 2 && !firstPowerupSpawned) {
            firstPowerupSpawned = true;
            Instantiate(powerup, transform.position, transform.rotation);
        }

        if (lifeScript.GetLife() < (maxLife / 2)/2 && firstPowerupSpawned && !secondPowerupSpawned) {
            secondPowerupSpawned = true;
            Instantiate(powerup, transform.position, transform.rotation);
        }

        //Destroy the object if life is lower than 0
        if (lifeScript.GetLife() <= 0)
        {
            particlesContainer.transform.SetParent(null);
            Destroy(gameObject, 5);
            transform.position = Vector2.MoveTowards(transform.position, transform.up*500, Time.deltaTime);
            GetComponent<BoxCollider2D>().enabled = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(60, Vector3.forward), 1 * Time.deltaTime);
            if (!defeated) {
                SoundManager.instance.Play(SoundManager.clip.bossHit);
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
        GetComponent<PolygonCollider2D>().enabled = false;
        defeated = true;
        for (float i = 0; i < 1.1f; i += 0.1f) {
            gameObject.transform.Find("EnemySprite").GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), i);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.transform.Find("EnemySprite").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}


