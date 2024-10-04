using UnityEngine;

/// <summary>
/// Updates the camera's position and rotation based on joystick input each frame.
/// The camera rotates vertically and horizontally, with vertical rotation clamped between -90 and 0 degrees.
/// </summary>
public class VRCamera : MonoBehaviour
{
    public VRJoystick joystick; // Reference to the VRJoystick used to control the camera movement.
    public float speed = 15f; // Speed multiplier for the camera's movement and rotation.
    private float XMove; // Tracks horizontal movement based on joystick input.
    private float YMove; // Tracks vertical movement based on joystick input.
    private float XRotate; // Tracks horizontal rotation of the camera.
    private float YRotate; // Tracks vertical rotation of the camera.

    /// <summary>
    /// Updates the camera's position and rotation based on joystick input each frame.
    /// The camera rotates vertically and horizontally, with vertical rotation clamped between -90 and 0 degrees.
    /// </summary>
    void Update()
    { 
        Vector2 inputDirection = joystick.GetInputDirection();// Get input direction from the joystick.

        // Calculate movement based on input direction, speed, and deltaTime.
        XMove = inputDirection.x * speed * Time.deltaTime;
        YMove = inputDirection.y * speed * Time.deltaTime;

        // Adjust the rotation along the X and Y axes.
        XRotate -= YMove;
        XRotate = Mathf.Clamp(XRotate, -90f, 0); // Clamp the vertical rotation to prevent flipping.
        YRotate += XMove;

        transform.localRotation = Quaternion.Euler(XRotate, YRotate, 0); // Apply the calculated rotation to the camera.
    }
}
