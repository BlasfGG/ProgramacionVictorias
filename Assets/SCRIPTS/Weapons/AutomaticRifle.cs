using UnityEngine;

public class AutomaticRifle : Weapon
{

    private void Awake()
    {
        fireType = FireType.Automatic;
    }

    public override bool Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            if (isReloading) return false; // Si el arma esta recargando no dispara

            nextFireTime = Time.time + 1f / fireRate;
            currentAmmo--;
            //Bullet();
            Ammotext();
            SmookEffect();
            return true;
        }
        return false;
    }
}
