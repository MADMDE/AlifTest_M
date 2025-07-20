using StarterAssets;
using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] PlayerShurikan shurikanPrefab;

    private void OnEnable()
    {
        StarterAssetsInputs.OnShootAction += Shoot;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.OnShootAction -= Shoot;
    }

    private void Shoot()
    {
        Instantiate(shurikanPrefab);
    }
}
