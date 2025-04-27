using TMPro;
using UnityEngine;
using Zenject;

public class Counter : MonoBehaviour
{
    public bool isCountDone = false;

    [SerializeField] private int _targetGoals;
    [SerializeField] private TMP_Text _goalText;
    [SerializeField] private GameObject _hudRestart;
    [SerializeField] private GameObject _hudBasic;

    private BallController _ball;
    private int _currentGoalsPlayer = 0;
    private int _currentGoalsEnemy = 0;

    [Inject]
    private void Construct(BallController ball)
    {
        _ball = ball;
    }

    public void UpdatePlayerGoal()
    {
        _currentGoalsPlayer++;

        if (_currentGoalsPlayer >= _targetGoals)
            CountDone();
    }

    public void UpdateEnemyGoal()
    {
        _currentGoalsEnemy++;

        if (_currentGoalsEnemy >= _targetGoals)
            CountDone();
    }

    public void ResetCountDone()
    {
        isCountDone = false;
        _currentGoalsPlayer = 0;
        _currentGoalsEnemy = 0;
    }

    private void Update()
    {
        UpdateGoalText();

    }

    private void CountDone()
    {
        _hudBasic.SetActive(false);
        _hudRestart.SetActive(true);
        isCountDone = true;
        _ball.ResetSpeed();
        _ball.ResetBall(0);
    }

    private void UpdateGoalText()
    {
        _goalText.text = $"{_currentGoalsPlayer:00}:{_currentGoalsEnemy:00}";
    }
}