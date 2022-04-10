using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationDampener = 0.625f;

    private CharacterController controller;
    private Vector3 motion = Vector3.forward;
    private float horizontalInput = 0f;
    

    private void Start()
    {
        if (!TryGetComponent(out controller))
            controller = gameObject.AddComponent<CharacterController>();

        EventManager.Instance.OnInputDetected += (e)=> horizontalInput = e;
    }
  
    private void Update()
    {
        motion = Vector3.forward;
        motion *= speed * Time.deltaTime;
        motion.x = horizontalInput;
        controller.Move(motion);
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(speed * rotationDampener, 0f, -controller.velocity.x * rotationDampener, Space.World);
    }
}
