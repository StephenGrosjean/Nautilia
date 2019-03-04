using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnSystem : MonoBehaviour
{
    //Zones enum
    public enum zone { Top_Left, Top_Center, Top_Right, Middle_Left, Middle_Center, Middle_Right, Bottom_Left, Bottom_Center, Bottom_Right };
    //Enemies enum
    public enum enemies {
        Flower_4,
        Flower_8,
        Flower_12,
        Flower_16,
        Rotating_1,
        Rotating_2,
        Rotating_3,
        Rotating_4,
        Rotating_5,
        Rotating_6,
        Burst_1_5_Down,
        Burst_2_5_Down,
        Burst_3_5_Down,
        Burst_4_5_Down,
        Burst_5_5_Down,
        Burst_5_Wait_Omni,
        Burst_10_Wait_Omni,
        Burst_20_Wait_Omni,
        Burst_30_Wait_Omni,
        Burst_12_3_Omni
    };
    [SerializeField] private float timeBeforeStart;
    [SerializeField] private Image bar;
    [SerializeField] private XMLSave saveSystem;
    [SerializeField] private int levelID;
    [SerializeField] private GameObject[] enemiesObject;
    [SerializeField] private Transform[] zones; //Zones transform
    [SerializeField] private Wave[] waves; //Waves array


    //Wave setup class
    [System.Serializable]
    class Wave {
        public enemies toSpawn;
        public zone zoneToSpawn;
        public bool waitUntilDestruction;
        public bool spawnUpgrade;
        public float timeBeforeSpawnNext;
        //public bool isEndLevel; //to set the en of the level
    }
    
    //List of enemies
    private List<GameObject> currentEnemies = new List<GameObject>();
    public List<GameObject> CurrentEnemies{
        get { return currentEnemies; }
        set { currentEnemies = value; }
    }

    //Enemy container
    private Transform enemyContainer;

    //Level Variables
    private float percentage, currentPercentage;
    private float currentWave, totalWaves;
    
    void Start()
    {
        enemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer").transform; //Get the enemy container transform
        StartCoroutine("SpawnWaves"); //Start the wave spawn
        totalWaves = waves.Length;
    }

    private void Update() {
        if(currentPercentage != percentage) {
            currentPercentage = Mathf.Lerp(currentPercentage, percentage, Time.deltaTime);
            bar.fillAmount = currentPercentage;
        }
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(timeBeforeStart);
        foreach (Wave wave in waves) {

            GameObject enemy = Instantiate(GetEnemy(wave.toSpawn), GetSpawnPosition(wave.zoneToSpawn).position, Quaternion.identity); //Instantiate enemy in correct zone
            enemy.GetComponent<EnemyControls>().DropUpgrade = wave.spawnUpgrade;
            enemy.transform.parent = GetSpawnPosition(wave.zoneToSpawn); //Put the enemy in the zone transform
            enemy.transform.position = Vector3.zero; //Set the enemy position to the zone transform position
            currentEnemies.Add(enemy); //Add the enemy to the list
            enemy.tag = "Enemy"; //Set the tag of the enemies
            //If the wave need to wait before spawning the next wave
            if (wave.waitUntilDestruction) {
                while (currentEnemies.Count > 0) { //Check if all the enemies are destroyed
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(wave.timeBeforeSpawnNext);
            DrawBar();
        }

        while (currentEnemies.Count > 0) {
            yield return new WaitForSeconds(0.1f);
        }

        //Save level
        saveSystem.Load();
        if (saveSystem.dataBase.firstDB[0].value < levelID) {
            saveSystem.dataBase.firstDB[0].value = levelID;
            saveSystem.Save();
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
            case enemies.Flower_12:
                return enemiesObject[2];
                break;
            case enemies.Flower_16:
                return enemiesObject[3];
                break;
            case enemies.Rotating_1:
                return enemiesObject[4];
                break;
            case enemies.Rotating_2:
                return enemiesObject[5];
                break;
            case enemies.Rotating_3:
                return enemiesObject[6];
                break;
            case enemies.Rotating_4:
                return enemiesObject[7];
                break;
            case enemies.Rotating_5:
                return enemiesObject[8];
                break;
            case enemies.Rotating_6:
                return enemiesObject[9];
                break;
            case enemies.Burst_1_5_Down:
                return enemiesObject[10];
                break;
            case enemies.Burst_2_5_Down:
                return enemiesObject[11];
                break;
            case enemies.Burst_3_5_Down:
                return enemiesObject[12];
                break;
            case enemies.Burst_4_5_Down:
                return enemiesObject[13];
                break;
            case enemies.Burst_5_5_Down:
                return enemiesObject[14];
                break;
            case enemies.Burst_5_Wait_Omni:
                return enemiesObject[15];
                break;
            case enemies.Burst_10_Wait_Omni:
                return enemiesObject[16];
                break;
            case enemies.Burst_20_Wait_Omni:
                return enemiesObject[17];
                break;
            case enemies.Burst_30_Wait_Omni:
                return enemiesObject[18];
                break;
            case enemies.Burst_12_3_Omni:
                return enemiesObject[19];
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

    void DrawBar() {
        currentWave++;
        percentage = currentWave / totalWaves;
    }
}
