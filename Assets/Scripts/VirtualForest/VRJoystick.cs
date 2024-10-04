using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// VRJoystick is a custom joystick controller for Unity UI that captures player input in VR or mobile environments.
/// It handles pointer events (drag, press, release) to move the joystick handle, calculates the input direction,
/// and restricts the handle movement within defined boundaries.
/// The class is designed for controlling player movements or other interactions based on joystick input.
/// </summary>
public class VRJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform centre = null; // The RectTransform representing the center of the joystick.
    [SerializeField] private RectTransform handle = null; // The RectTransform representing the joystick's handle (the part the user interacts with).

    private Canvas UICanvas; // Reference to the canvas the joystick UI belongs to.

    protected RectTransform baseRect = null; // The base RectTransform of the joystick UI element.
    protected CanvasGroup bgCanvas = null; // CanvasGroup to control visibility, opacity, and interactivity of the joystick UI.

    public RectTransform boundary; // The boundary within which the joystick can move.

    private Vector2 origin = Vector2.zero; // Stores the original center position of the joystick.
    private Vector2 inputDirection; // Stores the calculated input direction based on joystick movement.
    
    /// <summary>
    /// Initializes the joystick settings, ensuring the handle starts in the center.
    /// </summary>
    private void Awake()
    {
        UICanvas = GetComponentInParent<Canvas>();  // Get the canvas from the parent
        baseRect = GetComponentInParent<RectTransform>();  // Get the RectTransform of the parent UI element
        bgCanvas = GetComponentInParent<CanvasGroup>();  // Get the CanvasGroup for controlling the UI's properties

        // Set the joystick's pivot and anchor points to the center to ensure proper movement
        Vector2 center = new Vector2(0.5f, 0.5f);
        centre.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;  // Set the handle's position to the center
        origin = centre.anchoredPosition;  // Store the original position of the joystick's center
    }

    /// <summary>
    /// Called when the user presses down on the joystick area.
    /// </summary>
    /// <param name="eventData">Pointer event data containing details about the touch or click event.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // If the pointer is within the boundary, move the joystick handle accordingly
        if(RectTransformUtility.RectangleContainsScreenPoint(boundary, eventData.position))
        {
            MoveJoystickHandle(eventData);
        }  
    }

    /// <summary>
    /// Called when the user drags the joystick handle.
    /// </summary>
    /// <param name="eventData">Pointer event data containing details about the drag event.</param>
    public void OnDrag(PointerEventData eventData)
    {
        // If the drag event is within the boundary, update the joystick handle position
        if (RectTransformUtility.RectangleContainsScreenPoint(boundary, eventData.position))
        {
            MoveJoystickHandle(eventData);
        }
    }

    /// <summary>
    /// Called when the user releases the joystick.
    /// Resets the handle position and input direction to their default (centered) state.
    /// </summary>
    /// <param name="eventData">Pointer event data containing details about the release event.</param>
    public void OnPointerUp(PointerEventData eventData)
    {   
        handle.anchoredPosition = Vector2.zero; // Reset the handle to the center
        inputDirection = Vector2.zero; // Reset the input direction
    }

    /// <summary>
    /// Moves the joystick handle based on the user's input.
    /// </summary>
    /// <param name="eventData">Pointer event data containing details about the pointer position.</param>
    private void MoveJoystickHandle(PointerEventData eventData)
    {
        // Calculate the direction from the center to the pointer position
        Vector2 direction = ((Vector2)eventData.position - (Vector2)centre.position).normalized;
        inputDirection = direction; // Update the input direction based on the calculated direction

        // Move the handle within the radius of the joystick
        handle.anchoredPosition = direction * (centre.sizeDelta.x / 2f);

    }

    /// <summary>
    /// Gets the current input direction of the joystick.
    /// The direction is normalized, meaning it will always have a magnitude of 1 in any direction.
    /// </summary>
    /// <returns>Returns the joystick's current input direction as a normalized Vector2.</returns>
    public Vector2 GetInputDirection()
    {
        return inputDirection; // Return the calculated input direction for use in gameplay
    }
}

