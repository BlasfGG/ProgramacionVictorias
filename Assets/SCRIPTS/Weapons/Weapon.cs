
using System.Collections;
using TMPro;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public enum FireType
    {
        Automatic,
        SemiAutomatic
    }

    public FireType fireType;

    public string weaponName;

    public int damage; // daño del arma
    public float range; // alcance del arma
    public float fireRate; // cadencia del arma
    public float accuracy; // punteria: Que tanto se mueve el arma o dispara hacia donde apuntas
    public float timeToReload; // tiempo de recarga del arma

    public int currentAmmo; // municion de mi cargador actual
    public int currentMaxAmmo; // capacidad maxima de el cargador
    public int ammo; // municion disponible en la reserva
    public int maxAmmo; // capacidad maxima de mi reserva

    [HideInInspector] public float nextFireTime = 0f; // tiempo entre disparos
    [HideInInspector] public bool isReloading = false; // si el arma esta recargando

    [Header("Bullet")]
    //public GameObject bulletPrefab; // prefab de la bala
    public Transform shootPoint; // punto de disparo
    [SerializeField] private GameObject smookeEffect; // efecto de humo al disparar

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    public abstract bool Shoot();

    public virtual void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < currentMaxAmmo && ammo > 0)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    //protected void Bullet()
    //{
    //    GameObject bulletInstance = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    //    Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
    //    bulletRb.AddForce(shootPoint.forward * range, ForceMode.Impulse); // Instancia la bala hacia adelante
    //    Destroy(bulletInstance, 1f); // Destruye la bala despues de 2 segundos
    //}

    protected void Ammotext()
    {
        ammoText.text = $"{currentAmmo}/{ammo}";
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        Debug.Log("Recargando...");
        yield return new WaitForSeconds(timeToReload);

        int bulletsNeeded = currentMaxAmmo - currentAmmo;
        int bulletsToReload = Mathf.Min(bulletsNeeded, ammo);

        currentAmmo += bulletsToReload;
        ammo -= bulletsToReload;

        Debug.Log($"Recarga completa: {currentAmmo}/{ammo}");

        Ammotext();

        isReloading = false;
    }

    public void SmookEffect()
    {
        GameObject smookeInstance = Instantiate(smookeEffect, shootPoint.position, shootPoint.rotation);
        ParticleSystem ps = smookeInstance.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(smookeInstance, 0.3f);
    }
}


