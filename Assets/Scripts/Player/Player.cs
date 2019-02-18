using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
    [SerializeField] private GameObject playerSprite;
   
    public int playerLife = 3;
    
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private float maxInvincibilityTime = 1.0f;
    [SerializeField] private float deathAnimation = 1.0f;
    
    private bool _isInvincible = false;
    private PlayerUpgrade _playerUpgrade;
    
#endregion

    private void Start()
    {
        ShootingMode();
        if (blinkInterval > maxInvincibilityTime)
        {
            blinkInterval = maxInvincibilityTime;
        }
    }

    private void Update()
    {
        StartCoroutine(Die(deathAnimation));
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            ScoreManager.scoreValue += 1000;
            _playerUpgrade++;
            ShootingMode();
        }

        if (col.CompareTag("Bullet") && !_isInvincible)
        {
            StartCoroutine(InvincibilityBlink(maxInvincibilityTime));
            playerLife -= 1;
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
        if (playerLife == 0)
        {
            yield return new WaitForSeconds(time);
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }

    private IEnumerator InvincibilityBlink(float time)
    {
        _isInvincible = true;

        for (float i = 0; i < time; i += blinkInterval)
        {
            if (playerSprite.activeInHierarchy)
            {
                playerSprite.SetActive(false);
            }
            else
            {
                playerSprite.SetActive(true);
            }
            
            yield return new WaitForSeconds(blinkInterval);
        }
        
        playerSprite.SetActive(true);

        _isInvincible = false;
    }
}
