using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackMenuInRestartHud : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _map;

    private FirstHudView _firstHudView;

    [Inject]
    private void Construct(FirstHudView firstHudView)
    {
        _firstHudView = firstHudView;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Back);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Back);
    }

    private void Back()
    {
        _map.SetActive(false);
        _firstHudView.SetActiveBasic(false);
        _firstHudView.SetActiveRestart(false);
        _firstHudView.SetMainManu(true);
    }
}
