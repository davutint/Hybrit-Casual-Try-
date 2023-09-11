using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI coinText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;


        }
        else
            Destroy(instance);
    }

    public void ShowCointCountOnScreen(int coins)
    {
        coinText.text = coins.ToString();
    }
}
