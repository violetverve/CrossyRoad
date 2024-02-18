using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class MovingObjectSO : ScriptableObject
{
    [SerializeField] public Transform objectTransform;
    [SerializeField] public float speed;
}