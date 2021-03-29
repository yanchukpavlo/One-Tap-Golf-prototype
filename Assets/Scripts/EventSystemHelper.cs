using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemHelper : MonoBehaviour
{
    EventSystem system;
    GameObject currentGameObject;
    bool isEsenable = true;

    void Start()
    {
        system = GetComponent<EventSystem>();
        EventsManager.instance.onChangeStateTrigger += ChangeGameState;
    }

    private void OnDisable()
    {
        EventsManager.instance.onChangeStateTrigger -= ChangeGameState;
    }

    private void ChangeGameState(EventsManager.GameState state)
    {
        switch (state)
        {
            case EventsManager.GameState.Menu:
                isEsenable = true;
                break;
            case EventsManager.GameState.Play:
                isEsenable = false;
                break;
            case EventsManager.GameState.Win:
                break;

            default:
                break;
        }
    }

    void Update()
    {
        if (isEsenable)
        {
            if (currentGameObject != system.currentSelectedGameObject && system.currentSelectedGameObject != null)
            {
                currentGameObject = system.currentSelectedGameObject;
            }

            if (system.currentSelectedGameObject == null)
            {
                system.SetSelectedGameObject(currentGameObject);
            }
        }
    }
}
