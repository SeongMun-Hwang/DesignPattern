using System;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }
    StateController player;

    public IdleState idleState;
    public SpinState spinState;
    public AttackState attackState;
    public RunState runState;
    public DeathState deathState;
    public ProductionState productionState;

    public event Action<IState> stateChanged; //event를 붙히면 변수에 변수들을 쌓음
    //Action<IState> 파라미터로 IState타입을 건네주는 void 함수. 파라미터 없이 하려면 Action

    public StateMachine(StateController player)
    {
        this.player = player;
        idleState = new IdleState(player);
        runState = new RunState(player);
        deathState = new DeathState(player);
        spinState = new SpinState(player);
        attackState = new AttackState(player);
        productionState = new ProductionState(player);
    }
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();

        stateChanged?.Invoke(state);
    }
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState= nextState;
        CurrentState.Enter();

        stateChanged?.Invoke(CurrentState);
    }
    public void Execute()
    {
        CurrentState.Execute();
    }
}
