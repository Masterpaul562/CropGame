using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyShopDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisplay;

    private void Update()
    {
        moneyDisplay.text = "Gold: " + Game_Manager.Instance.money;
    }
}
