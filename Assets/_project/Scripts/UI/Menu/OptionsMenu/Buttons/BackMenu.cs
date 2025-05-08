using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackMenu : MonoBehaviour
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
        _button.onClick.AddListener(Back);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Back);
    }

    private void Back()
    {
        _firstHudView.SetOptionsMenu(false);
        _firstHudView.SetThisMenu(true);
    }
}
