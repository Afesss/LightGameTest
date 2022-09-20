using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class WinWindow : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [Inject] private SceneLoader sceneLoader;

    public void Activate()
    {
        gameObject.SetActive(true);
        restartButton.onClick.AddListener(sceneLoader.RestartScene);
    }
}
