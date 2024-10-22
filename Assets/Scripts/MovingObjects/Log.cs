using UnityEngine;
using CrossyRoad.Players;

namespace CrossyRoad.MovingObjects
{
    public class Log : MovingObject
    {
        [SerializeField] private float _safeStayTime = 2f;
        private float _safeStayTimer;
        private bool _isPlayerOnLog = false;

        protected override void Update()
        {
            base.Update();

            UpdatePosition();

            if (_isPlayerOnLog)
            {
                _safeStayTimer += Time.deltaTime;

                if (_safeStayTimer >= _safeStayTime)
                {
                    _safeStayTimer = 0;
                    _isPlayerOnLog = false;
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
                _isPlayerOnLog = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.transform.parent == transform)
            {
                player.transform.SetParent(null);
            }
            _isPlayerOnLog = false;
        }

    }
}