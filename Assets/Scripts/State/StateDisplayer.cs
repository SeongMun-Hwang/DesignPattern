using TMPro;
using UnityEngine;

public class StateDisplayer : MonoBehaviour
{
    public TextMeshPro stateLabel;
    StateController playerController;

    private void Awake()
    {
        playerController = GetComponent<StateController>();
    }

    private void OnEnable() //이벤트 구독
    {
        playerController.stateMachine.stateChanged += OnStateChanged;
    }
    private void OnDisable() //이벤트 구취
    {
        playerController.stateMachine.stateChanged -= OnStateChanged;
    }
    void OnStateChanged(IState state)
    {
        stateLabel.text=state.GetType().Name;
    }
}
