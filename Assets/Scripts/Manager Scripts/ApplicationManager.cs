using System;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    void Start()
    {
        SetTargetFrameRate();
    }

    private void SetTargetFrameRate()
    {
        Application.targetFrameRate = 60;
    }
}
