using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedUnitController : MonoBehaviour
{
    [Header("SETTİNGS")]
    [SerializeField] private int price;
    [SerializeField] private int ID;

    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;
    private bool isPurchased;
    private string keyUnit = "keyUnit";

    private void Start()
    {
        priceText.text = price.ToString();
        LoadUnit();
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

            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            Unlock();
            SaveUnit();


        }
    }

    private void Unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unlockedUnit.SetActive(true);
    }

    private void SaveUnit()
    {
        string key = keyUnit + ID.ToString();
        PlayerPrefs.SetString(key, "saved");
    }
    private void LoadUnit()
    {
        string key = keyUnit + ID.ToString();
        string status = PlayerPrefs.GetString(key);

        if (status.Equals("saved"))
        {
            Unlock();
        }
    }
}
