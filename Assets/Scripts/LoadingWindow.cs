using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class LoadingWindow : MonoBehaviour
{
    public event System.Action Loaded;
    [Inject] private ImageLoader imageLoader;

    private void Start()
    {
        StartCoroutine(WaitLoading());
    }

    private IEnumerator WaitLoading()
    {
        while (!imageLoader.Loaded)
        {
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
        Loaded?.Invoke();
    }
}
