using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;
    [SerializeField] private ProductData productData;

    private BagController bagController;

    private void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&isReadyToPick)
        {
            bagController = other.GetComponent<BagController>();
            if (bagController.isEmptySpace())
            {
                AudioManager.instance.PlayAudio(AudioClipType.crabClip);
                bagController.AddProductToBag(productData);
                isReadyToPick = false;

                StartCoroutine(ProductPicked());

            }
            
        }
    }

    IEnumerator ProductPicked()
    {
        Vector3 targetScale = originalScale / 3;
        transform.gameObject.LeanScale(targetScale, .5f);

        yield return new WaitForSeconds(4f);

        transform.gameObject.LeanScale(originalScale, 1f).setEase(LeanTweenType.easeOutBack);
        isReadyToPick = true;
    } 
}
