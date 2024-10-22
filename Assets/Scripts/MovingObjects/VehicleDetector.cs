using System.Linq;
using UnityEngine;

namespace CrossyRoad.MovingObjects
{
    public class VehicleDetector
    {
        private const string VehicleTag = "Vehicle";
        private float _vehicleSize = 0.1f;

        public Transform GetVehicleTransform(Vector3 position)
        {
            var detectionBox = Vector3.one * _vehicleSize;
            Collider[] hitColliders = Physics.OverlapBox(position, detectionBox);
            var vehicleCollider = hitColliders.FirstOrDefault(hitCollider => hitCollider.CompareTag(VehicleTag));
            return vehicleCollider?.transform;
        }
    }
}
