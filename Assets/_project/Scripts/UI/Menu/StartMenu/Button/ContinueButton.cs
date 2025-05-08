using UnityEngine;
using System.IO;
using Zenject;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] GameObject _map;

    private EnemyController _enemyController;
    private Counter _counter;
    private FirstHudView _firstHudView;
    private GameData _gameData;
    private string _path;
    private int _enemyDifficulty;
    private int _countGoals;

    [Inject]
    private void Construct(EnemyController enemyController, Counter counter, FirstHudView firstHudView, GameData gameData)
    {
        _enemyController = enemyController;
        _counter = counter;
        _firstHudView = firstHudView;
        _gameData = gameData;
    }

    private void Start()
    {
        _path = Path.Combine(Application.streamingAssetsPath, "GameData.json");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Continue);
    }

    private void Continue()
    {
        _gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(_path));
        _enemyDifficulty = _gameData.DifficultyEnemy;
        _countGoals = _gameData.GoalCount;

        _counter.SelectTargetGoals(_countGoals);
        _enemyController.ChangeSpeed(_enemyDifficulty);

        _map.SetActive(true);
        _firstHudView.SetMainManu(false);
        _firstHudView.SetActiveBasic(true);
    }
}
