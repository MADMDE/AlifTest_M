using Cinemachine;
using StarterAssets;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCContext : MonoBehaviour
{
    [Header("NPC Core Properties:")]
    public float EngageDistance = 5.0f;
    public float AttackDistance = 2.0f;
    public float FieldOfView = 90.0f;
    public float DistanceToWaypointThreshold = 0.5f;
    public float AttackCooldownTime = 2.0f;
    public int AttackDamage = 10;
    public float AttackImpulse = 0.05f;
    public float DisengageTime = 1.0f;

    [Header("NPC Refrences:")]
    public Transform[] waypoints;

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Animator BodyAnimator;
    [HideInInspector] public Transform Player;
    [HideInInspector] public CinemachineImpulseSource CameraImpulseSource;

    private NPCState _currentState;
    private NPCStateUI _stateUI;

    void Start()
    {
        Player = FindFirstObjectByType<FirstPersonController>().transform;
        Agent = GetComponent<NavMeshAgent>();
        BodyAnimator = GetComponent<Animator>();
        _stateUI = GetComponentInChildren<NPCStateUI>();
        CameraImpulseSource = GetComponent<CinemachineImpulseSource>();

        SetState(new PatrolState(this));
    }

    void Update()
    {
        _currentState?.Update();
    }

    public void SetState(NPCState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        _stateUI.UpdateStateVisual(_currentState);
    }

    public void UpdateLostPlayerUI(bool value)
    {
        _stateUI.UpdateLostPlayerUI(value);
    }

    public void DeactivateStateVisual()
    {
        _stateUI?.DeactivateStateVisual();
    }
}
