using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ImageLoader imageLoader;
    [SerializeField] private SceneLoader sceneLoader;
    public override void InstallBindings()
    {
        Container.BindInstance(imageLoader);
        Container.BindInstance(sceneLoader);
    }
}