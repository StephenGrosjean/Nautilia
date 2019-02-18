using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] private int bulletAmount;
    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> bullets;
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletAmount; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            //obj.GetComponent<SpriteRenderer>().sortingOrder = i;
            bullets.Add(obj);
        }
    }

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
