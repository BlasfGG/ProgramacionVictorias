using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private List<Item> inventario;
    public List<Item> Inventario { get => inventario; }

    public int indice;

    public int maxCapacity = 24;

    public int numero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TirarObjeto();
        }
    }

    public void AgregarObjeto(Item item)
    {
        inventario.Add(item);
    }

    public void TirarObjeto()
    {
        Instantiate(inventario[indice]._prefab, transform.position, transform.rotation);
        inventario.RemoveAt(indice);
    }
}
