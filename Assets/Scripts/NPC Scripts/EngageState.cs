using UnityEngine;

public class EngageState : NPCState
{
    public EngageState(NPCContext context) : base(context) { }

    public override void Enter()
    {
        //To Do: context.animator. Set To Walking
        context.Agent.isStopped = false;
    }

    public override void Update()
    {
        if (context.Player == null)
            return;

        float distanceToPlayer = Vector3.Distance(context.transform.position, context.Player.transform.position);

        if(distanceToPlayer < context.AttackDistance)
        {
            context.SetState(new AttackState(context));
            Debug.Log("Attack xxxxxxxxxxxxxxxxxxxxxxx");
            return;
        }
        else if (distanceToPlayer > context.EngageDistance)
        {
            context.SetState(new PatrolState(context));
            Debug.Log("Patrolling Again!!!! Lost Player!");
            return;
        }

        context.Agent.SetDestination(context.Player.position);
    }

    public override void Exit()
    {
        //To Do:
    }
}
