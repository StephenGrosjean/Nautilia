using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] private bool delete; //Destroy the enemy (Used for testing)

    [Range(-100, 100)]
    [SerializeField] private int rotationSpeed; //Rotation speed of the enemy

    private EnemySpawnSystem enemySpawnSystem; 
    private EnemyLife lifeScript;
    private bool isBlinking;


    void Start()
    {
        lifeScript = GetComponent<EnemyLife>(); //Get life script 
        enemySpawnSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnSystem>(); //Find the enemySpawnSystem
    }

    void Update()
    {
        //Delete the object (Used for testing)
        if (delete) {
            Destroy(gameObject);
        }

        //Destroy the object if life is lower than 0
        if(lifeScript.GetLife() <= 0) {
            Destroy(gameObject);
        }

        //Rotate the object 
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    //Remove the object from the List in enemySpawnSystem at object destroy
    private void OnDestroy() {
        enemySpawnSystem.CurrentEnemies.Remove(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Check the collision with a player bullet
        if (collision.CompareTag("PlayerBullet")) {
            lifeScript.DecreaseLife(1); //Decrease life

            //Blink the object
            if (!isBlinking) {
                isBlinking = true;
                StartCoroutine("Blink");
            }
        }
    }


    //Blink Ienumerator (Need optimisation)
    IEnumerator Blink() {
        float time = 0.02f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }
}
