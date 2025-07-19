using System;
using UnityEngine;
using Cinemachine;

public class AttackState : NPCState
{
    private float _lastAttackTime = float.NegativeInfinity;
    public AttackState(NPCContext context) : base(context) { }

    public override void Enter()
    {
        context.Agent.isStopped = true;
    }

    public override void Update()
    {
        if (context.Player == null)
            return;

        float distanceToPlayer = Vector3.Distance(context.transform.position, context.Player.transform.position);

        if(distanceToPlayer > context.AttackDistance)
        {
            context.SetState(new EngageState(context));
            return;
        }

        if(Time.time - _lastAttackTime > context.AttackCooldownTime)
        {
            context.BodyAnimator.SetTrigger("attack");
            Debug.Log("Sibil Chakhmaghi attack!");
            context.Player.GetComponent<PlayerHealth>()?.TakeDamage(context.AttackDamage);
            context.CameraImpulseSource.GenerateImpulse(context.AttackImpulse);
            _lastAttackTime = Time. time;
        }
        else
        {
            Debug.Log("Cooldown");
        }

        LookAt(context.Player);
    }

    public override void Exit()
    {
        // To Do: 
    }

    private void LookAt(Transform target)
    {
        Vector3 direction = target.position - context.transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation.normalized, rotation, Time.deltaTime * 10f);
    }

}
