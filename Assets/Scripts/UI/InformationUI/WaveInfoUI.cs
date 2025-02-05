using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textWave;
    [SerializeField] private TextMeshProUGUI textTime;
    
    [SerializeField] private AudioClip waveClip;
    [SerializeField] private AudioClip[] buffClips;
    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private string GetTimeString()
    {
        string s = "";
        return s;
    }
}
