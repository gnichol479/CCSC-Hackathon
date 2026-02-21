using UnityEngine;

public class SimpleDoorAction : MonoBehaviour
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
                SimpleDoor door = hit.transform.GetComponentInParent<SimpleDoor>();

                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }
    }
}