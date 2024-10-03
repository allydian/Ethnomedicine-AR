using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class AchievementSO : ScriptableObject
{
    public int ID => GetInstanceID();
    [field: SerializeField] [field: TextArea] public string Title { get; set; }
    [field: SerializeField] [field: TextArea] public string Description { get; set; }
    [field: SerializeField] public Sprite Badge { get; set; }
}
