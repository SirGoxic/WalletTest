using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Wallet : MonoBehaviour
{
    [Serializable]
    public class OnValueChangeEvent : UnityEvent<Currency, int>{}
    
    [SerializeField] private List<Currency> currencies = new List<Currency>();
    
    private Dictionary<Currency, Pocket> currencyPockets = new Dictionary<Currency, Pocket>();

    [SerializeField] public OnValueChangeEvent onValueChange = new OnValueChangeEvent();
    
    private void Start()
    {
        FillDictionary();
    }

    private void FillDictionary()
    {
        currencyPockets.Clear();
        
        //Cash pockets by currency name
        foreach (Currency currency in currencies)
        {
            if (currency != null)
            {
                Pocket pocket = new Pocket();
                pocket.SetCurrency(currency);
                currencyPockets.Add(currency, pocket);
                
                onValueChange.Invoke(currency, pocket.Value);
            }
        }
    }

    public void SavePocketsToPrefs()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] != null)
            {
                SaveAndLoad.SaveToPrefs(currencies[i].name, currencyPockets[currencies[i]]);
            }
        }
    }

    public void LoadPocketsFromPrefs()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] == null)
            {
                continue;
            }
            
            Pocket tempPocket = currencyPockets[currencies[i]];
            Pocket newPocket;
            try
            {
                newPocket = SaveAndLoad.LoadFromPrefs<Pocket>(currencies[i].name);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                newPocket = tempPocket;
            }

            if (newPocket != null)
            {
                currencyPockets[currencies[i]] = newPocket;
                onValueChange.Invoke(currencies[i], newPocket.Value);
            }
        }
    }
    
    public void SavePocketsToTxt()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] != null)
            {
                SaveAndLoad.SaveToTxt(currencies[i].name + ".txt", currencyPockets[currencies[i]]);
            }
        }
    }

    public void LoadPocketsFromTxt()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] == null)
            {
                continue;
            }
            
            Pocket tempPocket = currencyPockets[currencies[i]];
            Pocket newPocket;
            try
            {
                newPocket = SaveAndLoad.LoadFromTxt<Pocket>(currencies[i].name + ".txt");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                newPocket = tempPocket;
            }

            if (newPocket != null)
            {
                currencyPockets[currencies[i]] = newPocket;
                onValueChange.Invoke(currencies[i], newPocket.Value);
            }
        }
    }

    public void SavePocketsToBinary()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] != null)
            {
                SaveAndLoad.SaveToBinary(currencies[i].name + ".pocket", currencyPockets[currencies[i]]);
            }
        }
    }

    public void LoadPocketsFromBinary()
    {
        for (int i = 0; i < currencies.Count; i++)
        {
            if (currencies[i] == null)
            {
                continue;
            }
            
            Pocket tempPocket = currencyPockets[currencies[i]];
            Pocket newPocket;
            try
            {
                newPocket = (Pocket)SaveAndLoad.LoadFromBinary(currencies[i].name + ".pocket");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                newPocket = tempPocket;
            }

            if (newPocket != null)
            {
                currencyPockets[currencies[i]] = newPocket;
                onValueChange.Invoke(currencies[i], newPocket.Value);
            }
        }
    }
    
    public int GetCurrencyValue(Currency currency)
    {
        if (currencyPockets.ContainsKey(currency))
        {
            return currencyPockets[currency].Value;
        }
        else
        {
            Debug.LogError("Don't have this currency " + currency.name);
            return 0;
        }
    }

    public void AddCurrencyValue(Currency currency, int value)
    {
        if (currencyPockets.ContainsKey(currency))
        {
            currencyPockets[currency].AddValue(value);
            onValueChange.Invoke(currency, currencyPockets[currency].Value);
        }
        else
        {
            Debug.LogError("Don't have this currency " + currency.name);
        }
    }

    public void SetCurrencyValue(Currency currency, int value)
    {
        if (currencyPockets.ContainsKey(currency))
        {
            currencyPockets[currency].SetValue(value);
            onValueChange.Invoke(currency, currencyPockets[currency].Value);
        }
        else
        {
            Debug.LogError("Don't have this currency " + currency.name);
        }
    }
}
