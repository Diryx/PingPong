using UnityEngine;
using Zenject;

public class Installers : MonoInstaller
{
    [SerializeField] private GoalTrigger _goalTrigger;
    [SerializeField] private Counter _counter;
    [SerializeField] private BallController _ballController;
    [SerializeField] private FirstHudView _firstHudView;

    public override void InstallBindings()
    {
        Container.Bind<Counter>().FromInstance(_counter).AsSingle();
        Container.Bind<GoalTrigger>().FromInstance(_goalTrigger).AsSingle();
        Container.Bind<BallController>().FromInstance(_ballController).AsSingle();
        Container.Bind<FirstHudView>().FromInstance(_firstHudView).AsSingle();
    }
}