using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private float maxInvincibilityTime = 1.0f;
    
    private bool _isInvincible = false;
    private PlayerLife _playerScript;

    private void Start()
    {
        if (blinkInterval > maxInvincibilityTime)
        {
            blinkInterval = maxInvincibilityTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet") && !_isInvincible)
        { 
            StartCoroutine(InvincibilityBlink(maxInvincibilityTime));
            _playerScript.DecreaseLife(1);
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
