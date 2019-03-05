using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Object pooler (Used for bullets before the particle upgrade)
/// </summary>
public class BulletPooler : MonoBehaviour
{
    [SerializeField] private int bulletAmount;
    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> bullets;

    void Start()
    {
        //Create pool bullet
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletAmount; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bullets.Add(obj);
        }
    }

    //Get active bullet from list
    public GameObject GetBullet() {
        foreach(GameObject obj in bullets) {
            if (!obj.activeInHierarchy) {
                return obj;
                break;
            }
        }
        return null;
    }
}
