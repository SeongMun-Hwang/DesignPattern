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
    //이번 명령 커맨드는 에이전트의 목적지 설정으로 작동하므로 당연히! NavMeshAgent가 없으면 쓸 수 없다.
    public bool CanExecute(ISelectable executer) 
    {
        return GetComponent<NavMeshAgent>() != null;
    }
    public void Execute(ISelectable executer)
    {
        //명령 실행자가 ISelectable한 Unit이고, 냅멥에이전트가 있으면
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
