using UnityEngine;

public class VRCamera : MonoBehaviour
{
    public VRJoystick joystick;
    public float speed = 15f;
    private float XMove;
    private float YMove;
    private float XRotate;
    private float YRotate;

    void Update()
    { 
        Vector2 inputDirection = joystick.GetInputDirection();
        XMove = inputDirection.x * speed * Time.deltaTime;
        YMove = inputDirection.y * speed * Time.deltaTime;
        XRotate -= YMove;
        XRotate = Mathf.Clamp(XRotate, -90f, 0);
        YRotate += XMove;

        transform.localRotation = Quaternion.Euler(XRotate, YRotate, 0);
    }
}
