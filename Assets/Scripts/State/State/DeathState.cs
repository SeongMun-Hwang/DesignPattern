using UnityEngine;

public class DeathState : IState
{
    StateController player;
    public DeathState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Death");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}