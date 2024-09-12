using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu]
public class HintSO : ScriptableObject
{
    //Details for parsing into the panel when the hint is pressed
    public string hintName;
    public Sprite hintPanelSprite;
    [TextArea(5,10)]
    public string hintDescription;
    public VideoClip hintVideo;

    //Sprites for the hint container
    public Sprite hintNotFound;
    public Sprite hintFound;
}
