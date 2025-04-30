using UnityEngine;
using Zenject;

public class FirstHudView : MonoBehaviour
{
    [SerializeField] private GameObject _hudBasic;
    [SerializeField] private GameObject _hudRestart;
    [SerializeField] private GameObject _playerWinText;
    [SerializeField] private GameObject _enemyWinText;
    
    private Counter _counter;

    [Inject]
    private void Construct(Counter counter)
    {
        _counter = counter;
    }

    public void SetActiveBasic(bool isActive)
    {
        _hudBasic.SetActive(isActive);
    }

    public void SetActiveRestart(bool isActive)
    {
        _hudRestart.SetActive(isActive);
        if(_counter._isPlayerWin == true)
            _playerWinText.SetActive(isActive);
        else
            _enemyWinText.SetActive(isActive);
    }
}