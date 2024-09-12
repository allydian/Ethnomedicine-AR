using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlantSO : ScriptableObject
{
    public int ID => GetInstanceID();
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] [field: TextArea] public string Description { get; set; }
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public GameObject Model {get; set;}
}
