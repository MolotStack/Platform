using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private float _speed;

    private Vector2 _currentDirection;


    public void SetDirection(Vector2 direction)
    {
        _currentDirection = direction;
    }

    public void Click()
    {
        Debug.Log("Hello world!");
    }

    private void Movement()
    {
        transform.Translate(_currentDirection * _speed * Time.deltaTime);
    }
    
    private void Update()
    {
        Movement();
    }
}
