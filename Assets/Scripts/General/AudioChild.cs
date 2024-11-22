using System.Collections;
using Tech.Pooling;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioChild : MonoBehaviour
{
    public AudioSource Source {get; private set;}
    private Coroutine _coroutine;
    
    private void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    public void WaitToReturnPool()
    {
        _coroutine = StartCoroutine(ReturnPool());
    }

    private IEnumerator ReturnPool()
    {
        yield return new WaitUntil (() => !Source.isPlaying);
        ObjectPool.Instance.ReturnObjectToPool(gameObject);
    }
}
