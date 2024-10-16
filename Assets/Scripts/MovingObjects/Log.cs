using UnityEngine;
using CrossyRoad.Player;

public class Log : MovingObject
{
    [SerializeField] private float safeStayTime = 2f;
    private float safeStayTimer;
    private bool playerOnLog = false;

    private void Update()
    {
        UpdatePosition();

        if (playerOnLog)
        {
            safeStayTimer += Time.deltaTime;

            if (safeStayTimer >= safeStayTime)
            {
                safeStayTimer = 0;
                playerOnLog = false;
                Player.Instance.Die(new CarriedByStreamDeathBehaviour());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.transform.SetParent(transform);
            playerOnLog = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.transform.parent == transform)
        {
            player.transform.SetParent(null);
        }
        playerOnLog = false;
    }

}
