using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [Range(-100, 100)]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private bool delete;

    private EnemySpawnSystem enemySpawnSystem;
    private bool isBlinking;
    private EnemyLife lifeScript;

    void Start()
    {
        lifeScript = GetComponent<EnemyLife>();
        enemySpawnSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnSystem>();
    }

    void Update()
    {
        if (delete) {
            Destroy(gameObject);
        }
        if(lifeScript.GetLife() <= 0) {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    private void OnDestroy() {
        enemySpawnSystem.CurrentEnemies.Remove(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PlayerBullet")) {
            lifeScript.DecreaseLife(1);
            if (!isBlinking) {
                isBlinking = true;
                StartCoroutine("Blink");
            }
        }
    }

    IEnumerator Blink() {
        float time = 0.02f;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSeconds(time);
        isBlinking = false;

    }
}
