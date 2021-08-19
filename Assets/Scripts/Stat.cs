using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int currentValue;
    [SerializeField]
    private int maxValue;

    public int GetCurrentValue ()
    {
        return currentValue;
    }

    public int GetMaxValue()
    {
        return maxValue;
    }

    public void SetCurrentValue(int value)
    {
        currentValue = value;
    }

    public void SetMaxValue(int value)
    {
        maxValue = value;
    }



}
