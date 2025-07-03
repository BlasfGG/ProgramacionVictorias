using UnityEngine;
using UnityEngine.UI;


public class InventoryUIHandler : MonoBehaviour
{

    [SerializeField] private GameObject inventoryPanel; 
    [SerializeField] private GameObject uiItem; 
                                                
    [SerializeField] private GameObject instanceDestination; 
    private GameObject[] itemsInstanciados = new GameObject[24]; 
                                                                 

    private InventoryHandler inventario; 
    private bool inventoryOpened = false; 

    private int actualPage = 0;

    private void Start()
    {
        inventario = FindObjectOfType<InventoryHandler>();
        itemsInstanciados = new GameObject[inventario.maxCapacity]; 
    }
    private void Update()
    {
        ToggleInventory();
    }
        


    
    private void ToggleInventory()
    {
        if (OpenInventoryInput())
        {
            
            inventoryOpened = !inventoryOpened;
            inventoryPanel.SetActive(inventoryOpened); 

            if (inventoryOpened)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UpdateInventory(); 
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

    }





    private void UpdateInventory()
    {
        int itemsPerPage = 4; 
        int startIndex = actualPage * itemsPerPage; 
        int endIndex = Mathf.Min(startIndex + itemsPerPage, inventario.Inventario.Count); 

        
        foreach (var item in itemsInstanciados)
        {
            if (item != null)
                item.SetActive(false);
        }

        
        Vector3[] posiciones = new Vector3[]
        {
         new Vector3(-1433, -348, 0),
         new Vector3(-1433, -715, 0),
         new Vector3(-651, -348, 0),
         new Vector3(-651, -706, 0)
        };

        
        for (int i = startIndex; i < endIndex; i++)
        {
            if (uiItem == null)
            {
                Debug.LogError("El prefab uiItem no está asignado.");
                return;
            }

            if (itemsInstanciados[i] == null) 
            {
                GameObject newUiItem = Instantiate(uiItem);
                newUiItem.transform.SetParent(instanceDestination.transform, false); 
                newUiItem.transform.localScale = Vector3.one; 

                
                int localIndex = i % itemsPerPage; 
                newUiItem.transform.localPosition = posiciones[localIndex];

                UIItem uiItemComponent = newUiItem.GetComponent<UIItem>();
                if (uiItemComponent == null)
                {
                    Debug.LogError("El prefab uiItem no tiene el componente UIItem.");
                    Destroy(newUiItem);
                    return;
                }

                uiItemComponent.SetItemInfo(inventario.Inventario[i]);
                itemsInstanciados[i] = newUiItem;
            }

            
            itemsInstanciados[i].SetActive(true);
        }
    }

    public void NextPage() 
    {
        actualPage++;

        if (actualPage >= 2) 
        {
            actualPage = 2;
        }

        int endIndex = Mathf.Min((actualPage * 2) + 2, inventario.maxCapacity); 

        for (int i = (actualPage - 1) * 2; i < endIndex - 2; i++) 
        {
            itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 2; i < endIndex; i++) 
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }


    public void PreviousPage() 
    {
        actualPage--; 

        if (actualPage <= 0) 
        {
            actualPage = 0;
        }
        int endIndex = Mathf.Min((actualPage * 2 + 2), inventario.maxCapacity); 

        for (int i = (actualPage + 1) * 2; i < endIndex + 2; i++) 
        {
            itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 2; i < endIndex; i++) 
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }


    private bool OpenInventoryInput()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

}

