
using System.Collections;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int llavesNecesarias; // Número de objetos necesarios para desbloquear la puerta
    [SerializeField] private TextMeshProUGUI txtAviso; // Texto de aviso que se mostrará al desbloquear la puerta
    private bool estaDesbloqueada = false; // Estado de la puerta, si está desbloqueada o no

    private InventoryHandler playerInventory; // Referencia al inventario del jugador

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    }


    void Update()
    {
        if (playerInventory.Inventario.Count >= llavesNecesarias) //
        {
            StartCoroutine(PuertaDesbloqueada()); // Muestra el mensaje de aviso
        }
        if(playerInventory.Inventario.Count == 10)
        {
            txtAviso.text = "Se ha abierto la Salida!"; // Cambia el texto de aviso al desbloquear la puerta
        }
    }


    private IEnumerator PuertaDesbloqueada() // Corrutina para mostrar el aviso y luego ocultarlo
    {
        if (!estaDesbloqueada && txtAviso != null) // (!) hace que el bool sea true e indique que la puerta ya está desbloqueada y // no se vuelva a mostrar el mensaje de aviso si ya está desbloqueada
        {
            estaDesbloqueada = true;
            txtAviso.gameObject.SetActive(true); // Muestra el texto de aviso
            yield return new WaitForSeconds(1.5f);
            txtAviso.gameObject.SetActive(false); // Oculta el texto de aviso
            transform.gameObject.SetActive(false); // Desactiva la puerta
        }
    }
}
