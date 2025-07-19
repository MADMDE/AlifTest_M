using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCStateUI : MonoBehaviour
{
    [Header("State UI References:")]
    [SerializeField] TMP_Text stateText;
    [SerializeField] Image stateFrame;   
    [SerializeField] TMP_Text lostPlayerText;
    [SerializeField] Image lostPlayerFrame;


    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        LookAtCamera();
    }

    public void UpdateStateVisual(NPCState currentState)
    {
        string text = "- default";
        Color color = new Color(1f, 1f, 1f, 0.9f);
        if (currentState is PatrolState)
        {
            text = "patrol";
            color = new Color(0, 1f, 0, 0.9f);
        }
        else if (currentState is EngageState)
        {
            text = "engage";
            color = new Color(1f, 1f, 0, 0.9f);
        }
        else if (currentState is AttackState)
        {
            text = "attack";
            color = new Color(1f, 0, 0, 0.9f);
        }

        stateText.text = text;
        stateText.color = color;

        stateFrame.color = new Color(color.r, color.g, color.b, 0.25f);
    }

    private void LookAtCamera()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }

    internal void UpdateLostPlayerUI(bool value)
    {
        lostPlayerText.gameObject.SetActive(value);
        lostPlayerFrame.gameObject.SetActive(value);
    }
}
