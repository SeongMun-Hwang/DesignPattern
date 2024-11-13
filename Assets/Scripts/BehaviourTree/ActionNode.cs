using System;
using UnityEngine;

public class ActionNode : INode
{
    Func<NodeState> onUpdate = null;

    public ActionNode(Func<NodeState> onUpdate)
    {
        this.onUpdate = onUpdate;
    }
    public NodeState Evaluate()
    {
        return onUpdate?.Invoke() ?? NodeState.Failure; //onUpdate가 함수가 없으면 Failure 반환
    }
}
