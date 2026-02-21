using UnityEngine;

public class SmartDoorAction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position,
                                playerCamera.transform.forward,
                                out hit,
                                interactDistance))
            {
                SmartDoor door = hit.transform.GetComponentInParent<SmartDoor>();

                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}