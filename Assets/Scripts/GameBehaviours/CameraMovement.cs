using UnityEngine;

public class CameraMovement : GameBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private float smoothSpeed = 6f;
    private Vector3 initialPos;

    private void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null) Debug.LogError("Player not assigned");
        initialPos = transform.position;

    }

    private void LateUpdate()
    {
        var trailPosition = player.position + offset;
        var desiredPosition = new Vector3(initialPos.x, trailPosition.y, trailPosition.z);
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
