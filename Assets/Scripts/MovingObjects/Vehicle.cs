using UnityEngine;
using Utils;
using CrossyRoad.Player;

public class Vehicle : MovingObject 
{
    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;

    [SerializeField] private CustomTrigger _vehicleDetector;
    [SerializeField] private CustomTrigger _frontPlayerDetector;
    [SerializeField] private CustomTrigger _sidePlayerDetector;
    
    protected override void Start()
    {
        base.Start();
        _currentSpeed = MovingObjectSO.speed;
    }
    
    protected override void Update()
    {
        transform.position += Direction * _currentSpeed * Time.deltaTime;
    }

    private void OnEnable() {
        _vehicleDetector.OnTriggerEnterEvent += OnVehicleDetectorTriggerEnter;
        _vehicleDetector.OnTriggerExitEvent += OnVehicleDetectorTriggerExit;

        _frontPlayerDetector.OnTriggerEnterEvent += OnFrontPlayerDetectorTriggerEnter;

        // _sidePlayerDetector.OnTriggerEnterEvent += OnSidePlayerDetectorTriggerEnter;
    }

    private void OnDisable() {
        _vehicleDetector.OnTriggerEnterEvent -= OnVehicleDetectorTriggerEnter;
        _vehicleDetector.OnTriggerExitEvent -= OnVehicleDetectorTriggerExit;

        _frontPlayerDetector.OnTriggerEnterEvent -= OnFrontPlayerDetectorTriggerEnter;
        // _sidePlayerDetector.OnTriggerEnterEvent -= OnSidePlayerDetectorTriggerEnter;
    }

    private void OnVehicleDetectorTriggerEnter(Collider other) {
        var otherVehicle = other.GetComponent<Vehicle>();
        if (otherVehicle != null) {
            _currentSpeed = otherVehicle.CurrentSpeed;
        }
    }

    private void OnVehicleDetectorTriggerExit(Collider other) {
        Vehicle otherVehicle = other.GetComponent<Vehicle>();
        if (otherVehicle != null) {
            _currentSpeed = MovingObjectSO.speed;
        }
    }

    private void OnFrontPlayerDetectorTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null) {
            Player.Instance.Die(new RunOverDeathBehaviour());
        }
    }

    private void OnSidePlayerDetectorTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null) {
            Player.Instance.Die(new HitIntoVehicleDeathBehaviour(transform));
        }
    }
}