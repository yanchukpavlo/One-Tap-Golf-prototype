
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(GameManager manager);

    public abstract void Update(GameManager manager);
}
