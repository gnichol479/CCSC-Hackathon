using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 4f;

    private bool isOpen = false;
    private bool isMoving = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
openRotation = Quaternion.Euler(0, 0, openAngle) * closedRotation;    }

    public void ToggleDoor()
    {
        if (!isMoving)
        {
            StartCoroutine(RotateDoor());
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