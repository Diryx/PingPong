using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float moveVertical = Input.GetAxis("Vertical") * _speed;
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, moveVertical);
    }
}