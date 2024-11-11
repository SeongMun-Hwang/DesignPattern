using UnityEngine;

public interface ISelectable
{
    void Select();
    void DeSelect();
    bool IsSelected();
    void ExcuteCommand(ICommand command);
}