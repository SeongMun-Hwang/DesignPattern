using UnityEngine;

public interface ICommand
{
    string Name { get; }
    bool CanExecute(ISelectable executer);
    void Execute(ISelectable executer);
    void Cancel();
}