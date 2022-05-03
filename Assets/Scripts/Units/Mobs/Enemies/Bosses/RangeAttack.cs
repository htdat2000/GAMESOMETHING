using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : BossAttack
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;

    protected override void Attack()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SeekTarget(target.transform);
    }
}
