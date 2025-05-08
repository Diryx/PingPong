using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsButton : MonoBehaviour
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
        _button.onClick.AddListener(ToOptions);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ToOptions);
    }

    private void ToOptions()
    {
        _firstHudView.SetOptionsMenu(true);
        _firstHudView.SetThisMenu(false);
    }
}
