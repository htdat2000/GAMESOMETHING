using UnityEngine;

public abstract class DamageableObjects : MonoBehaviour
{
    [SerializeField] protected int _id;
    [SerializeField] protected string _name;
    [Header("Attributes")]
    protected int hp; 
    protected int _dmg; public int dmg { get { return _dmg; } set { _dmg = value; } }

    public abstract void TakeDmg(int dmg);
    protected abstract void HPEqual0();
}
