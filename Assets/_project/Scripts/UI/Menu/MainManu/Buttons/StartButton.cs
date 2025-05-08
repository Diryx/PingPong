using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private FirstHudView _firstHudView;

    [Inject]
    private void Construct(FirstHudView firstHudView)
    {
        _firstHudView = firstHudView;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        _firstHudView.SetThisMenu(false);
        _firstHudView.SetStartMenu(true);
    }
}
