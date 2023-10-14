using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject iHasProgressGameObject;

    private IHasProgress iHasProgress;
    // Start is called before the first frame update
    private void Start()
    {
        iHasProgress = iHasProgressGameObject.GetComponent<IHasProgress>();

        if (iHasProgress == null )
        {
            Debug.LogError("I has progress is " + iHasProgress + "You need to change iHasProgressGameObject");
        }

        iHasProgress.OnProgressChanged += IHasProgress_OnProgressChanged1;

        barImage.fillAmount = 0;

        Hide();
    }

    private void IHasProgress_OnProgressChanged1(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalize;

        if (barImage.fillAmount == 0 || barImage.fillAmount == 1)
        {
            Hide();
        }

        else
        {
            Show();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
