using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasUIHandler : MonoBehaviour
{
    public bool StopTime = false;
    private void OnEnable()
    {
        PlayerInput.Instance.InputActions.BasicAction.Disable();
        if(StopTime)
            Time.timeScale = 0;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.InputActions.BasicAction.Enable();
        if (StopTime)
            Time.timeScale = 1f;
    }
}
