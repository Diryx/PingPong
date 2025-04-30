using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _exitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(QuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
