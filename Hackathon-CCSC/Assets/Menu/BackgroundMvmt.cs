using UnityEngine;

public class BackgroundPan : MonoBehaviour
{
    public float panSpeed = 2f;
    public float panAmount = 100f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * panSpeed, panAmount);
        transform.localPosition = startPos + new Vector3(offset, 0, 0);
    }
}