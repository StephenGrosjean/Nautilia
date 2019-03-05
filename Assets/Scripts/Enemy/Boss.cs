using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
/// <summary>
/// Boss Script
/// </summary>
public class Boss : MonoBehaviour
{
    public enum stage { Left, Right, Head}; //Different stages

    [SerializeField] private int globalLife, timeBeforeNextPhase;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private AudioMixerSnapshot pauseSnap;
    [SerializeField] private Image lifeImage;
    [SerializeField] private EnemyLife leftArm, rightArm, head;
    [SerializeField] private List<GameObject> firstSequenceObj, secondSequenceObj, thirdSequenceObj;

    private int maxLife;
    private bool leftArm_Destroyed, rightArm_Destroyed;
    private stage currentStage; //Current stage
    private int leftLife, rightLife, headLife;
    private float difficulty;

    void Start()
    {
        currentStage = stage.Left; 

        StartCoroutine(SwitchPhase());
        maxLife = leftLife + rightLife + headLife;
        InvokeRepeating("UpdateLife", 0, 0.1f);
        GetLifes();
    }

    void Update() {
        GetLifes();
        //Check current stage
        switch (currentStage) {
            case stage.Left:
                if (leftArm.GetLife() <= 0) {
                    leftArm_Destroyed = true;
                    currentStage = stage.Right;
                    StartCoroutine(SwitchPhase());
                }
                break;
            case stage.Right:
                if(rightArm.GetLife() <= 0) {
                    rightArm_Destroyed = true;
                    currentStage = stage.Head;
                    StartCoroutine(SwitchPhase());
                }
                break;
            case stage.Head:
                if (head.GetLife() <= 0) {
                    DetatchObjects(thirdSequenceObj);
                    StartCoroutine("WinSequence");
                    pauseSnap.TransitionTo(0.2f);
                }
                break;
        }
    }

    //Set lifes variables
    void GetLifes() {
        rightLife = rightArm.GetLife();
        leftLife = leftArm.GetLife();
        headLife = head.GetLife();
    }

    //Update the life
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

    //Switch phases
    IEnumerator SwitchPhase() {
        switch (currentStage) {
            case stage.Left:
                rightArm.IsImortal = true;
                head.IsImortal = true;
                EnableObjects(firstSequenceObj);
                break;
            case stage.Right:
                Shake();
                head.IsImortal = true;
                DetatchObjects(firstSequenceObj);
                yield return new WaitForSeconds(timeBeforeNextPhase);
                rightArm.IsImortal = false;
                EnableObjects(secondSequenceObj);
                break;
            case stage.Head:
                Shake();
                DetatchObjects(secondSequenceObj);
                yield return new WaitForSeconds(timeBeforeNextPhase);
                head.IsImortal = false;
                EnableObjects(thirdSequenceObj);
                break;
        }
        
    }

    //Enable objects in list
    void EnableObjects(List<GameObject> toActivate) {
        foreach(GameObject obj in toActivate) {
            obj.SetActive(true);
        }
    }

    //Detatch object from it's parent
    void DetatchObjects(List<GameObject> toActivate) {
        foreach (GameObject obj in toActivate) {
            if (obj != null) {
                obj.transform.parent = null;
                obj.GetComponent<ParticleSystem>().Stop();
            }
        }
        
    }

    //Shake the screen
    void Shake() {
        StartCoroutine(Camera.main.GetComponent<CameraShake>().DoShake(0.02f, 3));
    }

    //Win Sequence
    IEnumerator WinSequence() {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0; //Stop time
        winScreen.SetActive(true); //Activate winScreen
        XMLSave.instance.Load(); //Load the save
        difficulty = XMLSave.instance.dataBase.firstDB[7].value; //Load current difficulty
        XMLSave.instance.dataBase.firstDB[8 + (int)difficulty].value = 1; //Save level done
        XMLSave.instance.Save(); //Save all changes
    }
}
