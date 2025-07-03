using UnityEngine;

public class Balas : MonoBehaviour
{
    [SerializeField] private GameObject bulletHolePrefab;
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 spawnPosition = collision.contacts[0].point + collision.contacts[0].normal * 0.001f; // crea lo posicion del prefab segun su normal del impacto y lo muevo un poco para que no quede pegado 0.001
        Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal); // giro el prefab segun la normal del impacto

        GameObject bulletHole = Instantiate(bulletHolePrefab, spawnPosition , rotation); // prefab - posicion - rotacion
        Destroy(bulletHole, 2f);

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
