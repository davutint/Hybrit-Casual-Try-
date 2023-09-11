using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;


        }
        else
            Destroy(instance);
    }
    public void AddCoin(int price)
    {
        coins += price;
        DisplayCoins();
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }

    private void DisplayCoins()
    {
        UIManager.instance.ShowCointCountOnScreen(coins);
    }

    private void SpendCoin(int price)
    {
        coins -= price;
        DisplayCoins();
    }

    public bool TryBuyThisUnit(int price)
    {
        if (GetCoins()>price)
        {
            SpendCoin(price);
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coins;
    }
}
