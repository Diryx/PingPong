using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
    public bool _isPlayerTouched;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedChange;
    [SerializeField] private float _delaySpawnAfterGoal;
    [SerializeField] private float _minRandom;
    [SerializeField] private float _maxRandom;
    [SerializeField] private ParticleSystem _hitBallParticlesPrefab;
    [SerializeField] private float _delayDeleteParticlesHitBall;

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

    private void Start()
    {
        _currentSpeed = _speed;
        _rb = GetComponent<Rigidbody2D>();
        _cts = new CancellationTokenSource();
        _direction = new Vector2(Random.Range(_minRandom, _maxRandom), Random.Range(_minRandom, _maxRandom)).normalized;
    }

    private void Update()
    {
        MovementBall();
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public async void ResetBall(float currentSpeed)
    {
        currentSpeed = _currentSpeed;

        _currentSpeed = 0;
        gameObject.transform.position = Vector2.zero;

        await UniTask.WaitForSeconds(_delaySpawnAfterGoal, cancellationToken: _cts.Token);
        _currentSpeed = currentSpeed;
        _direction = new Vector2(Random.Range(_minRandom, _maxRandom), Random.Range(_minRandom, _maxRandom)).normalized;
    }

    public void ResetSpeed()
    {
        if (_counter.IsCountDone == true)
            _currentSpeed = 0;
        else
            _currentSpeed = _speed;
    }

    private void MovementBall()
    {
        _rb.linearVelocity = _direction * _currentSpeed;
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem hitBallParticles = Instantiate(_hitBallParticlesPrefab, gameObject.transform.position, Quaternion.identity);

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

        await UniTask.WaitForSeconds(_delayDeleteParticlesHitBall, cancellationToken: _cts.Token);

        if (hitBallParticles != null)
        {
            Destroy(hitBallParticles.gameObject);
        }
    }

    private void OnBallTouchToEntity()
    {
        _currentSpeed += _speedChange;
        _direction.x = -_direction.x;
    }
}
