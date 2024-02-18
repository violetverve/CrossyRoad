using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    private const string OBSTACLE_TAG = "Obstacle";
    private const string WATER_TERRAIN_TAG = "WaterTerrain";

    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerXPositionChanged;
    public event EventHandler OnPlayerMoved;
    public event EventHandler OnPlayerDied;
    public event EventHandler OnNewMaxXPositionReached;
    private Animator animator;
    private bool isHopping = false;
    private bool isDead = false;
    private IDeathBehaviour deathBehavior;
    private int maxPositionXReached = 0;

    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        PlayerVisual.Instance.OnHopAnimationComplete += PlayerVisual_OnHopAnimationComplete;
    }

    private void PlayerVisual_OnHopAnimationComplete(object sender, EventArgs e) {
        isHopping = false;
    }

    public void Die(IDeathBehaviour deathBehavior) {
        if (this.deathBehavior != null) {
            return;
        }
        SetDead(true);
        this.deathBehavior = deathBehavior;
        deathBehavior.Execute();
        OnPlayerDied?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {

        if (isDead) {
            return;
        }
   
        if (Input.GetKeyDown(KeyCode.W)) {

            float zDifference = transform.position.z - Mathf.RoundToInt(transform.position.z);

            MovePlayer(new Vector3(1, 0, -zDifference));

            if (transform.position.x > maxPositionXReached) {
                maxPositionXReached = Mathf.RoundToInt(transform.position.x);
                OnNewMaxXPositionReached.Invoke(this, EventArgs.Empty);
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            float zDifference = transform.position.z - Mathf.RoundToInt(transform.position.z);

            MovePlayer(new Vector3(-1, 0, -zDifference));
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            MovePlayer(new Vector3(0, 0, 1));
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            MovePlayer(new Vector3(0, 0, -1));
        }

    }

    public void SetParent(Transform parent) {
        transform.SetParent(parent);
    }

    private void MovePlayer(Vector3 direction) {
        if (CanMove(direction) && !isHopping) {

            isHopping = true;
            Vector3 newPosition = transform.position + direction;
            transform.position = newPosition;
        

            if (direction.x != 0) {
                OnPlayerXPositionChanged?.Invoke(this, EventArgs.Empty);
            }

            OnPlayerMoved?.Invoke(this, EventArgs.Empty);

            if (direction != Vector3.zero) {
                Vector3 adjustedDirection = Quaternion.Euler(0, -90, 0) * direction;
                transform.forward = adjustedDirection.normalized;
            }
        }
    }

    bool CanMove(Vector3 direction) {
        float size = 0.4f;
        Vector3 newPosition = transform.position + direction;
        newPosition.y = 0.2f;

        Collider[] hitColliders = Physics.OverlapBox(newPosition, new Vector3(size, size, size));

        foreach (var hitCollider in hitColliders) {
            if (hitCollider.CompareTag(OBSTACLE_TAG)) {
                return false;
            }
        }
        return true;
    }

    public float GetXPosition() {
        return transform.position.x;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetKinematic(bool isKinematic) {
        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.isKinematic = isKinematic;
    }

    public void DisableCollider() {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void DeactivatePlayer() {
        gameObject.SetActive(false);
    }

    public void SetDead(bool isDead) {
        this.isDead = isDead;
    }
}