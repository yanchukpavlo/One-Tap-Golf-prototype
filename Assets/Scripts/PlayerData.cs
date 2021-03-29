
[System.Serializable]
public class PlayerData
{
    public int bestScore;

    public PlayerData(UI_Manager manager)
    {
        bestScore = manager.Score;
    }
}
