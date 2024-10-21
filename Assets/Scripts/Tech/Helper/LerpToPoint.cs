using UnityEngine;

namespace ResilientCore
{
    public class LerpToPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform TargetPoint { get; private set; }
		[field: SerializeField] public float LerpSpeed { get; private set; } = 25f;
       
        void Update()
        {
            transform.position =
                Vector3.Lerp(transform.position, TargetPoint.position, LerpSpeed * Time.deltaTime);
        }
	}
}
