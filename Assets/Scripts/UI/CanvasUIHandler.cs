using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUIHandler : MonoBehaviour
{
    public bool StopTime = false;
    protected virtual void OnEnable()
    {
        PlayerInput.Instance.InputActions.BasicAction.Disable();
        if(StopTime)
            Time.timeScale = 0;
    }
    protected virtual void OnDisable()
    {
        PlayerInput.Instance.InputActions.BasicAction.Enable();
        if (StopTime)
            Time.timeScale = 1f;
    }
}
