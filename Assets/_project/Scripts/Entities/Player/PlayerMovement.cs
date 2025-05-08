using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private GameData _gameData;
    KeyCode upKey;
    KeyCode downKey;

    [Inject]
    private void Construct(GameData gameData)
    {
        _gameData = gameData;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        upKey = _gameData.GetMoveUpKey();
        downKey = _gameData.GetMoveDownKey();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(upKey))
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _speed);
        }
        else if (Input.GetKeyDown(downKey))
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_speed);
        }
    }
}