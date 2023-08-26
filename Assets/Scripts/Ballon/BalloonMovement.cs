using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalloonMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _initVelocity;
    [SerializeField] private float _velocityIncreaseStep;
    [SerializeField] private float _timeStepToIncreaseVelocity;

    private Vector2 _currentVelocity = new Vector2(0,0);
    private Rigidbody2D _rigibody;
    private Timer _timer;


    private void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        
        _timer = FindObjectOfType<Timer>();
        if (!_timer)
        {
            Debug.LogError("Timer not found");
        }
        _currentVelocity = _initVelocity;
    }

    private void Update()
    {
        int speedMultiplier = (int)_timer.CurrentTime / (int)_timeStepToIncreaseVelocity;

        if (_currentVelocity.y < _initVelocity.y + _velocityIncreaseStep * speedMultiplier)
        {
            _currentVelocity.y = _initVelocity.y + _velocityIncreaseStep * speedMultiplier;
        }

    }
    private void FixedUpdate()
    {
        _rigibody.MovePosition(_rigibody.position + _currentVelocity * Time.fixedDeltaTime);
    }

 
}
