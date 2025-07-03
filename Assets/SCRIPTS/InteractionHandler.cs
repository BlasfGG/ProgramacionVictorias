
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] protected internal float range; 
    [SerializeField] protected LayerMask detection; 

    protected RaycastHit target;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward * range, out target, range, detection))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                target.collider.GetComponent<IInteractable>().Interact();
                Destroy(target.collider.gameObject); 
            }
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * range);
    }

}

