using System;
using UnityEngine;

public class PatrolState : NPCState
{
    private int _waypointIndex = 0;
    private bool _isWayback = false;
    public PatrolState(NPCContext context) : base(context) { }

    public override void Enter()
    {
        context.Agent.isStopped = false;

        if (context.waypoints.Length > 0)
        {
            context.Agent.SetDestination(context.waypoints[0].position);
        }
    }

    public override void Update()
    {
        if (context.Player == null)
            return;

        float distanceToPlayer = Vector3.Distance(context.transform.position, context.Player.position);

        if (distanceToPlayer < context.EngageDistance
            && InFieldOfView(context.Player))
        {
            context.SetState(new EngageState(context));
            return;
        }

        if (context.waypoints.Length == 0)
            return;

        float distanceToWaypoint = Vector3.Distance(context.transform.position, context.waypoints[_waypointIndex].position);
        if (distanceToWaypoint < context.DistanceToWaypointThreshold)
        {

            _waypointIndex = ClaculateNewWaypointIndex();

            context.Agent.SetDestination(context.waypoints[_waypointIndex].position);
        }

    }

    private int ClaculateNewWaypointIndex()
    {
        int index = this._waypointIndex + (_isWayback ? -1 : +1);

        if (index >= context.waypoints.Length)
        {
            _isWayback = true;
            index = context.waypoints.Length - 2;
        }
        else if (index < 0)
        {
            _isWayback = false;
            index = 1;
        }

        return Mathf.Clamp(index, 0, context.waypoints.Length - 1);
    }

    private bool InFieldOfView(Transform target)
    {
        Vector3 directionToTarget = target.position - context.transform.position;
        float angle = Vector3.Angle(context.transform.forward, directionToTarget);
        bool result = angle < context.FieldOfView;
        return result;
    }

    public override void Exit()
    {
        //To Do: context.animator Set NotWalking
    }
}
