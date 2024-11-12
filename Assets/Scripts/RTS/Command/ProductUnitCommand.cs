using UnityEngine;

public class ProductUnitCommand : MonoBehaviour, ICommand
{
    [SerializeField] UnitData unitData;
    public string Name => "Produce"+unitData.unitName;

    public void Cancel()
    {
        
    }

    public bool CanExecute(ISelectable executer)
    {
        return true;
    }

    public void Execute(ISelectable executer)
    {
        if(executer is Building building)
        {
            building.StartUnitProduction(unitData);
        }
    }
}
