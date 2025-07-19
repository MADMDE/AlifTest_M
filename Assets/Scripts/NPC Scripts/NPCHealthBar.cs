using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthBar : MonoBehaviour
{
    [Header("HealthBar References:")]
    [SerializeField] Image fillRendered;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    internal void UpdateHealth(int hP, int maxHP)
    {
        float progress = Mathf.Clamp01((float)hP / (float)maxHP);
        fillRendered.fillAmount = progress;
    }

    private void LateUpdate()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
