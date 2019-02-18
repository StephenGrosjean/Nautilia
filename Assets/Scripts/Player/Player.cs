using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
#region Player variable
    [SerializeField] private GameObject doubleShotPoint; 
    [SerializeField] private GameObject quadShotPoint;
    [SerializeField] private GameObject singleShotPoint;
    [SerializeField] private GameObject playerSprite;
   
    public int playerLife = 3;
    public int shootMode = 0;
    
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private float maxInvincibilityTime = 1.0f;
    [SerializeField] private float deathAnimation = 1.0f;
    
    private bool _isInvincible = false;
    
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

    private void DoubleShotUpgrade()
    {
        doubleShotPoint.SetActive(true);
        
        singleShotPoint.SetActive(false);
    }
    
    private void QuadShotUpgrade()
    {
        doubleShotPoint.SetActive(false);
        quadShotPoint.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            ScoreManager.scoreValue += 1000;
            shootMode++;
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
        if (shootMode == 1)
        {
            DoubleShotUpgrade();
        }
        else if(shootMode == 2)
        {
            QuadShotUpgrade();
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
