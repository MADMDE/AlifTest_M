using System;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] int MaxHP = 100;
    [Header("Health References:")]
    [SerializeField] GameObject vfxDestroyPrefab;
    [SerializeField] NPCHealthBar healthBar;
    int HP;
    bool isAlive;

    private void Start()
    {
        HP = MaxHP;
        isAlive = true;

        UpdateHealthBarUI();
    }

    [ContextMenu(nameof(Test_TakeDamage))]
    public void Test_TakeDamage()
    {
        TakeDamage(10);
    }
    public void TakeDamage(int damage)
    {
        if (!isAlive)
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
        isAlive = false;
        //Instantiate(vfxDestroyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void UpdateHealthBarUI()
    {
        healthBar.UpdateHealth(HP, MaxHP);
    }
}

