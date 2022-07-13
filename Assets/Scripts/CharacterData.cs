using System.Collections.Generic;
using activity;

public class CharacterData
{
    private List<Activity> activities = new List<Activity>();

    public void AddActivity(ControllableMovementActivity activity)
    {
        activities.Add(activity);
    }

    public void SkipTime(float deltaTime)
    {
        foreach (var activity in activities)
        {
            activity.Execute(this, deltaTime);
        }
    }
}