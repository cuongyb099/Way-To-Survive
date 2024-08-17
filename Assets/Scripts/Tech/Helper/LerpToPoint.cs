
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResilientCore
{
    public class LerpToPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform TargetPoint { get; private set; }
		[field: SerializeField] public float SwitchBlendValue { get; private set; } = 0.1f;
       
        void Update()
        {
            //transform.position = Vector3.Lerp(transform.position, TargetPoint.transform.position, SwitchBlendValue);
            transform.DOMove(TargetPoint.position, SwitchBlendValue);
        }
	}
}
