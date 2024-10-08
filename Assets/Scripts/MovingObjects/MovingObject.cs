using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    [SerializeField] private MovingObjectSO movingObjectSO;
    private Vector3 direction;

    public MovingObjectSO MovingObjectSO => movingObjectSO;
    public Vector3 Direction => direction;

    protected virtual void Start()
    {
        SetupStartDirection();
    }

    protected virtual void Update()
    {
        UpdatePosition();
    }

    protected void UpdatePosition()
    {
        transform.position += direction * movingObjectSO.speed * Time.deltaTime;
    }

    protected void SetupStartDirection()
    {
        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }
    }

    protected void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

}
