using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.EnhancedTouch;
using Unity.Services.Analytics.Internal;

public class FloatingJoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    [SerializeField]
    GameObject joystick;

    OnScreenStick screenStick;
    RectTransform mainRect;
    RectTransform joystickRect;

    private void Awake()
    {
        screenStick = joystick.GetComponentInChildren<OnScreenStick>();
        mainRect = GetComponent<RectTransform>();
        joystickRect = joystick.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        ExecuteEvents.dragHandler(screenStick, eventData);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mainRect, eventData.pressPosition, eventData.pressEventCamera, out localPosition);
        joystickRect.localPosition = localPosition;
        ExecuteEvents.pointerDownHandler(screenStick, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ExecuteEvents.pointerUpHandler(screenStick, eventData);
    }
}
