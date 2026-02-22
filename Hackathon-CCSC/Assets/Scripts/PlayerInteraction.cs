using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    private GameObject currentInteractable;

    [Header("Computer UI")]
public GameObject computerUIPanel;
public MonoBehaviour playerController;

    void Update()
{
    RaycastHit hit;

    
    if (playerCamera != null &&
        Physics.Raycast(playerCamera.transform.position,
                        playerCamera.transform.forward,
                        out hit,
                        interactDistance))
    {

        if (hit.collider.CompareTag("Computer"))
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        OpenComputer();
    }
}
        if (hit.collider.CompareTag("Interactable") ||
            hit.collider.CompareTag("Blender"))
        {
            currentInteractable = hit.collider.gameObject;

            interactUI.SetActive(true);

            if (hit.collider.CompareTag("Blender"))
            {
                interactText.text = "Press E to Use Blender";
            }
            else
            {
                Interactable interactable = currentInteractable.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactText.text = "Press E to " + interactable.interactionName;
                }
                else
                {
                    interactText.text = "Press E to Interact";
                }
            }

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
void OpenComputer()
{
    computerUIPanel.SetActive(true);

    playerController.enabled = false;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

public void CloseComputer()
{
    computerUIPanel.SetActive(false);

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}
}