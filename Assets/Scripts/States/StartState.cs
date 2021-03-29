using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : BaseState
{
    //bool isPlay;
    public override void EnterState(GameManager manager)
    {
        //isPlay = false;
        manager.NewLevel();
        EventsManager.instance.ChangeStateTrigger(EventsManager.GameState.Play);
    }

    public override void Update(GameManager manager)
    {
        if (/*isPlay && */ Input.GetButtonDown("Fire1"))
        {
            manager.TransitionToState(manager.interactionState);
        }

        //isPlay = true;
    }
}
