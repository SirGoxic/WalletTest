using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Currency", menuName = "Currency", order = 2)]
public class Currency : ScriptableObject
{
    [SerializeField] private string currencyName = "Currency";

    public string CurrencyName => currencyName;
}
