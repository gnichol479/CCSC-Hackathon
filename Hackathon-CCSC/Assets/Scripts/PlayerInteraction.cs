using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    private GameObject currentInteractable;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position,
                            playerCamera.transform.forward,
                            out hit,
                            interactDistance))
        {
if (hit.collider.CompareTag("Interactable") || 
    hit.collider.CompareTag("Blender"))            {
                currentInteractable = hit.collider.gameObject;

                Interactable interactable = currentInteractable.GetComponent<Interactable>();

                interactUI.SetActive(true);
                interactText.text = "Press E to " + interactable.interactionName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    HandleInteraction(currentInteractable);
                }

                return;
            }
        }

        interactUI.SetActive(false);
        currentInteractable = null;
    }

    private GameObject heldItem;
    public Transform holdPoint;
void HandleInteraction(GameObject obj)
{
    if (obj.CompareTag("Blender"))
    {
        if (heldItem != null)
        {
            DepositIntoBlender();
        }
        return;
    }

    if (heldItem == null)
    {
        PickUp(obj);
    }
    else
    {
        if (obj != heldItem)
        {
            Drop();
            PickUp(obj);
        }
        else
        {
            Drop();
        }
    }
}

void PickUp(GameObject obj)
{
    heldItem = obj;

    obj.GetComponent<Rigidbody>().isKinematic = true;
    obj.transform.SetParent(holdPoint);
    obj.transform.localPosition = Vector3.zero;
}

void Drop()
{
    heldItem.GetComponent<Rigidbody>().isKinematic = false;
    heldItem.transform.SetParent(null);
    heldItem = null;
}
void DepositIntoBlender()
{
    Debug.Log("Deposited: " + heldItem.name);

    Destroy(heldItem); // or disable instead

    heldItem = null;
}
}