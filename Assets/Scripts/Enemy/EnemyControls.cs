using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [Range(-100, 100)]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private bool delete;

    private EnemySpawnSystem enemySpawnSystem;

    void Start()
    {
        enemySpawnSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnSystem>();
    }

    void Update()
    {
        if (delete) {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    private void OnDestroy() {
        enemySpawnSystem.CurrentEnemies.Remove(gameObject);
    }
}
