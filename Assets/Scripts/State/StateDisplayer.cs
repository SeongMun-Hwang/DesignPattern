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

    private void OnEnable() //�̺�Ʈ ����
    {
        playerController.stateMachine.stateChanged += OnStateChanged;
    }
    private void OnDisable() //�̺�Ʈ ����
    {
        playerController.stateMachine.stateChanged -= OnStateChanged;
    }
    void OnStateChanged(IState state)
    {
        stateLabel.text=state.GetType().Name;
    }
}
