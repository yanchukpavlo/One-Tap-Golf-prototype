using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameUI;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text gameScoreText;

    int score = 0;
    int best = 0;

    public int Score
    {
        get { return score; }
    }

    void Start()
    {        
        LoadData();
        UpdateText();

        EventsManager.instance.onChangeStateTrigger += ChangeGameState;
        mainMenu.SetActive(true);
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
                gameUI.SetActive(false);
                mainMenu.SetActive(true);
                if (score > best)
                {
                    best = score;
                    SaveData();
                }
                score = 0;
                UpdateText();
                break;

            case EventsManager.GameState.Play:
                mainMenu.SetActive(false);
                gameUI.SetActive(true);
                UpdateText();
                break;

            case EventsManager.GameState.Win:
                AddScore();
                break;

            default:
                break;
        }
    }

    public void StartButton()
    {
        GameManager.instance.TransitionToState(GameManager.instance.startState);
    }

    private void UpdateText()
    {
        string s = string.Format("SCORE: {0}\nBEST: {1}",
                         score, best);
        gameScoreText.text = s;
        bestScoreText.text = "Best score: " + best;
    }

    void AddScore()
    {
        score += 1;
        UpdateText();
    }

    void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    void LoadData()
    {
        PlayerData data = SaveSystem.LoadData();
        if (data != null) best = data.bestScore;
        else best = 0;
    }

    public void ResetDataButton()
    {
        best = 0;
        score = 0;
        SaveData();
        UpdateText();
    }
}