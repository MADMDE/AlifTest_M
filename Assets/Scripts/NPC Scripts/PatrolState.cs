using System;
using UnityEngine;

public class PatrolState : NPCState
{
    private int _waypointIndex = 0;
    public PatrolState(NPCContext context) : base(context) { }

    public override void Enter()
    {
        context.Agent.isStopped = false;
        //To Do: context.animator. Set To Walking.....

        if(context.waypoints.Length > 0)
        {
            context.Agent.SetDestination(context.waypoints[0].position);
        }
    }

    public override void Update()
    {
        if (context.Player == null)
            return;

        float distanceToPlayer = Vector3.Distance(context.transform.position, context.Player.position);

        if( distanceToPlayer < context.EngageDistance 
            && InFieldOfView(context.Player))
        {
            context.SetState(new EngageState(context));
            Debug.Log("Engage!!!!!!!!!!");
            return;
        }

        if (context.waypoints.Length == 0)
            return;

        float distanceToWaypoint = Vector3.Distance(context.transform.position, context.waypoints[_waypointIndex].position);
        if(distanceToWaypoint < context.DistanceToWaypointThreshold)
        {
            _waypointIndex = (_waypointIndex + 1) % context.waypoints.Length;
            //To Do: Return In Reverse
            context.Agent.SetDestination(context.waypoints[_waypointIndex].position);
        }

    }

    private bool InFieldOfView(Transform target)
    {
        Vector3 directionToTarget = target.position - context.transform.position;
        float angle = Vector3.Angle(context.transform.forward, directionToTarget);
        bool result = angle < context.FieldOfView;
        Debug.Log($"Angle:{angle} InFieldOfView???: {result}");
        return result;
    }

    public override void Exit()
    {
        //To Do: context.animator Set NotWalking
    }
}
