public abstract class Activity
{
    public void Execute(CharacterData character, float deltaTime)
    {
        var actor = StageManager.instance.GetActor(character);
        
        if (actor != null)
        {
            ExecuteOnStage(actor, deltaTime);
        }
    }

    protected abstract void ExecuteOnStage(Actor actor, float deltaTime);
}