public abstract class NPCState
{
    protected NPCContext context;

    public NPCState(NPCContext context)
    {
        this.context = context;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
