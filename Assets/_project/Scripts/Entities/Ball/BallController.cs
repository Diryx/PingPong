using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
    public bool _isPlayerTouched;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedChange;

    private Rigidbody2D _rb;
    private Counter _counter;
    private Vector2 _direction;
    private float _currentSpeed;
    private CancellationTokenSource _cts;

    [Inject]
    private void Construct(Counter counter)
    {
        _counter = counter;
    }

    public async void ResetBall(float currentSpeed)
    {
        currentSpeed = _currentSpeed;

        _currentSpeed = 0;
        gameObject.transform.position = Vector2.zero;

        await UniTask.Delay(1000, cancellationToken: _cts.Token);
        _currentSpeed = currentSpeed;
        _direction = new Vector2(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)).normalized;
    }

    public void ResetSpeed()
    {
        if (_counter.isCountDone == true)
            _currentSpeed = 0;
        else
            _currentSpeed = _speed;
    }

    private void Start()
    {
        _currentSpeed = _speed;
        _rb = GetComponent<Rigidbody2D>();
        _cts = new CancellationTokenSource();
        _direction = new Vector2(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f)).normalized;
    }

    private void Update()
    {
        MovementBall();
    }
    
    private void MovementBall()
    {
        _rb.linearVelocity = _direction * _currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement _))
        {
            OnBallTouchToEntity();
            _isPlayerTouched = true;
        }
        else if (collision.gameObject.TryGetComponent(out EnemyController _))
        {
            OnBallTouchToEntity();
            _isPlayerTouched = false;
        }
        else if (collision.gameObject.TryGetComponent(out Wall _))
        {
            _direction.y = -_direction.y;
        }
    }

    private void OnBallTouchToEntity()
    {
        _currentSpeed += _speedChange;
        _direction.x = -_direction.x;
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
