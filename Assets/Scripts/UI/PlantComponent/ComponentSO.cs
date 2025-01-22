using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

/// <summary>
/// This ScriptableObject represents an individual plant with its associated data, such as name, description, image, and 3D model.
/// </summary>
public class ComponentSO : ScriptableObject
{
    public int ID => GetInstanceID(); // Unique ID for the plant, derived from the instance ID of the ScriptableObject.
    [field: SerializeField] public string Name { get; set; } // The name of the plant.
    [field: SerializeField][field: TextArea] public string Description { get; set; } // A detailed description of the plant, providing additional information or characteristics.
}
