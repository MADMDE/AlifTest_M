using UnityEngine;
using System;

public class EngageState : NPCState
{
    private bool _disengaging;
    private float _inititateDisengageTime = float.NegativeInfinity;
    public EngageState(NPCContext context) : base(context) { }

    public override void Enter()
    {
        //To Do: context.animator. Set To Walking
        context.Agent.isStopped = false;
        _disengaging = false;
    }

    public override void Update()
    {
        if (context.Player == null)
            return;

        float distanceToPlayer = Vector3.Distance(context.transform.position, context.Player.transform.position);

        if(distanceToPlayer < context.AttackDistance)
        {
            context.SetState(new AttackState(context));
            return;
        }
        else if (distanceToPlayer > context.EngageDistance)
        {
            if(!_disengaging)
            {
                _disengaging = true;
                _inititateDisengageTime = Time.time;
                Debug.Log("_inititateDisengageTime" + _inititateDisengageTime);
                context.UpdateLostPlayerUI(true);
            }

            if(Time.time - _inititateDisengageTime > context.DisengageTime)
            {
                _disengaging = false;
                context.SetState(new PatrolState(context));
                Debug.Log("Disengage=>Patroling");
            }

            return;
        }
        else
        {
            _disengaging = false;
            context.UpdateLostPlayerUI(false);
        }

        context.Agent.SetDestination(context.Player.position);
    }

    public override void Exit()
    {
        context.UpdateLostPlayerUI(false);
    }
}
