using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    [SerializeField] Transform prefab;
    public Transform Prefab {  get { return prefab; } }

    [SerializeField] Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    [SerializeField] string objectName;
    public string ObjectName {  get { return objectName; } }
}
