using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [Header("HealthBar References:")]
    [SerializeField] Image fillRendered;

    internal void UpdateHealth(int hP, int maxHP)
    {
        float progress = Mathf.Clamp01((float)hP / (float)maxHP);
        fillRendered.fillAmount = progress;
    }
}
