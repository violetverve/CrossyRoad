using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrossyRoad.Player;

public class EagleManager : MonoBehaviour
{
    [SerializeField] private MovingObjectSO eagleSO;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Vector3 spawnRotation;
    private float timeForEagleToFlyToPlayer;

    private float flyingTimer;
    private bool isFlying;

    private void Start()
    {
        TimeManager.Instance.TimeWithoutMovingIsUp += Instance_TimeWithoutMovingIsUp;

        float xPlayerPos = Player.Instance.GetXPosition();

        float xEaglePos = spawnPosition.x;

        float offset = 4f;

        float xDistance = xEaglePos - xPlayerPos + offset;

        timeForEagleToFlyToPlayer = xDistance / eagleSO.speed;
    }

    private void Update()
    {

        if (!isFlying)
        {
            return;
        }

        flyingTimer += Time.deltaTime;

        if (flyingTimer > timeForEagleToFlyToPlayer)
        {
            isFlying = false;
            flyingTimer = 0;

            Player.Instance.Die(new CarriedByEagleDeathBehaviour());
        }
    }

    private void Instance_TimeWithoutMovingIsUp(object sender, System.EventArgs e)
    {
        SpawnEagle();
        isFlying = true;

        Player.Instance.Paralyze();
    }

    private void SpawnEagle()
    {
        Vector3 playerPosition = Player.Instance.GetPosition();

        spawnPosition += new Vector3(playerPosition.x, 0, playerPosition.z);

        Instantiate(eagleSO.objectTransform, spawnPosition, Quaternion.Euler(spawnRotation));
    }
}