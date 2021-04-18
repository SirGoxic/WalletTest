using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private Wallet wallet = null;

    [Space]
    
    [SerializeField] private Currency currency = null;
    [SerializeField] private TMP_Text valueText = null;

    public void UpdateValue(Currency currency, int value)
    {
        if (currency == this.currency)
        {
            valueText.text = value.ToString();
        }
    }

    public void AddValue(int value)
    {
        wallet.AddCurrencyValue(currency, value);
    }
    
    public void SetValue(int value)
    {
        wallet.SetCurrencyValue(currency, value);
    }
}
