using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

/// <summary>
/// This ScriptableObject represents an individual plant with its associated data, such as name, description, image, and 3D model.
/// </summary>
public class PlantSO : ScriptableObject
{
    public int ID => GetInstanceID(); // Unique ID for the plant, derived from the instance ID of the ScriptableObject.
    [field: SerializeField] public string Name { get; set; } // The name of the plant.
    [field: SerializeField] [field: TextArea] public string Description { get; set; } // A detailed description of the plant, providing additional information or characteristics.
    [field: SerializeField] public Sprite Image { get; set; } // The sprite representing the plant's image, typically used for UI display.
    [field: SerializeField] public GameObject Model {get; set;} // The 3D model of the plant, which can be used for AR/VR or game-world rendering.
}
