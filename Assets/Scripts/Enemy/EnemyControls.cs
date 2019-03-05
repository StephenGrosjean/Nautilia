using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupsDrop;
    [SerializeField] private GameObject particlesContainer;
    [SerializeField] private GameObject upgradeObject, rotateObject;
    [Space(10)]
    [Range(-100, 100)] [SerializeField] private float rotationSpeed;
    [SerializeField] private int incrementRotation;
    [Space(10)]
    [SerializeField] private int numberOfPoints;
    [SerializeField] private float pointSpawnRadius;
    [SerializeField] private Color blinkColor = Color.red;

    private EnemySpawnSystem enemySpawnSystem;
    private EnemyLife lifeScript;
    private bool isBlinking;
    private SpriteRenderer spriteRendererComponent;
    private BulletPooler poolPoint;

    private bool dropUpgrade;
    public bool DropUpgrade {
        set { dropUpgrade = value; }
    }


    void Start()
    {
        poolPoint = GameObject.FindGameObjectWithTag("PoolPoint").GetComponent<BulletPooler>(); //Point pooler
        StartCoroutine("AddRotation"); //Add rotation
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
        lifeScript = GetComponent<EnemyLife>(); //Get life script 
        enemySpawnSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnSystem>(); //Find the enemySpawnSystem
    }

    public void Hit() {
        lifeScript.DecreaseLife(1); //Decrease life
        if (!isBlinking) {
            isBlinking = true;
            StartCoroutine("Blink");
        }
    }
    
    void FixedUpdate()
    {
        //Destroy the object if life is lower than 0
        if (lifeScript.GetLife() <= 0)
        {
            for (int i = 0; i < numberOfPoints; i++) {
                GameObject point = poolPoint.GetBullet();
                point.transform.position = new Vector2(Random.Range(transform.position.x- pointSpawnRadius, transform.position.x+ pointSpawnRadius), Random.Range(transform.position.y - pointSpawnRadius, transform.position.y + pointSpawnRadius));
                point.SetActive(true);
            }

            particlesContainer.transform.SetParent(null);
            SoundManager.instance.Play(SoundManager.clip.enemyDeath);
            if (!Application.isEditor) {
                VibrationController.Vibrate(50);
            }
            if (dropUpgrade) {
                Instantiate(upgradeObject, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        //Rotate the object 
        rotateObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

//Remove the object from the List in enemySpawnSystem at object destroy
private void OnDestroy()
    {
        if (powerupsDrop.Length > 0)
        {
            Instantiate(powerupsDrop[Random.Range(0, powerupsDrop.Length)], transform.position, Quaternion.identity);
        }

        enemySpawnSystem.CurrentEnemies.Remove(gameObject); //Remove enemy from array
    }

    //Blink Ienumerator (Need optimisation)
    IEnumerator Blink() {
        float time = 0.03f;
        spriteRendererComponent.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        spriteRendererComponent.color = Color.white;
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }

    //Rotate
    IEnumerator AddRotation() {
        while (true) {
            rotationSpeed += incrementRotation;
            yield return new WaitForSeconds(0.1f);
        }
    }
}


