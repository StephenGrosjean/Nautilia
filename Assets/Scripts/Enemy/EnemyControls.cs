using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] private bool delete; //Destroy the enemy (Used for testing)
    [SerializeField] private GameObject[] powerupsDrop;

    [Range(-100, 100)] [SerializeField] private int rotationSpeed; //Rotation speed of the enemy


    [SerializeField] private GameObject upgradeObject;

    private EnemySpawnSystem enemySpawnSystem;
    private EnemyLife lifeScript;
    private bool isBlinking;
    private SpriteRenderer spriteRendererComponent;

    void Start()
    {
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
        lifeScript = GetComponent<EnemyLife>(); //Get life script 
        enemySpawnSystem =
            GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnSystem>(); //Find the enemySpawnSystem
    }

    void Update()
    {
        int upgradeDropRate = Random.Range(1, 100);
        //Delete the object (Used for testing)
        if (delete)
        {
            Destroy(gameObject);
        }

        //Destroy the object if life is lower than 0
        if (lifeScript.GetLife() <= 0)
        {
            if (upgradeDropRate <= 10)
            {
                Instantiate(upgradeObject, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        //Rotate the object 
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    //Remove the object from the List in enemySpawnSystem at object destroy
    private void OnDestroy()
    {
        if (powerupsDrop.Length > 0)
        {
            Instantiate(powerupsDrop[Random.Range(0, powerupsDrop.Length)], transform.position, Quaternion.identity);
        }

        enemySpawnSystem.CurrentEnemies.Remove(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check the collision with a player bullet
        if (collision.CompareTag("PlayerBullet"))
        {
            lifeScript.DecreaseLife(1); //Decrease life

            //Blink the object
            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine("Blink");
            }
        }
    }


    //Blink Ienumerator (Need optimisation)
    IEnumerator Blink()
    {
        float time = 0.02f;
        spriteRendererComponent.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        spriteRendererComponent.color = new Color(1, 1, 1);
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }
}
