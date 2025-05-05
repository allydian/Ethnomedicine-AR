using Lean.Touch;
using UnityEngine;

public class OneFingerRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX = -1f; // vertical (pitch)
    [SerializeField] private float rotationSpeedY = -1f; // horizontal (yaw)
    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }

    private void HandleFingerUpdate(LeanFinger finger)
    {
        if (LeanTouch.Fingers.Count == 1)
        {
            float deltaX = finger.ScaledDelta.x;
            float deltaY = finger.ScaledDelta.y;

            // Rotate: vertical drag → rotate X, horizontal drag → rotate Y
            transform.Rotate(-deltaY * rotationSpeedX, deltaX * rotationSpeedY, 0f, Space.World);
        }

        Debug.Log("Touch count: " + Input.touchCount);

    }
}
