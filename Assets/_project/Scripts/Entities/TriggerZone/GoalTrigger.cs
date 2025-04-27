using UnityEngine;
using Zenject;

public class GoalTrigger : MonoBehaviour
{
    private Counter _counter;

    [Inject]
    private void Construct(Counter counter)
    {
        _counter = counter;
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.TryGetComponent(out BallController ball))
        {
            if (ball._isPlayerTouched == true)
                _counter.UpdatePlayerGoal();
            else 
                _counter.UpdateEnemyGoal();

            ball.ResetBall(0);
        }
    }
}