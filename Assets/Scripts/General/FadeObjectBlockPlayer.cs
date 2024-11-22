using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class FadeObjectBlockPlayer : MonoBehaviour
{
   private Camera _mainCam;
   private Transform _target;
   
   [SerializeField] private LayerMask _maskObject;
   [SerializeField] private Vector3 _playerOffset;
   [SerializeField] private Material _fadeMaterial;
   [SerializeField] private int _maxMaterialToFade = 10;
   [SerializeField] private float _fadeTime = 0.3f;
   [SerializeField] private float _alphaValue = 0.1f;
   private readonly Stack<Material> _poolMaterials = new ();
   
   private readonly List<FadeObject> _objectsNotRemove = new();
   private readonly List<FadeObject> _blockingViewObjects = new ();
   private readonly Dictionary<FadeObject, Tween> _cullingObjects = new ();
   private readonly RaycastHit[] _hitObjects = new RaycastHit[10];
   
   private void Awake()
   {
      _mainCam = Camera.main;

      for (int i = 0; i < _maxMaterialToFade; i++)
      {
         _poolMaterials.Push(Instantiate(_fadeMaterial));
      }
   }

   private void Start()
   {
       _target = GameManager.Instance.Player.transform;
   }

   private void Update()
   {
      ChekingObjectFace();
   }

   private Vector3 rayDirection;
   private void ChekingObjectFace()
   {
      var camPos = _mainCam.transform.position;
      rayDirection = (_target.position + _playerOffset) - camPos;

      var hitAmount = Physics.RaycastNonAlloc(_mainCam.transform.position, rayDirection.normalized,
         _hitObjects, rayDirection.magnitude, _maskObject);
      
      _objectsNotRemove.Clear();

      if (hitAmount > 0)
      {
         foreach (var hit in _hitObjects)
         {
            if(!hit.collider) continue;
            if(!hit.collider.TryGetComponent(out FadeObject fadeObject)) continue;
            
            if (!_blockingViewObjects.Contains(fadeObject))
            {
               _blockingViewObjects.Add(fadeObject);
               
               if(fadeObject.MaterialCount <= 0) continue;
               
               fadeObject.DoFade(_alphaValue, _fadeTime, _poolMaterials, _fadeMaterial);
               _objectsNotRemove.Add(fadeObject);
               continue;
            }
            if(!_objectsNotRemove.Contains(fadeObject))
            _objectsNotRemove.Add(fadeObject);
         }
      }
      
      var toRemove = _blockingViewObjects.Except(_objectsNotRemove).ToList();

      foreach (var obj in toRemove)
      {
         obj.ResetObject(_fadeTime, _poolMaterials);
         _blockingViewObjects.Remove(obj);
      }
      
      /*for (int i = _blockingViewObjects.Count - 1; i >= 0; i--)
      {
         for (int j = _objectsNotRemove.Count - 1; j >= 0; j--)
         {
            if (_blockingViewObjects[i] == _objectsNotRemove[j])
            {
               _objectsNotRemove.RemoveAt(j);
               continue;
            }

            _blockingViewObjects[i].ResetObject(_fadeTime, _poolMaterials);
            _blockingViewObjects.RemoveAt(i);
         }
      }*/
   }

#if UNITY_EDITOR
   [Header("Debug")]
   public bool DebugMode;
   private void OnDrawGizmos()
   {
      if(!Application.isPlaying || !DebugMode) return;
        
      Gizmos.color = Color.red;
      Gizmos.DrawLine(_mainCam.transform.position, _target.position + _playerOffset);
   }
#endif
}
