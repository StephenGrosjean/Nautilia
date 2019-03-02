using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public enum stage { Left, Right, Head};

    [SerializeField] private int globalLife;
    [SerializeField] private Image lifeImage;
    [SerializeField] private EnemyLife leftArm, rightArm, head;
    [SerializeField] private List<GameObject> firstSequenceObj, secondSequenceObj, thirdSequenceObj;

    private int maxLife;
    private bool leftArm_Destroyed, rightArm_Destroyed;
    private stage currentStage;
    private int leftLife, rightLife, headLife;

    void Start()
    {
        currentStage = stage.Left;
        GetLifes();
        SwitchPhase();
        maxLife = leftLife + rightLife + headLife;
        InvokeRepeating("UpdateLife", 0, 0.1f);
    }

    void Update() {
        GetLifes();
        switch (currentStage) {
            case stage.Left:
                if (leftArm.GetLife() <= 0) {
                    leftArm_Destroyed = true;
                    currentStage = stage.Right;
                    SwitchPhase();
                }
                break;
            case stage.Right:
                if(rightArm.GetLife() <= 0) {
                    rightArm_Destroyed = true;
                    currentStage = stage.Head;
                    SwitchPhase();
                }
                break;
         }
    }

    void GetLifes() {
        rightLife = rightArm.GetLife();
        leftLife = leftArm.GetLife();
        headLife = head.GetLife();
    }

    void UpdateLife() {
        switch (currentStage) {
            case stage.Left:
                globalLife = leftLife + rightLife + headLife;
                break;
            case stage.Right:
                globalLife = rightLife + headLife;
                break;
            case stage.Head:
                globalLife = headLife;
                break;
        }
        float percentage = ((globalLife*100) / maxLife)/100f;
        lifeImage.fillAmount = percentage;
    }

    void SwitchPhase() {
        switch (currentStage) {
            case stage.Left:
                rightArm.IsImortal = true;
                head.IsImortal = true;
                EnableObjects(firstSequenceObj);
                break;
            case stage.Right:
                rightArm.IsImortal = false;
                head.IsImortal = true;
                DetatchObjects(firstSequenceObj);
                EnableObjects(secondSequenceObj);
                break;
            case stage.Head:
                head.IsImortal = false;
                DetatchObjects(secondSequenceObj);
                EnableObjects(thirdSequenceObj);
                break;
        }
    }

    void EnableObjects(List<GameObject> toActivate) {
        foreach(GameObject obj in toActivate) {
            obj.SetActive(true);
        }
    }

    void DetatchObjects(List<GameObject> toActivate) {
        foreach (GameObject obj in toActivate) {
            if (obj != null) {
                obj.transform.parent = null;
                obj.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
