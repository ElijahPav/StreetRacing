using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private const float _horizontalBorderPosition = 1.2f;
    private const float _verticalBorderPosition = 3;

    private const float _horizontalDefaultSpeed = 4f;
    private const float _vertiaclDefaultSpeed = 4f;

    private const float _horizontalDebuffSpeed = 3f;
    private const float _vertiaclDebuffSpeed = 3f;

    private const float _horizontalBuffSpeed = 5f;
    private const float _vertiaclBuffSpeed = 5f;

    private float _currentHorizontalSpeed;
    private float _currentVertiaclSpeed;

    private bool IsMovementEnable;
    public void DithableMovement() => IsMovementEnable = false;
    public void EnableMovement() => IsMovementEnable = true;


    void Start()
    {
        SetDefaultSpeed();

    }

    void Update()
    {
        if (IsMovementEnable)
        {
            if (Input.GetKey(KeyCode.D) && transform.position.x < _horizontalBorderPosition * Vector3.right.x)
            {
                transform.Translate(Vector3.right * _currentHorizontalSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                if (Input.GetKey(KeyCode.A) && transform.position.x > _horizontalBorderPosition * Vector3.left.x)
                {
                    transform.Translate(Vector3.left * _currentHorizontalSpeed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKey(KeyCode.W) && transform.position.y < _verticalBorderPosition * Vector3.up.y)
            {
                transform.Translate(Vector3.up * _currentVertiaclSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                if (Input.GetKey(KeyCode.S) && transform.position.y > _verticalBorderPosition * Vector3.down.y)
                {
                    transform.Translate(Vector3.down * _currentVertiaclSpeed * Time.deltaTime, Space.World);
                }
            }
        }

    }

    public void BoostSpeed()
    {
        _currentHorizontalSpeed = _horizontalBuffSpeed;
        _currentVertiaclSpeed = _vertiaclBuffSpeed;
    }
    public void DebuffSpeed()
    {
        _currentHorizontalSpeed = _horizontalDebuffSpeed;
        _currentVertiaclSpeed = _vertiaclDebuffSpeed;
    }

    public void SetDefaultSpeed()
    {
        _currentHorizontalSpeed = _horizontalDefaultSpeed;
        _currentVertiaclSpeed = _vertiaclDefaultSpeed;
    }

}
