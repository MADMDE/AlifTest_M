using UnityEngine;
using Cinemachine;
using StarterAssets;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int MaxHP = 100;
    [Header("Health References:")]
    [SerializeField] CinemachineVirtualCamera gameOverCamera;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] PlayerHealthBar healthBar;

    int HP;
    bool _isAlive;
    bool _isGameOver;
    FirstPersonController _firstPersonController;

    private void Start()
    {
        HP = MaxHP;
        _isAlive = true;
        _isGameOver = false;
        _firstPersonController = GetComponent<FirstPersonController>();
        gameOverUI.SetActive(false);

        UpdateHealthBarUI();
    }

    [ContextMenu(nameof(Test_TakeDamage))]
    public void Test_TakeDamage()
    {
        TakeDamage(30);
    }
    public void TakeDamage(int damage)
    {
        if (!_isAlive)
            return;

        HP -= damage;

        if (HP <= 0 && !_isGameOver)
        {
            GameOver();
        }

        UpdateHealthBarUI();
    }

    private void GameOver()
    {
        _isAlive = false;
        _isGameOver = true;
        gameOverCamera.Priority = 10;
        gameOverUI.SetActive(true);
        _firstPersonController.gameObject.SetActive(false);

        Invoke(nameof(RestartLevel), 5f);
    }

    private void RestartLevel()
    {
        MainManager.Instance.RestartLevel();
    }

    private void UpdateHealthBarUI()
    {
        healthBar.UpdateHealth(HP, MaxHP);
    }

    
}
