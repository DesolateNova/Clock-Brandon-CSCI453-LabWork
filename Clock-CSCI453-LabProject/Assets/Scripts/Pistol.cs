using UnityEngine;

public class Pistol : Weapon
{

    private void Update()
    {
        /**if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }*/
    }
    public override void Shoot()
    {
        Debug.Log("Kerplow");
        RaycastHit hit;
        Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            bulletCount--;
        }
    }
    public override int Reload(int currentAmmo)
    {
        Debug.Log("Reloading!");
        int bulletsNeeded = maxCapacity - bulletCount;
        bulletCount += bulletsNeeded;
        return currentAmmo - bulletsNeeded;

    }

    public override int GetRemainingBullets()
    {
        return bulletCount;
    }
}
