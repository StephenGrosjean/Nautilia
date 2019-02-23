using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public enum PlayerUpgrade { FirstUpgrade, SecondUpgrade, ThirdUpgrade, FourthUpgrade, FifthUpgrade }

    private Collider2D hitColliders;
    #region Player variable
    [SerializeField] private float collisionRadius;
    [SerializeField] private GameObject firstUpgradePoint; 
    [SerializeField] private GameObject secondUpgradePoint;
    [SerializeField] private GameObject thirdUpgradePoint;
    [SerializeField] private GameObject fourthUpgradePoint;
    [SerializeField] private GameObject fifthUpgradePoint;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private float maxInvincibilityTime = 1.0f;
    [SerializeField] private float deathAnimation = 1.0f;
    [SerializeField] private GameObject hitCollider;

    private bool _isInvincible = false;
    public bool IsInvincible {
        get { return _isInvincible; }
    }
    public PlayerUpgrade _playerUpgrade;
    private PlayerLife _playerScript;
    #endregion

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    private void Start()
    {
        _playerScript = GetComponent<PlayerLife>();
        ShootingMode();
    }

    private void Update()
    {
        StartCoroutine(Die(deathAnimation));
    }

    private void FixedUpdate() {
       /* hitColliders = Physics2D.OverlapCircle(transform.position, collisionRadius);
        if (hitColliders != null && hitColliders.CompareTag("Bullet") && !_isInvincible) {
            StartCoroutine(InvincibilityBlink(maxInvincibilityTime));
            _playerScript.DecreaseLife(1);
        }*/
    }

    public void Hit() {
        if (!_isInvincible) {
            StartCoroutine(InvincibilityBlink(maxInvincibilityTime));
            _playerScript.DecreaseLife(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {

            ScoreManager.AddScore(10000);
            Debug.Log("UPGRADE");
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
    
    private IEnumerator InvincibilityBlink(float time)
    {
        _isInvincible = true;
        hitCollider.SetActive(false);
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
        hitCollider.SetActive(true);

        _isInvincible = false;
    }
}