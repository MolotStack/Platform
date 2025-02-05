using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpriteAnimationScriptableObject", order = 1)]
public class SpriteAnimationScriptableObject : ScriptableObject
{
    //public string Name;
    public int FrameRate;

    public List<Sprite> Sprites;

    public bool IsLooping;
    public bool AllowNext;
}
