using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameManager : MonoBehaviour
{
   
    [SerializeField] private GameObject bossIntro, bossAppear, bossReal;
    [SerializeField] private float waitBetweenPhases;

    public enum phases { Intro, Standby, BossStart};
    private phases currentPhase;
    private AudioSource source;

    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
        currentPhase = phases.Intro;
        StartCoroutine(Coordinator());
    }

    IEnumerator Coordinator()
    {
        switch (currentPhase) {
            case phases.Intro:
                Debug.Log("Intro");
                bossIntro.SetActive(true);
                yield return new WaitForSeconds(waitBetweenPhases);
                SwitchPhase();
                break;
            case phases.Standby:
                Debug.Log("Appear");
                source.Play();
                bossIntro.SetActive(false);
                bossAppear.SetActive(true);
                yield return new WaitForSeconds(waitBetweenPhases);
                SwitchPhase();
                break;
            case phases.BossStart:
                Debug.Log("Start");
                bossAppear.SetActive(false);
                bossReal.SetActive(true);
                yield return new WaitForSeconds(waitBetweenPhases);
                break;


        }
    }

    void SwitchPhase() {
        switch (currentPhase) {
            case phases.Intro:
                currentPhase = phases.Standby;
                StartCoroutine("Coordinator");
                break;
            case phases.Standby:
                currentPhase = phases.BossStart;
                StartCoroutine("Coordinator");
                break;
        }
    }

}
