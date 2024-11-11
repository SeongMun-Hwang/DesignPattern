using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
    bool isSelected = false;
    [SerializeField]
    public GameObject selectIndicator;
    public UnitData unitData;

    ICommand currentCommand;
    public float attackRange;
    private void Start()
    {
        GetComponent<NavMeshAgent>().speed = unitData.moveSpeed;
        attackRange = unitData.attackRange;
    }
    public bool IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        isSelected = true;
        selectIndicator.SetActive(true);
    }
    public void DeSelect()
    {
        isSelected = false;
        selectIndicator.SetActive(false);
    }
    public void ExcuteCommand(ICommand command)
    {
        if (currentCommand != null)
        {
            currentCommand.Cancel();
            StopAllCoroutines();
        }

        if (command.CanExecute(this))
        {
            currentCommand = command;
            command.Execute(this);
        }
        else
        {

        }
    }
}
