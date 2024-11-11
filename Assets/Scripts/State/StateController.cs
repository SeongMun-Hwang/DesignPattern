using UnityEngine;

public class StateController : MonoBehaviour
{
    public StateMachine stateMachine;
    private void Awake()
    {
        stateMachine = new StateMachine(this);
    }
    private void OnEnable()
    {
        EventBus.Instance.Subscribe(GlobalEvent.AllIdle, OnIdle);
        EventBus.Instance.Subscribe(GlobalEvent.AllAttack, OnAttack);
        EventBus.Instance.Subscribe(GlobalEvent.AllSpin, OnSpin);
        EventBus.Instance.Subscribe(GlobalEvent.AllDeath, OnDeath);
        EventBus.Instance.Subscribe(GlobalEvent.AllRun, OnRun);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe(GlobalEvent.AllIdle, OnIdle);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllAttack, OnAttack);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllSpin, OnSpin);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllDeath, OnDeath);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllRun, OnRun);
    }
    void Start()
    {
        stateMachine.Initialize(stateMachine.idleState);
    }
    void Update()
    {
        stateMachine.Execute();
    }
    public void OnIdle()
    {
        stateMachine.TransitionTo(stateMachine.idleState);
    }
    public void OnAttack()
    {
        stateMachine.TransitionTo(stateMachine.attackState);
    }
    public void OnSpin()
    {
        stateMachine.TransitionTo(stateMachine.spinState);
    }
    public void OnDeath()
    {
        stateMachine.TransitionTo(stateMachine.deathState);
    }
    public void OnRun()
    {
        stateMachine.TransitionTo(stateMachine.runState);
    }
}
