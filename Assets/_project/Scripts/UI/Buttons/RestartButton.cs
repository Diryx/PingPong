using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _hudBasic;
    [SerializeField] private GameObject _hudRestart;

    private Counter _counter;
    private BallController _ballController;

    [Inject]
    private void Construct(Counter counter, BallController ball)
    {
        _counter = counter;
        _ballController = ball;
    }

    public async void RestartGame()
    {
        _hudBasic.SetActive(true);
        _hudRestart.SetActive(false);
        _counter.ResetCountDone();

        await UniTask.Delay(1000);

        _ballController.ResetSpeed();
    }

    private void Start()
    {
        _button.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }
}