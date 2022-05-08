using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int dmg;
    [SerializeField] protected float speed;
    protected Transform target;
    protected Vector3 dir;

    protected void Update()
    {
        if(target != null || dir.magnitude != 0)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {   
            Player player;
            collision.TryGetComponent<Player>(out player);
            player.TakeDmg(dmg);
            player.KnockbackEffect(this.gameObject);
            Destroy(gameObject);
        }
        if(collision.CompareTag("OtherDamageableByEnemies"))
        {
            DamageableObjects attackedObject;
            collision.TryGetComponent<DamageableObjects>(out attackedObject);
            attackedObject.TakeDmg(dmg);
            Destroy(gameObject);
        }
    }

    public void SeekTarget(Transform _target)
    {
        target = _target;
        dir = (target.position - transform.position).normalized;
        Invoke("AutoDestroy", 3);
    }

    protected void AutoDestroy()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 _dir)
    {
        dir = _dir;
        Invoke("AutoDestroy", 3);
    }
}
