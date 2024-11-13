using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : INode
{
    List<INode> children;
    public SelectorNode(List<INode> children)
    {
        this.children = children;
    }
    public NodeState Evaluate()
    {
        if (children == null)
        {
            return NodeState.Failure;
        }
        foreach (INode child in children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Running:
                    return NodeState.Running;
            }
        }
        return NodeState.Failure;
    }
}
