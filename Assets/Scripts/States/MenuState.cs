using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void EnterState(GameManager manager)
    {
        manager.PrepareForGame();
    }

    public override void Update(GameManager manager)
    {
        
    }
}
