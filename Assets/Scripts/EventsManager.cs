using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    public enum GameState
    {
        Menu,
        Play,
        Win
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public event Action<GameState> onChangeStateTrigger;
    public void ChangeStateTrigger(GameState state)
    {
        onChangeStateTrigger?.Invoke(state);
    }
}