using UnityEngine;

namespace CrossyRoad.MovingObjects
{
    [CreateAssetMenu()]
    public class MovingObjectSO : ScriptableObject
    {
        [SerializeField] public Transform objectTransform;
        [SerializeField] public float speed;
    }
}
