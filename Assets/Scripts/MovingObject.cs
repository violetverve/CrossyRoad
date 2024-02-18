using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField] private MovingObjectSO movingObjectSO;
    private Vector3 direction;

    private void Start() {
        SetupStartDirection();
    }

    private void Update() {
        UpdatePosition();
    }

    protected void UpdatePosition() {
        transform.position += direction * movingObjectSO.speed * Time.deltaTime;
    }

    protected void SetupStartDirection() {
        if (direction == Vector3.zero) {
            direction = transform.forward;
        }
    }

    protected void SetDirection(Vector3 newDirection) {
        direction = newDirection;
    }

}
