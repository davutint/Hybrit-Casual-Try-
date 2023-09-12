using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnlockBakeryUnitController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;
    [SerializeField] private int UseProductInSeconds = 10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGameObj;
    [SerializeField] private ParticleSystem smokeParticle;
    private float time;
    private int storedProductCount;

    private void Start()
    {
        DisplayProductCount();
    }

    private void Update()
    {
        if (storedProductCount > 0)
        {

            time += Time.deltaTime;
            if (time >= UseProductInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
        
    }
    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
        ControlSmokeParticle();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool StoreProduct()
    {
        if (maxStoredProductCount == storedProductCount)
        {
            return false;
        }
        storedProductCount++;

        DisplayProductCount();
        return true;
        
    }

    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount();
        CreateCoin();
    }
    private void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f;
        Vector3 instantiatePos = coinTransform.position + position;

        Instantiate(coinGameObj, instantiatePos, Quaternion.identity);

    }

    private void ControlSmokeParticle()
    {
        if (storedProductCount== 0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if (smokeParticle.isStopped)
            {
                smokeParticle.Play();
            }
        }
    }
}
