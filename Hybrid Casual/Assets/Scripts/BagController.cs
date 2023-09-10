using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
   

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
}
