using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Image icon;
    [SerializeField] Color successedColor;
    [SerializeField] Color failedColor;
    [SerializeField] Sprite successedIcon;
    [SerializeField] Sprite failedIcon;

    private void Start()
    {
        DeliveryManager.Instance.OnCompleteRecipe += DeliveryManager_OnCompleteRecipe;
        DeliveryManager.Instance.OnDeliveryFailure += DeliveryManager_OnDeliveryFailure;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnDeliveryFailure(object sender, System.EventArgs e)
    {
        background.color = failedColor;
        icon.sprite = failedIcon;
        resultText.text = "DELIVERY\nFAILED";
        gameObject.SetActive(true);
    }

    private void DeliveryManager_OnCompleteRecipe(object sender, System.EventArgs e)
    {
        background.color = successedColor;
        icon.sprite = successedIcon;
        resultText.text = "DELIVERY\nSUCCESSED";
        gameObject.SetActive(true);
    }
}
