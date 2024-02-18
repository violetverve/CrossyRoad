using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerVisual : MonoBehaviour {

    public static PlayerVisual Instance { get; private set; }

    public event EventHandler OnHopAnimationComplete;
    private const string HOP_TRIGGER = "Hop";
    private Animator animator;

    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnPlayerMoved += Player_OnPlayerMoved;
    }

    private void Player_OnPlayerMoved(object sender, System.EventArgs e) {
        animator.SetTrigger(HOP_TRIGGER);
    }

    public void HopAnimationCompleted() {
        OnHopAnimationComplete?.Invoke(this, EventArgs.Empty);
    }

    public void PlayAnimation(string trigger) {
        animator.SetTrigger(trigger);
    }

}