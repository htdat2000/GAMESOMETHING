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

    protected override void BossSkill()
    {
        Vector3 dir1 = new Vector3 (1, 0, 0);
        GameObject bulletGO1 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet1 = bulletGO1.GetComponent<Bullet>();
        bullet1.SetDirection(dir1);

        Vector3 dir2 = new Vector3 (-1, 0, 0);
        GameObject bulletGO2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet2 = bulletGO2.GetComponent<Bullet>();
        bullet2.SetDirection(dir2);

        Vector3 dir3 = new Vector3 (0, 1, 0);
        GameObject bulletGO3 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet3 = bulletGO3.GetComponent<Bullet>();
        bullet3.SetDirection(dir3);

        Vector3 dir4 = new Vector3 (0, -1, 0);
        GameObject bulletGO4 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet4 = bulletGO4.GetComponent<Bullet>();
        bullet4.SetDirection(dir4);
    }

}
