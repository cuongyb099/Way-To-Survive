using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSelectionUI : MonoBehaviour
{
    public GameObject WheelSelectionCanvas;
    private void Awake()
    {
        InputEvent.OnInputSwitchGuns += ToggleSelectionUI;
    }

    private void OnDestroy()
    {
        InputEvent.OnInputSwitchGuns -= ToggleSelectionUI;
    }

    public void ToggleSelectionUI()
    {
        WheelSelectionCanvas.SetActive(!WheelSelectionCanvas.activeSelf);
    }
    
}
