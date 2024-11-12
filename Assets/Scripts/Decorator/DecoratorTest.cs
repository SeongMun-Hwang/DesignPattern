using UnityEngine;

public class DecoratorTest : MonoBehaviour
{
    void Start()
    {
        Warrior warrior = new RealWarrior();
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Sword(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());

        warrior = new Shield(warrior);
        Debug.Log(warrior.Description + " " + warrior.Attack());
    }
}
