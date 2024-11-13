using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AttackBehaviourAI : MonoBehaviour
{
    [SerializeField] float detectRange = 5f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float moveSpeed = 3;

    Vector3 originalPosition;
    BehaviourTreeRunner runner;
    Transform detectedPlayer;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        originalPosition = transform.position;

        runner = new BehaviourTreeRunner(SettingAttackBT());
    }

    // Update is called once per frame
    void Update()
    {
        runner.Operate();
    }

    INode SettingAttackBT()
    {
        INode root = new SelectorNode(
            new List<INode>
            {
                new SequenceNode(
                    new List<INode>() {
                        new ActionNode(CheckIsAttacking),
                        new ActionNode(CheckInAttackRange),
                        new ActionNode(DoAttack)
                    }
                    ),
                new SequenceNode(
                    new List<INode>() {
                        new ActionNode(CheckDetectEnemy),
                        new ActionNode(MoveToDetectedEnemy)
                    }
                    ),
                new ActionNode(ReturnToOrigin)
            }
            );

        return root;
    }
    NodeState CheckIsAttacking()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SphereAttack"))
        {
            Debug.Log("Now Attacking");
            return NodeState.Running;
        }
        return NodeState.Success;
    }
    NodeState CheckInAttackRange()
    {
        if (detectedPlayer != null)
        {
            if(Vector3.Magnitude(detectedPlayer.position - transform.position) < attackRange)
            {
                Debug.Log("Player in attack range");
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
    NodeState DoAttack()
    {
        if (detectedPlayer != null)
        {
            animator.SetTrigger("Attack");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
    NodeState CheckDetectEnemy()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));
        if(overlaps!=null && overlaps.Length > 0)
        {
            Debug.Log("Detect Success");
            detectedPlayer = overlaps[0].transform;

            return NodeState.Success;
        }
        detectedPlayer = null;
        return NodeState.Failure;
    }
    NodeState MoveToDetectedEnemy()
    {
        if(detectedPlayer != null)
        {
            if(Vector3.Distance(detectedPlayer.position, transform.position) < detectRange)
            {
                Debug.Log("Player in detect range");
                return NodeState.Success;
            }
            transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, Time.deltaTime * moveSpeed);
            Debug.Log("Move to Player");
            return NodeState.Running;
        }
        Debug.Log("Player detect failed");
        return NodeState.Failure;
    }
    NodeState ReturnToOrigin()
    {
        if(Vector3.Distance(originalPosition, transform.position) < float.Epsilon)
        {
            Debug.Log("Enemy in origin position");
            return NodeState.Success;
        }
        else
        {
            transform.position=Vector3.MoveTowards(transform.position,originalPosition, Time.deltaTime * moveSpeed);
            Debug.Log("Move to Origin position");
            return NodeState.Running;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}
