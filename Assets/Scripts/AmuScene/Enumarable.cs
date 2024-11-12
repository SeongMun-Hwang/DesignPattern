using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Enumarable : MonoBehaviour
{
    public class NumberCollection : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            yield return 10;
            yield return 11;
            yield return 12;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    IEnumerable<int> GetNumber()
    {
        yield return 10;
        yield return 11;
        yield return 12;
    }
    IEnumerator GetNumbersByEnumerator()
    {
        yield return 20;
        yield return 21;
        yield return 22;
    }
    void Start()
    {
        foreach (var number in GetNumber())
        {
            Debug.Log(number);
        }
        IEnumerator enumerator = GetNumbersByEnumerator();
        while (enumerator.MoveNext())
        {
            Debug.Log(enumerator.Current);
        }
        NumberCollection numberEnumerator = new NumberCollection();
        foreach (var number in numberEnumerator)
        {
            {
                Debug.Log(number);
            }
        }
    }
}