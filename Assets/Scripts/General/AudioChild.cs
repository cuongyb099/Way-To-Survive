using System.Collections;
using Tech.Pooling;
using UnityEngine;

public class AudioChild : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnPool());
    }

    private IEnumerator ReturnPool()
    {
        yield return new WaitUntil (() => !audioSource.isPlaying);
        ObjectPool.Instance.ReturnObjectToPool(gameObject);
    }
}
