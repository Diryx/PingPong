using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _centerReturnSpeed = 2f;
    [SerializeField] private Rigidbody2D _ballRb;

    private Transform _enemyTransform;

    private void Start()
    {
        _enemyTransform = transform;
    }

    private void Update()
    {
        EnemyAI();
    }

    private void EnemyAI()
    {
        bool isBallMovingOnEnemy = _ballRb.linearVelocity.x > 0;

        float targetY = isBallMovingOnEnemy
            ? _ballRb.position.y
            : Mathf.MoveTowards(_enemyTransform.position.y, 0, _centerReturnSpeed * Time.deltaTime);

        float newY = Mathf.MoveTowards(_enemyTransform.position.y, targetY, _speed * Time.deltaTime);

        _enemyTransform.position = new Vector2(_enemyTransform.position.x, newY);
    }
}