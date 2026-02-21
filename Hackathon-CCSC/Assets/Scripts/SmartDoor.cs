using UnityEngine;

public class SmartDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 4f;

    public enum Axis { X, Y, Z }
    public Axis rotationAxis = Axis.Y;

    [Header("Double Door Settings")]
    public SmartDoor pairedDoor; // Assign if this is part of a double door

    private bool isOpen = false;
    private bool isMoving = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;

        Vector3 axisVector = Vector3.zero;

        switch (rotationAxis)
        {
            case Axis.X:
                axisVector = new Vector3(openAngle, 0, 0);
                break;
            case Axis.Y:
                axisVector = new Vector3(0, openAngle, 0);
                break;
            case Axis.Z:
                axisVector = new Vector3(0, 0, openAngle);
                break;
        }

        openRotation = Quaternion.Euler(axisVector) * closedRotation;
    }

    public void ToggleDoor(bool calledFromPair = false)
    {
        if (!isMoving)
        {
            StartCoroutine(RotateDoor());

            // If this is part of a double door, trigger partner
            if (pairedDoor != null && !calledFromPair)
            {
                pairedDoor.ToggleDoor(true);
            }
        }
    }

    private System.Collections.IEnumerator RotateDoor()
    {
        isMoving = true;

        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );

            yield return null;
        }

        transform.localRotation = targetRotation;

        isOpen = !isOpen;
        isMoving = false;
    }
}