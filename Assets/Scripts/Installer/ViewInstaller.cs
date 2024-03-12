using UI;
using UnityEngine;
using Zenject;

public class ViewInstaller : MonoInstaller
{
    [SerializeField] private EndGamePopUpView _endGamePopUpView;

    public override void InstallBindings()
    {
        Container.Bind<UITextView>().FromComponentsInHierarchy().AsCached();
        Container.BindInterfacesAndSelfTo<GameInfoController>().AsSingle();
        Container.Bind<EndGamePopUpView>().FromInstance(_endGamePopUpView).AsSingle();
        Container.BindInterfacesAndSelfTo<EndGamePopUpController>().AsSingle();
    }
}