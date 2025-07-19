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
    Animator _animator;
    NPCContext _context;

    private void Start()
    {
        HP = MaxHP;
        _isAlive = true;
        _animator = GetComponent<Animator>();
        _context = GetComponent<NPCContext>();

        UpdateHealthBarUI();
    }

    [ContextMenu(nameof(Test_TakeDamage))]
    public void Test_TakeDamage()
    {
        TakeDamage(100);
    }
    public void TakeDamage(int damage)
    {
        if (!_isAlive)
            return;

        HP -= damage;

        if(HP <= 0)
        {
            Death();
        }

        UpdateHealthBarUI();
    }

    public void Death()
    {
        _isAlive = false;
        _animator.SetTrigger("death");
        _context.Agent.isStopped = true;
        _context.enabled = false;
        _context.DeactivateStateVisual();

        healthBar.gameObject.SetActive(false);
    }
    private void UpdateHealthBarUI()
    {
        if (!_isAlive)
            return;

        healthBar.UpdateHealth(HP, MaxHP);
    }
}

