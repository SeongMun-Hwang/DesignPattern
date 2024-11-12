using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable
{
    [SerializeField] GameObject selectIndicator;
    [SerializeField] BuildingData buildingData;
    [SerializeField] Transform spawnPoint;

    UnitData[] producableUnits;
    Coroutine activeProductionCoroutine;

    bool isSelected = false;
    private void Start()
    {
        name = buildingData.name;
        producableUnits = buildingData.producableUnits;
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
        if (command.CanExecute(this))
        {
            command.Execute(this);
        }
        else
        {

        }
    }
    public bool CanProduceUnit(UnitData unitData)
    {
        if (!buildingData.canProduceUnits) return false;
        foreach (var unit in producableUnits)
        {
            if (unit == unitData) return true;
        }
        return false;
    }
    public void StartUnitProduction(UnitData unitData)
    {
        if (activeProductionCoroutine != null)
        {
            Debug.Log("already in progress");
            return;
        }
        activeProductionCoroutine = StartCoroutine(ProduceUnit(unitData));
    }
    IEnumerator ProduceUnit(UnitData unitData)
    {
        StateMachine stateMachine = GetComponent<StateController>().stateMachine;
        stateMachine.TransitionTo(stateMachine.productionState);

        yield return new WaitForSeconds(unitData.productionTime);

        SpawnUnit(unitData);

        stateMachine.TransitionTo(stateMachine.idleState);
        activeProductionCoroutine = null;
    }
    void SpawnUnit(UnitData unitData)
    {
        Instantiate(unitData.prefab, spawnPoint.position, Quaternion.identity);
    }
}
