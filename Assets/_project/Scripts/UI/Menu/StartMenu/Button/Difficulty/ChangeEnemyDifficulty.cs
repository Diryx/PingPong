using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangeEnemyDifficulty : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] private int _enemySpeed;

    private EnemyController _enemyController;
    private GameData _gameData;
    private string _path;

    [Inject]
    private void Construct(EnemyController enemyController, GameData gameData)
    {
        _enemyController = enemyController;
        _gameData = gameData;
    }

    private void Start()
    {
        _path = Path.Combine(Application.streamingAssetsPath, "GameData.json");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeDifficulty);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeDifficulty);
    }

    private void ChangeDifficulty()
    {
        _gameData.DifficultyEnemy = _enemySpeed;
        File.WriteAllText(_path, JsonUtility.ToJson(_gameData, true));
    }
}
