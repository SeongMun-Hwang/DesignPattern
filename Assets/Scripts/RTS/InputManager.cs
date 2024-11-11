using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    UnitSelector unitSelector;
    private enum CommandMode
    {
        None,
        Attack,
        Move,
    }
    private CommandMode commandMode = CommandMode.None;
    void Start()
    {
        unitSelector = FindFirstObjectByType<UnitSelector>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            commandMode = CommandMode.Attack;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            commandMode = CommandMode.Move;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelCommandMode();
        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }
    void CancelCommandMode()
    {
        if (commandMode != CommandMode.None)
        {
            commandMode = CommandMode.None;
        }
        else
        {
            unitSelector.DeselectAll();
        }
    }
    void HandleLeftClick()
    {
        if (commandMode == CommandMode.Attack)
        {
            HandleAttackCommand();
        }
        else if (commandMode == CommandMode.Move)
        {
            HandleMoveCommand();
        }
        else
        {
            HandleSelection();
        }
    }
    void HandleSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            ISelectable selectable = hit.collider.GetComponent<ISelectable>();

            if (selectable != null)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    unitSelector.Select(selectable);
                }
                else
                {
                    unitSelector.DeselectAll();
                    unitSelector.Select(selectable);
                }
            }
            else
            {
                unitSelector.DeselectAll();
            }
        }
    }
    void HandleRightClick()
    {
        if (commandMode == CommandMode.None)
        {
            HandleMoveCommand();
        }
    }
    void HandleMoveCommand()
    {
        Vector3 destination = GetMouseClickPosition();
        List<ISelectable> selectedUnit = unitSelector.GetSelectedUnits();

        foreach (ISelectable selectable in selectedUnit)
        {
            if (selectable is Unit unit)
            {
                MoveCommand moveCommand = unit.GetComponent<MoveCommand>();
                if (moveCommand != null)
                {
                    moveCommand.SetDestination(destination);
                    unit.ExcuteCommand(moveCommand);
                }
            }
        }
        commandMode = CommandMode.None;
    }
    Vector3 GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void HandleAttackCommand()
    {
        commandMode = CommandMode.None;
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            ISelectable target = hit.collider.GetComponent<ISelectable>();
            if(target != null)
            {
                List<ISelectable> selectedUnits=unitSelector.GetSelectedUnits();
                foreach(ISelectable selectable in selectedUnits)
                {
                    if (selectable is Unit unit)
                    {
                        AttackCommand attackCommand=unit.GetComponent<AttackCommand>();
                        if (attackCommand != null)
                        {
                            attackCommand.SetTarget(target);
                            unit.ExcuteCommand(attackCommand);
                        }
                    }
                }
                commandMode = CommandMode.None;
            }
            else
            {
                //æÓ≈√∂•
            }
        }
    }
}