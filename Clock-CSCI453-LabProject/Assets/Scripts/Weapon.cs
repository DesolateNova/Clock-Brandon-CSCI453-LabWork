using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected int maxCapacity;

    public virtual void Shoot()
    {

    }
    public virtual int Reload(int currentAmmo)
    {
        return 0;
    }

    public virtual int GetRemainingBullets()
    {
        return 0;
    }



}
