using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    
    [Header("Only gameplay")]
    public string itemName;
    public ItemType type;
    public ActionType actionType;
    public ItemIdentificator identificator;
    [HideInInspector] public int maxStackCount = 4;
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
    Smash,
    Eat,
    Light,
    End
}

public enum ItemIdentificator
{
    Axe,
    Pickaxe,
    Sword,
    Stick,
    Stone,
    Meat,
    Wood,
    CookedMeat,
    Torch,
    Gas
}