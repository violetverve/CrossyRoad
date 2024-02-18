using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class CollactibleObjectSO : ScriptableObject {
    [SerializeField] public Transform objectTransform;
    [SerializeField] public string objectName;
    [SerializeField] public Sprite objectSprite;
}