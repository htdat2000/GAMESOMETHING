using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Nothing")]
public abstract class Items : ScriptableObject
{
    [Header("Item Info")]
    [SerializeField] protected int _id; public int id { get {return _id;} }
    [SerializeField] protected string _name; public string name_ { get {return _name;} }
    [SerializeField] protected Sprite _icon; public Sprite icon { get{return _icon;} }
    [SerializeField] protected string _description; public string description { get {return _description;} }

    [Header("Item Setup")]
    [SerializeField] protected bool _stackAble; public bool stackAble  { get {return _stackAble;} }
    public bool usable;
    
    [Header("Unity Collider Size")]
    [SerializeField] protected float _sizeX; public float sizeX  { get {return _sizeX;} }
    [SerializeField] protected float _sizeY; public float sizeY { get {return _sizeY;} }

    public abstract void Use(Player player);
}
