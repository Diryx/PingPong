using UnityEngine;

[System.Serializable]
public class GameData
{
    public int GoalCount = 3;
    public int DifficultyEnemy = 3;

    public string MoveUp = "W";
    public string MoveDown = "S";

    public KeyCode GetMoveUpKey() => (KeyCode)System.Enum.Parse(typeof(KeyCode), MoveUp);
    public KeyCode GetMoveDownKey() => (KeyCode)System.Enum.Parse(typeof(KeyCode), MoveDown);

    public void SetMoveUpKey(KeyCode key)
    {
        MoveUp = key.ToString();
    }
    public void SetMoveDownKey(KeyCode key)
    {
        MoveDown = key.ToString();
    }
}