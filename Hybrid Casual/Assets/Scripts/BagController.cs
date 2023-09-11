using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    [SerializeField] TextMeshPro maxText;
    int maxBagCapacity;
    private void Start()
    {
        maxBagCapacity = 5;
        ControlBagCapacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            for (int i =productDataList.Count-1;i>=0; i--)
            {
                SelProductToShop(productDataList[i]);
                Destroy(bag.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
            ControlBagCapacity();
        }
    }

    public void AddProductToBag(ProductData productData)
    {
        
        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bag, true);
        CalculateObjectSize(boxProduct);
        float yPosition = CalculateNewYPositionOfBox();

        
        boxProduct.transform.localRotation = Quaternion.identity;
        boxProduct.transform.localPosition = Vector3.zero;
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        productDataList.Add(productData);
        ControlBagCapacity();
    }

    private void SelProductToShop(ProductData productData)
    {
        CashManager.instance.ExchangeProduct(productData);
    }

    private float CalculateNewYPositionOfBox()
    {
        float newYPos = productSize.y * productDataList.Count;
        return newYPos;
    }

    private void CalculateObjectSize(GameObject gameobject)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameobject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;
        }
    }

    private void ControlBagCapacity()
    {
        if (productDataList.Count==maxBagCapacity)
        {
            SetMaxTextOn();
        }
        else
        {
            SetMaxTextOff();
        }
    }

    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true);
        }
        
    }


    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false);
        }
    }

    public bool isEmptySpace()
    {
        if (productDataList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }
}
