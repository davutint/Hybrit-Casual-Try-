using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedUnitController : MonoBehaviour
{
    [Header("SETTİNGS")]
    [SerializeField] private int price;

    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;
    private bool isPurchased;

    private void Start()
    {
        priceText.text = price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!isPurchased)
        {
            UnlockUnit();
        }
    }

    private void UnlockUnit()
    {
        if (CashManager.instance.TryBuyThisUnit(price))
        {
            Unlock();

        }
    }

    private void Unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unlockedUnit.SetActive(true);
    }
}
