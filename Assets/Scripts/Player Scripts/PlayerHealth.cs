using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int MaxHP = 100;
    [Header("Health References:")]
    [SerializeField] CinemachineVirtualCamera gameOverCamera;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] PlayerHealthBar healthBar;
    int HP;
    bool isAlive;
    bool _isGameOver;

    private void Start()
    {
        HP = MaxHP;
        isAlive = true;
        _isGameOver = false;
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
        if (!isAlive)
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
        isAlive = false;
        _isGameOver = true;
        gameOverCamera.Priority = 10;
        gameOverUI.SetActive(true);

        Invoke(nameof(RestartLevel), 5f);
    }

    private void RestartLevel()
    {
        MainManager.Instance.RestartLevel();
    }

    private void UpdateHealthBarUI()
    {
        //healthBar.UpdateHealth(HP, MaxHP);
    }

    
}
