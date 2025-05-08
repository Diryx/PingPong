using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Zenject;
using TMPro;
using System.IO;

public class SelectMovementKey : MonoBehaviour
{
    [SerializeField] private TMP_Text _keyDisplayText;
    [SerializeField] private Button _rebindButton;
    [SerializeField] private string _actionName;

    private GameData _gameData;
    private bool _isWaitingForInput;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }

    private void Start()
    {
        LoadGameData();
        UpdateKeyDisplay();
    }

    private void OnEnable()
    {
        _rebindButton.onClick.AddListener(StartRebinding);
        LoadGameData();
        UpdateKeyDisplay();
    }

    private void OnDisable()
    {
        _rebindButton.onClick.RemoveListener(StartRebinding);
        _isWaitingForInput = false;
    }

    private void StartRebinding()
    {
        if (_isWaitingForInput) return;

        _isWaitingForInput = true;
        _keyDisplayText.text = "---";
        StartCoroutine(WaitForKeyPress());
    }

    private IEnumerator WaitForKeyPress()
    {
        while (_isWaitingForInput)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        if (IsValidKey(keyCode))
                        {
                            AssignKey(keyCode);
                            yield break;
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private bool IsValidKey(KeyCode key)
    {
        return key != KeyCode.Mouse0 &&
               key != KeyCode.Mouse1 &&
               key != KeyCode.Mouse2 &&
               key != KeyCode.Escape &&
               key != KeyCode.None;
    }

    private void AssignKey(KeyCode newKey)
    {
        switch (_actionName)
        {
            case "MoveUp": _gameData.SetMoveUpKey(newKey); break;
            case "MoveDown": _gameData.SetMoveDownKey(newKey); break;
        }

        _isWaitingForInput = false;
        UpdateKeyDisplay();
        SaveGameData();
    }

    private void UpdateKeyDisplay()
    {
        _keyDisplayText.text = _actionName switch
        {
            "MoveUp" => _gameData.MoveUp,
            "MoveDown" => _gameData.MoveDown,
            _ => "UNKNOWN"
        };
    }

    private void SaveGameData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "GameData.json");
        string json = JsonUtility.ToJson(_gameData, true);
        File.WriteAllText(path, json);
    }

    private void LoadGameData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "GameData.json");

        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, _gameData);
    }
}