using System.Collections.Generic;
using UnityEngine;

public class SequenceNode :INode
{
    List<INode> children;
    public SequenceNode(List<INode> children)
    {
        this.children = children;
    }
    public NodeState Evaluate()
    {
        if (children == null || children.Count == 0)
        {
            return NodeState.Failure;
        }
        foreach (INode child in children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Failure:
                    return NodeState.Failure;
                case NodeState.Running:
                    return NodeState.Running;
            }
        }
        return NodeState.Success;
    }
}
