using TMPro;
using UnityEngine;
using Zenject;

public class Counter : MonoBehaviour
{
    [HideInInspector] public bool IsCountDone = false;
    [HideInInspector] public bool _isPlayerWin;

    [SerializeField] private int _targetGoals;
    [SerializeField] private TMP_Text _goalText;

    private BallController _ball;
    private FirstHudView _firstHudView;
    private int _currentGoalsPlayer = 0;
    private int _currentGoalsEnemy = 0;

    [Inject]
    private void Construct(BallController ball, FirstHudView firstHudView)
    {
        _ball = ball;
        _firstHudView = firstHudView;
    }

    private void Update()
    {
        UpdateGoalText();

    }

    public void UpdatePlayerGoal()
    {
        _currentGoalsPlayer++;

        if (_currentGoalsPlayer >= _targetGoals)
        {
            _isPlayerWin = true;
            CountDone();
        }
    }

    public void UpdateEnemyGoal()
    {
        _currentGoalsEnemy++;

        if (_currentGoalsEnemy >= _targetGoals)
        {
            _isPlayerWin = false;
            CountDone();
        }
    }

    public void ResetCountDone()
    {
        IsCountDone = false;
        _currentGoalsPlayer = 0;
        _currentGoalsEnemy = 0;
    }

    private void CountDone()
    {
        _firstHudView.SetActiveBasic(false);
        _firstHudView.SetActiveRestart(true);
        IsCountDone = true;
        _ball.ResetSpeed();
        _ball.ResetBall(0);
    }

    private void UpdateGoalText()
    {
        _goalText.text = $"{_currentGoalsPlayer:0}:{_currentGoalsEnemy:0}";
    }
}