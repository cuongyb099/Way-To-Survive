using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    [SerializeField] [Range(0f, 100f)] private float defaultDistance = 6f;
    [SerializeField] [Range(0f, 100f)] private float maxDistance = 8f;
    [SerializeField] [Range(0f, 100f)] private float minDistance = 2f;

    [SerializeField] [Range(0f, 100f)] private float smoothing = 4f;
    [SerializeField] [Range(0f, 2f)] private float zoomSensitivity = 1f;

    [SerializeField] private float currentTargetDistance;

    private CinemachineInputProvider inputProvider;
    private CinemachineTransposer transposer;

    private void Awake()
    {
        transposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        currentTargetDistance = defaultDistance;
    }

    // Update is called once per frame
    void Update()
    {
		Zoom();
    }

    private void Zoom()
    {
        float zoomValue = -inputProvider.GetAxisValue(2) * zoomSensitivity;
        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = transposer.m_FollowOffset.magnitude;
        if (currentTargetDistance == currentDistance)
        {
            return;
        }
        float learpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

		transposer.m_FollowOffset = transposer.m_FollowOffset.normalized * learpedZoomValue;
    }
}
