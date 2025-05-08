using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangeTargetGoals : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] private int _targetGoals;

    private Counter _counter;
    private GameData _gameData;
    private string _path;

    [Inject]
    private void Construct(Counter counter, GameData gameData)
    {
        _counter = counter;
        _gameData = gameData;
    }

    private void Start()
    {
        _path = Path.Combine(Application.streamingAssetsPath, "GameData.json");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeGoals);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeGoals);
    }

    private void ChangeGoals()
    {
        _gameData.GoalCount = _targetGoals;
        File.WriteAllText(_path, JsonUtility.ToJson(_gameData, prettyPrint: true));
    }
}
