using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int dmg;
    [SerializeField] protected float speed;
    public Transform target;

    protected void Update()
    {
        if(target != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
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
}
