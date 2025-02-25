using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected int maxCapacity;
    [SerializeField] protected PlayerController playerController;

    protected void Awake()
    {
        playerController = GameObject.Find("--- Player ---").GetComponent<PlayerController>();
        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
    }

    public virtual void Shoot()
    {
        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
        if (bulletCount <= 0)
            Reload();
    }
    public virtual void Reload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    public virtual int GetRemainingBullets()
    {
        return 0;
    }
    
    protected IEnumerator ReloadCoroutine()
    {
        if (playerController.SpareRounds >= maxCapacity)
        {
            bulletCount = maxCapacity;
            playerController.SpareRounds -= maxCapacity;
        }
        else
        {
            bulletCount = playerController.SpareRounds;
            playerController.SpareRounds = 0;
        }

        yield return new WaitForSeconds(1f);

        UIManager.Instance.UpdateAmmoUI(bulletCount, playerController.SpareRounds);
    }


}
