using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _delayRestartGame;

    private Counter _counter;
    private BallController _ballController;
    private FirstHudView _firstHudView;

    [Inject]
    private void Construct(Counter counter, BallController ball, FirstHudView firstHudView)
    {
        _counter = counter;
        _ballController = ball;
        _firstHudView = firstHudView;
    }

    private void Start()
    {
        _button.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(RestartGame);
    }

    public async void RestartGame()
    {
        _firstHudView.SetActiveBasic(true);
        _firstHudView.SetActiveRestart(false);
        _counter.ResetCountDone();

        await UniTask.WaitForSeconds(_delayRestartGame);

        _ballController.ResetSpeed();
        _ballController.ResetBall(0);
    }
}