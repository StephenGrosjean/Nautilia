﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public enum PlayerUpgrade { FirstUpgrade, SecondUpgrade, ThirdUpgrade, FourthUpgrade, FifthUpgrade }
    
    
#region Player variable
    [SerializeField] private GameObject firstUpgradePoint; 
    [SerializeField] private GameObject secondUpgradePoint;
    [SerializeField] private GameObject thirdUpgradePoint;
    [SerializeField] private GameObject fourthUpgradePoint;
    [SerializeField] private GameObject fifthUpgradePoint;
    
    [SerializeField] private float deathAnimation = 1.0f;
    
    private PlayerUpgrade _playerUpgrade;
    private PlayerLife _playerScript;
    private PlayerTakeDamage _playerDamaged;
#endregion

    private void Start()
    {
        ShootingMode();
    }

    private void Update()
    {
        StartCoroutine(Die(deathAnimation));
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            ScoreManager.AddScore(10000);
            _playerUpgrade++;
            ShootingMode();
        }
    }

    private void ShootingMode()
    {
        switch (_playerUpgrade)
        {
            case PlayerUpgrade.FirstUpgrade:
                firstUpgradePoint.SetActive(true);
                break;

            case PlayerUpgrade.SecondUpgrade:
                firstUpgradePoint.SetActive(false);
                secondUpgradePoint.SetActive(true);
                break;

            case PlayerUpgrade.ThirdUpgrade:
                secondUpgradePoint.SetActive(false);
                thirdUpgradePoint.SetActive(true);
                break;

            case PlayerUpgrade.FourthUpgrade:
                thirdUpgradePoint.SetActive(false);
                fourthUpgradePoint.SetActive(true);
                break;

            case PlayerUpgrade.FifthUpgrade:
                fourthUpgradePoint.SetActive(false);
                fifthUpgradePoint.SetActive(true);
                break;
        }
    }

    private IEnumerator Die(float time)
    {
        if (_playerScript.GetLife() == 0)
        {
            yield return new WaitForSeconds(time);
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }
}
