using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName ="",menuName ="KitchenObject")]
public class KitchenObjects : ScriptableObject
{
    public Transform Prefab;
    public Sprite Spritee;
    
    public string ObjectName; 
}
