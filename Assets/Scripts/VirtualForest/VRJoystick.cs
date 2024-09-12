using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform centre = null;
    [SerializeField] private RectTransform handle = null;
    private Canvas UICanvas;
    protected RectTransform baseRect = null;
    protected CanvasGroup bgCanvas = null;
    public RectTransform boundary;
    private Vector2 origin = Vector2.zero;
    private Vector2 inputDirection;
    
    private void Awake(){
        UICanvas = GetComponentInParent<Canvas>();
        baseRect = GetComponentInParent<RectTransform>();
        bgCanvas = GetComponentInParent<CanvasGroup>();

        Vector2 center = new Vector2(0.5f, 0.5f);
        centre.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
        origin = centre.anchoredPosition;     
    }

    public void OnPointerDown(PointerEventData eventData){
        if(RectTransformUtility.RectangleContainsScreenPoint(boundary, eventData.position)){
        MoveJoystickHandle(eventData);
        }  
    }

    public void OnDrag(PointerEventData eventData){
        if (RectTransformUtility.RectangleContainsScreenPoint(boundary, eventData.position))
        {
            MoveJoystickHandle(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData){   
        handle.anchoredPosition = Vector2.zero;
        inputDirection = Vector2.zero;
    }

    private void MoveJoystickHandle(PointerEventData eventData){
        Vector2 direction = ((Vector2)eventData.position - (Vector2)centre.position).normalized;
        inputDirection = direction;
        handle.anchoredPosition = direction * (centre.sizeDelta.x / 2f);

    }

    public Vector2 GetInputDirection()
    {
        return inputDirection;
    }
}

