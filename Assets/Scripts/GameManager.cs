using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region Variables

    [Header("Game objects")]
    [SerializeField] GameObject hole;
    [SerializeField] GameObject ballPref;
    [SerializeField] Transform ballSpawnPos;
    [SerializeField] Trajectory trajectory;
    [Space]

    [Header("Gameplay parameters")]
    [SerializeField] float startforce = 1;
    [SerializeField] float maxforce = 8.8f;
    [SerializeField] float speedIncrement = 0.2f;

    BaseState currentState;
    public BaseState CurrentState
    {
        get { return currentState; }
    }

    GameObject ball;
    float force = 1;
    float speedChangeForce = 1;

    public readonly MenuState menuState = new MenuState();
    public readonly StartState startState = new StartState();
    public readonly InteractionState interactionState = new InteractionState();
    public readonly ResultState resultState = new ResultState();

    #endregion

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

    void Start()
    {
        EventsManager.instance.onChangeStateTrigger += ChangeStateTrigger;
        TransitionToState(menuState);
    }

    private void OnDisable()
    {
        EventsManager.instance.onChangeStateTrigger -= ChangeStateTrigger;
    }

    private void ChangeStateTrigger(EventsManager.GameState state)
    {
        switch (state)
        {
            case EventsManager.GameState.Menu:
                TransitionToState(menuState);
                break;
            case EventsManager.GameState.Play:
                break;
            case EventsManager.GameState.Win:
                TransitionToState(startState);
                break;

            default:
                break;
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Update(this);
        }
    }

    public void TransitionToState(BaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void PrepareForGame()
    {
        speedChangeForce = startforce - speedIncrement;
        force = startforce;
    }

    public void StartPush()
    {
        trajectory.Show();

        if (force > maxforce)
        {
            StopPush();
            return;
        }

        force += Time.deltaTime * speedChangeForce;
        trajectory.UpdateDots(ball.transform.position, force);
    }

    public void StopPush()
    {
        trajectory.Hide();
        ball.GetComponent<Rigidbody2D>().drag = 0;
        if (ball != null) ball.GetComponent<Ball>().Push(force);
        force = startforce;

        TransitionToState(resultState);
    }

    public void NewLevel()
    {
        hole.transform.position = new Vector3(Random.Range(-1f, 8f), -3.225f, -1f);
        if (ball != null) Destroy(ball);
        ball = Instantiate(ballPref, ballSpawnPos.position, Quaternion.identity);
        speedChangeForce += speedIncrement;
    }
}
