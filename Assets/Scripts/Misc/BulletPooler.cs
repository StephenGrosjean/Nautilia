using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler current;
    [SerializeField] private int bulletAmount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform offPos;
    public Transform OffPos {
        get { return offPos; }
    }

    private List<GameObject> bullets;

    private void Awake() {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        for(int i = 0; i < bulletAmount; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            obj.GetComponent<SpriteRenderer>().sortingOrder = i;
            bullets.Add(obj);
        }
    }

    public GameObject GetBullet() {
        foreach(GameObject obj in bullets) {
            if (obj.GetComponent<BulletBehaviour>().CanBePulled) {
                obj.GetComponent<BulletBehaviour>().CanBePulled = false;
                return obj;
                break;
            }
        }
        return null;
    }

    public void MoveOff(GameObject obj) {
        obj.transform.position = offPos.position;
    }

    
}
