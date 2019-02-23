using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    //Zones enum
    public enum zone { Top_Left, Top_Center, Top_Right, Middle_Left, Middle_Center, Middle_Right, Bottom_Left, Bottom_Center, Bottom_Right };
    //Enemies enum
    public enum enemies { Flower_4, Flower_8, Rotating_1, Rotating_2, Rotating_3, Burst_1_5_Down, Burst_2_5_Down, Burst_12_3_Omni};

    [SerializeField] private GameObject[] enemiesObject;
    [SerializeField] private Transform[] zones; //Zones transform
    [SerializeField] private Wave[] waves; //Waves array

    //Wave setup class
    [System.Serializable]
    class Wave {
        public enemies toSpawn;
        public zone zoneToSpawn;
        public bool waitUntilDestruction;
    }
    
    //List of enemies
    private List<GameObject> currentEnemies = new List<GameObject>();
    public List<GameObject> CurrentEnemies{
        get { return currentEnemies; }
        set { currentEnemies = value; }
    }

    //Enemy container
    private Transform enemyContainer;

    
    void Start()
    {
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer").transform; //Get the enemy container transform
        StartCoroutine("SpawnWaves"); //Start the wave spawn
    }

    IEnumerator SpawnWaves() {
        foreach (Wave wave in waves) {

            GameObject enemy = Instantiate(GetEnemy(wave.toSpawn), GetSpawnPosition(wave.zoneToSpawn).position, Quaternion.identity); //Instantiate enemy in correct zone
            enemy.transform.parent = GetSpawnPosition(wave.zoneToSpawn); //Put the enemy in the zone transform
            enemy.transform.position = Vector3.zero; //Set the enemy position to the zone transform position
            currentEnemies.Add(enemy); //Add the enemy to the list

            //If the wave need to wait before spawning the next wave
            if (wave.waitUntilDestruction) {
                while (currentEnemies.Count > 0) { //Check if all the enemies are destroyed
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    GameObject GetEnemy(enemies enemy) {
        switch (enemy) {
            case enemies.Flower_4:
                return enemiesObject[0];
                break;
            case enemies.Flower_8:
                return enemiesObject[1];
                break;
            case enemies.Rotating_1:
                return enemiesObject[2];
                break;
            case enemies.Rotating_2:
                return enemiesObject[3];
                break;
            case enemies.Rotating_3:
                return enemiesObject[4];
                break;
            case enemies.Burst_1_5_Down:
                return enemiesObject[5];
                break;
            case enemies.Burst_2_5_Down:
                return enemiesObject[6];
                break;
            case enemies.Burst_12_3_Omni:
                return enemiesObject[7];
                break;
        }
        return null;
    }

    //Get all spawn positions
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
