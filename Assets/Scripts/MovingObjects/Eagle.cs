using UnityEngine;

namespace CrossyRoad.MovingObjects
{
    public class Eagle : MovingObject
    {
        private const string IS_FLYING = "isFlying";
        private Vector3 _eagleDirection = new Vector3(-1, 0, 0);

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            SetDirection(_eagleDirection);

            animator.SetBool(IS_FLYING, true);
        }
    }
}