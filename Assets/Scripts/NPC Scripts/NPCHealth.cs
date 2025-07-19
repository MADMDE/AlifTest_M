using System;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] int MaxHP = 100;
    [Header("Health References:")]
    [SerializeField] GameObject vfxDestroyPrefab;
    [SerializeField] NPCHealthBar healthBar;

    int HP;
    bool _isAlive;

    private void Start()
    {
        HP = MaxHP;
        _isAlive = true;

        UpdateHealthBarUI();
    }

    [ContextMenu(nameof(Test_TakeDamage))]
    public void Test_TakeDamage()
    {
        TakeDamage(10);
    }
    public void TakeDamage(int damage)
    {
        if (!_isAlive)
            return;

        HP -= damage;

        if(HP <= 0)
        {
            SelfDestruct();
        }

        UpdateHealthBarUI();
    }

    public void SelfDestruct()
    {
        _isAlive = false;
        //Instantiate(vfxDestroyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateHealthBarUI()
    {
        healthBar.UpdateHealth(HP, MaxHP);
    }
}

