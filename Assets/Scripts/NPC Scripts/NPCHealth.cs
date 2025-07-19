using System;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    [SerializeField] int MaxHP;
    [SerializeField] GameObject vfxDestroyPrefab;
    int HP;
    bool isAlive;

    private void Start()
    {
        HP = MaxHP;
        isAlive = true;
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
    }

    public void SelfDestruct()
    {
        isAlive = false;
        //Instantiate(vfxDestroyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

