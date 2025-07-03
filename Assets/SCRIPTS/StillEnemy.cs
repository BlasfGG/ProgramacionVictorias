using UnityEngine;

public class StillEnemy : MonoBehaviour
{
    [Header("Configuración de Explosión")]
    [Header("Configuración de Vida")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private int damageToPlayer = 20;
    [SerializeField] private float pushBackForce = 5f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private ParticleSystem explosionEffect;

    private int currentHealth;
    private bool hasExploded = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (hasExploded) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                PlayerHealth player = hitCollider.GetComponent<PlayerHealth>();
                if (player != null)
                {

                    Explode();
                    break;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (hasExploded) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Explode();
        }
    }


    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Debug.Log("Aplicando daño al jugador");
                    ParticleExplosion();
                    playerHealth.TakeDamage(damageToPlayer);
                    Destroy(gameObject);


                    Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 direction = (hitCollider.transform.position - transform.position).normalized;
                        direction.y = 0;
                        rb.AddForce(direction * pushBackForce, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void ParticleExplosion()
    {

        Debug.Log("Particulas");
        ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(explosion.gameObject, 1f);



    }



}




