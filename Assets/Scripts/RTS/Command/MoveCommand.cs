using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : MonoBehaviour, ICommand
{
    public string Name => "Move";

    Vector3 destination;
    NavMeshAgent agent;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
    public bool CanExecute(ISelectable executer)
    {
        return GetComponent<NavMeshAgent>() != null;
    }
    public void Execute(ISelectable executer)
    {
        if (executer is Unit unit && GetComponent<NavMeshAgent>() is NavMeshAgent agent)
        {
            this.agent = agent;
            StateMachine stateMachine = GetComponent<StateController>().stateMachine;
            stateMachine.TransitionTo(stateMachine.runState);
            agent.SetDestination(destination);
            unit.StartCoroutine(WaitForArrival());
        }
    }
    IEnumerator WaitForArrival()
    {
        while (agent.pathPending) //destination 입력 후 경로 계산 시간 동안 대기
        {
            yield return null;
        }
        while (agent.remainingDistance > agent.stoppingDistance) //대기 후 destination 도달까지 대기
        {
            yield return null;
        }
        StateMachine stateMachine = GetComponent<StateController>().stateMachine;
        stateMachine.TransitionTo(stateMachine.idleState);
    }
    public void Cancel()
    {
        if (agent != null)
        {
            agent.ResetPath();
        }
    }
}
