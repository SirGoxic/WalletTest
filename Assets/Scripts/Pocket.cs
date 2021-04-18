using System;
using UnityEngine;

[Serializable]
public class Pocket
{
    [NonSerialized] private Currency currency = null;
    [SerializeField] private int value = 0;

    public bool DontHaveCurrency => currency == null;
    
    public Currency Currency => currency;
    public int Value => value;

    public void SetCurrency(Currency currency)
    {
        this.currency = currency;
    }
    
    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public void AddValue(int incr)
    {
        value += incr;

        if (value < 0 )
        {
            value = 0;
        }
    }
}
