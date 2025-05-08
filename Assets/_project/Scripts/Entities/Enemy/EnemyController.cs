using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int _speed = 8;
    [SerializeField] private float _centerReturnSpeed = 2f;
    [SerializeField] private Rigidbody2D _ballRb;
    [SerializeField] private bool _isPlayerDirection = false;

    private void Update()
    {
        EnemyAI();
    }

    public void ChangeSpeed(int speed)
    {
        _speed = speed;
    }

    private void EnemyAI()
    {
        bool isBallMovingOnEnemy;
        
        if(_isPlayerDirection == false)
            isBallMovingOnEnemy = _ballRb.linearVelocity.x > 0;
        else
            isBallMovingOnEnemy = _ballRb.linearVelocity.x < 0;

        float targetY = isBallMovingOnEnemy
            ? _ballRb.position.y
            : Mathf.MoveTowards(transform.position.y, 0, _centerReturnSpeed * Time.deltaTime);

        float newY = Mathf.MoveTowards(transform.position.y, targetY, _speed * Time.deltaTime);

        transform.position = new Vector2(transform.position.x, newY);
    }
}