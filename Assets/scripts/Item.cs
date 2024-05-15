using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    
    [Header("Only gameplay")]
    public string name;
    public ItemType type;
    public ActionType actionType;
    [Header("Only UI")]
    public bool stackable = true;
    [Header("3D Model")]
    public GameObject prefabToSpawn;
    [Header("2D Icon")]
    public Sprite image;
}

public enum ItemType
{
    Tool,
    Material
}

public enum ActionType
{
    Mine,
    Chop,
    Smash
}
