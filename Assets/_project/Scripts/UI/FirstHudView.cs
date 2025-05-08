using UnityEngine;
using Zenject;

public class FirstHudView : MonoBehaviour
{
    [SerializeField] private GameObject _hudBasic;
    [SerializeField] private GameObject _hudRestart;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _thisMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _achivmentsMenu;
    [SerializeField] private GameObject _customMenu;
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

    public void SetMainManu(bool isActive)
    {
        _mainMenu.SetActive(isActive);
    }

    public void SetThisMenu(bool isActive)
    {
        _thisMenu.SetActive(isActive);
    }

    public void SetStartMenu(bool isActive)
    {
        _startMenu.SetActive(isActive);
    }

    public void SetOptionsMenu(bool isActive)
    {
        _optionsMenu.SetActive(isActive);
    }

    public void SetAchivmentsMenu(bool isActive)
    {
        _achivmentsMenu.SetActive(isActive);
    }

    public void SetCustomMenu(bool isActive)
    {
        _customMenu.SetActive(isActive);
    }
}