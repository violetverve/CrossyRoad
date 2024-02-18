using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MovingObject {

    private const string IS_FLYING = "isFlying";
    private Vector3 eagleDirection = new Vector3(-1, 0, 0);

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        SetDirection(eagleDirection);

        animator.SetBool(IS_FLYING, true);
    }
}