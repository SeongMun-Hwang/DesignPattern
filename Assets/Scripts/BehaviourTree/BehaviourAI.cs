using System.Collections.Generic;
using UnityEngine;

public class BehaviourAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] float nearBoundary;

    BehaviourTreeRunner runner;
    void Start()
    {
        runner = new BehaviourTreeRunner(SettingFollow());
    }

    void Update()
    {
        runner.Operate();
    }
    INode SettingFollow()
    {
        INode root = new SelectorNode(
            new List<INode>()
            {
                new SequenceNode(
                    new List<INode>()
                    {
                        new ActionNode(IsNearPlayer),
                        new ActionNode(Wait)
                    }
                ),
                new ActionNode(FollowPlayer)
            }
        );
        return root;
    }
    NodeState IsNearPlayer()
    {
        if (Vector3.Magnitude(player.transform.position - transform.position) < nearBoundary)
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
    NodeState Wait()
    {
        return NodeState.Success;
    }
    NodeState FollowPlayer()
    {
        if (Vector3.Magnitude(player.transform.position - transform.position) < nearBoundary)
        {
            return NodeState.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        return NodeState.Running;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,nearBoundary);
    }
}
