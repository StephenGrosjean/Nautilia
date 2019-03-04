using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public enum PlayerUpgrade { FirstUpgrade, SecondUpgrade, ThirdUpgrade, FourthUpgrade, FifthUpgrade }

    private Collider2D hitColliders;
    #region Player variable
    [SerializeField] private GameObject firstUpgradePoint; 
    [SerializeField] private GameObject secondUpgradePoint;
    [SerializeField] private GameObject thirdUpgradePoint;
    [SerializeField] private GameObject fourthUpgradePoint;
    [SerializeField] private GameObject fifthUpgradePoint;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject hitCollider;
    [SerializeField] private float blinkInterval = 0.2f;
    [SerializeField] private float maxInvincibilityTime = 1.0f;
    [SerializeField] private float deathAnimation = 1.0f;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private bool _isInvincible = false;
    public bool IsInvincible {
        get { return _isInvincible; }
    }

    private EnemyControls _enemyScript;
    private CameraShake cameraShake;
    private PlayerUpgrade _playerUpgrade;
    private PlayerLife _playerScript;
    private bool isBlinking;
    #endregion

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        _enemyScript = GetComponent<EnemyControls>();
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
            Handheld.Vibrate();
            SoundManager.instance.Play(SoundManager.clip.PlayerHit);
            StartCoroutine(cameraShake.DoShake(0.1f,0.3f));
            StartCoroutine(InvincibilityBlink(maxInvincibilityTime));
            _playerScript.DecreaseLife(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Upgrade"))
        {
            Debug.Log("Upgrade");
            ScoreManager.AddScore(10000);
            _playerUpgrade++;
            ShootingMode();
            StartCoroutine(Blink());
            //_enemyScript.upgradeDropRate = 1;
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

    private IEnumerator Blink()
    {
        float time = 0.3f;
        for (int i = 0; i <= 1; i++) {
            _playerSpriteRenderer.color = new Color(0, 1, 0);
            yield return new WaitForSeconds(time);
            _playerSpriteRenderer.color = new Color(1, 1, 1);
            yield return new WaitForSeconds(time);
        }
        isBlinking = false;
    }
}