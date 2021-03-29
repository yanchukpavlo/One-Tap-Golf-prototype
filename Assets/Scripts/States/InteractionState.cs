
using UnityEngine;

public class InteractionState : BaseState
{
    public override void EnterState(GameManager manager)
    {
        
    }

    public override void Update(GameManager manager)
    {
        manager.StartPush();

        if (Input.GetButtonUp("Fire1"))
        {
            manager.StopPush();
        }
    }
}
