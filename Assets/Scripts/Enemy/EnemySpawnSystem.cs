using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private Transform[] zones;
    [SerializeField] private Wave[] waves;

    [System.Serializable]
    class Wave {
        public GameObject toSpawn;
        public zone zoneToSpawn;
        public bool waitUntilDestruction;
    }
    public enum zone { Top_Left, Top_Center, Top_Right, Middle_Left, Middle_Center, Middle_Right, Bottom_Left, Bottom_Center, Bottom_Right };
    

    private List<GameObject> currentEnemies = new List<GameObject>();
    public List<GameObject> CurrentEnemies{
        get { return currentEnemies; }
        set { currentEnemies = value; }
    }

    private Transform enemyContainer;

    void Start()
    {
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer").transform;
        StartCoroutine("SpawnWaves");
    }

    private void Update() {
    }

    IEnumerator SpawnWaves() {
        foreach (Wave wave in waves) {
            GameObject enemy = Instantiate(wave.toSpawn, GetSpawnPosition(wave.zoneToSpawn).position, Quaternion.identity);
            enemy.transform.parent = enemyContainer;
            currentEnemies.Add(enemy);
            if (wave.waitUntilDestruction) {
                while (currentEnemies.Count > 0) {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    Transform GetSpawnPosition(zone zone) {
        switch (zone) {
            case zone.Top_Left:
                return zones[0];
                break;
            case zone.Top_Center:
                return zones[1];
                break;
            case zone.Top_Right:
                return zones[2];
                break;
            case zone.Middle_Left:
                return zones[3];
                break;
            case zone.Middle_Center:
                return zones[4];
                break;
            case zone.Middle_Right:
                return zones[5];
                break;
            case zone.Bottom_Left:
                return zones[6];
                break;
            case zone.Bottom_Center:
                return zones[7];
                break;
            case zone.Bottom_Right:
                return zones[8];
                break;
        }

        return null;
       
    }
}
