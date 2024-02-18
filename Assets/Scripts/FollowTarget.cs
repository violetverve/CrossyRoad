using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public static FollowTarget Instance { get; private set; }
    [SerializeField] private Transform target;
    private Vector3 offset;
    // private float percentageComplete;
    // private float elapsedTime;
    // private float desiredDuration = 0.3f;
    // private float speed = 0.1f;
    // private Vector3 desiredPosition;
    // private Vector3 startPosition;

    private void Awake() {
        Instance = this;
        offset = transform.position - target.position;
    }

    // private void Start() {
    //     Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;
    // }

    // private void Player_OnPlayerMoved(object sender, System.EventArgs e) {
    //     elapsedTime = 0;
    //     desiredPosition = target.position + offset;
    //     startPosition = transform.position;
    //     percentageComplete = 0;
    // }


    private void Update() {
        if (target == null) {
            return;
        }

        transform.position = target.position + offset;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}