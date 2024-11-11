using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackCommand : MonoBehaviour, ICommand
{
    public string Name => "Attack";
    ISelectable target;

    public void SetTarget(ISelectable target)
    {
        this.target = target;
    }

    public void Cancel()
    {
    }

    public bool CanExecute(ISelectable executer)
    {
        return target != null && executer is Unit;
    }

    public void Execute(ISelectable executer)
    {
        if(executer is Unit unit && target!=null)
        {
            unit.StartCoroutine(AttackRoutine(unit));
        }
    }
    IEnumerator AttackRoutine(Unit unit)
    {
        NavMeshAgent agent =GetComponent<NavMeshAgent>();
        while (true)
        {
            if(IsinRange(unit, target))
            {
                StateMachine stateMachine= GetComponent<StateController>().stateMachine;
                stateMachine.TransitionTo(stateMachine.attackState);
                PerformAttack(unit, target);
                yield return new WaitForSeconds(1);
            }
            else
            {
                StateMachine stateMachine = GetComponent<StateController>().stateMachine;
                stateMachine.TransitionTo(stateMachine.runState);
                GetComponent<NavMeshAgent>().SetDestination((target as MonoBehaviour).transform.position);
                while(!IsinRange(unit, target))
                {
                    yield return null;
                }
                GetComponent<NavMeshAgent>().ResetPath();
            }
        }
    }
    bool IsinRange(Unit unit,ISelectable target)
    {
        float distance = Vector3.Distance(unit.transform.position, (target as MonoBehaviour).transform.position);
        return distance <= unit.attackRange;
    }
    void PerformAttack(Unit unit, ISelectable target)
    {
        unit.transform.LookAt((target as MonoBehaviour).transform.position);
        Debug.Log(unit.name + " Attacking!");
    }
}
