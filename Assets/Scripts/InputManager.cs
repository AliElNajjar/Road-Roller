using UnityEngine;

public class InputManager : MonoBehaviour
{

    private void Update()
    {
#if UNITY_EDITOR
        GetMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        GetTouchInput();
#endif
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            EventManager.Instance.OnInputDetected?.Invoke(Input.GetAxis("Mouse X"));
        }

        if (Input.GetMouseButtonUp(0))
        {
            EventManager.Instance.OnInputDetected?.Invoke(0f);
        }

        
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                var deltaInput = touch.deltaPosition / (Screen.width * touch.deltaTime);
                EventManager.Instance.OnInputDetected?.Invoke(deltaInput.x);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Stationary)
            {
                EventManager.Instance.OnInputDetected?.Invoke(0f);
            }
            
        }
    }
}
