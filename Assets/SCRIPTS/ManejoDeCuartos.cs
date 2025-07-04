using System.Collections.Generic;
using UnityEngine; 


public class ManejoDeCuartos : MonoBehaviour
{
    
    [Header("Pool Settings")]

   
    [SerializeField] private GameObject objetivoPrefab; // Prefab del objetivo que se va a instanciar en cada punto de aparici�n

    [SerializeField] private int tama�oPool = 8; // Cantidad de objetivos que se manejar�n en el sistema de pool

    [SerializeField] private Transform[] puntosRespawnPrefab; // Arreglo de puntos donde se colocar�n los objetivos 
   
    [SerializeField] private GameObject llave;  // Referencia al GameObject de la llave 

    private Queue<GameObject> poolObjetivos = new Queue<GameObject>();  // almacena los objetivos para reutilizarlos

    private int objetivosDestruidos = 0; // Contador que lleva la cantidad de objetivos destruidos

    
    void Awake()
    {
        InitializePool(); 
    }

   
    void Start()
    {
        SpawnTargets(); // Aparecen los objetivos en los puntos indicados

        // Si hay una llave asignada, se desactiva al principio
        if (llave != null)
            llave.SetActive(false);
    }

    // M�todo que crea la pool de objetivos y los deja inactivos hasta que se necesiten
    void InitializePool()
    {
        for (int i = 0; i < tama�oPool; i++) // Se crea cada objetivo hasta completar el tama�o del pool
        {
            GameObject newTarget = Instantiate(objetivoPrefab, transform); // Instancia el prefab como hijo del objeto actual
            newTarget.SetActive(false); // Se desactiva para no aparecer a�n
            newTarget.GetComponent<Objetivos>().SetManager(this); // Se asigna el manager actual al objetivo
            poolObjetivos.Enqueue(newTarget); // Se agrega el objetivo a la cola
        }
    }

    // M�todo que activa los objetivos en las posiciones de respawn
    void SpawnTargets()
    {
        int i = 0;
        foreach (Transform spawnPoint in puntosRespawnPrefab) // Itera por cada punto de aparici�n
        {
            if (poolObjetivos.Count > 0) // Si hay objetivos disponibles en la pool
            {
                GameObject target = poolObjetivos.Dequeue(); // Saca un objetivo de la pool
                target.transform.position = spawnPoint.position; // Lo posiciona en el lugar correcto
                target.transform.rotation = spawnPoint.rotation; // Asigna su rotaci�n
                target.SetActive(true); // Lo activa en la escena
                i++;
            }
        }

        // Reinicia el contador de objetivos destruidos
        objetivosDestruidos = 0;
    }

    // Este m�todo es llamado por el objetivo cuando es destruido
    public void OnTargetDestroyed(GameObject target)
    {
        target.SetActive(false); // Se desactiva el objetivo
        poolObjetivos.Enqueue(target); // Se regresa a la pool para reutilizarlo
        objetivosDestruidos++; // Se incrementa el contador

        // Si todos los objetivos fueron destruidos, se activa la llave
        if (objetivosDestruidos >= tama�oPool && llave != null)
        {
            llave.SetActive(true); // Aparece la llave
        }
    }
}
