using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBurningUI : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float warningPoint = 0.5f;
        bool show = e.progressNormalize >= warningPoint && stoveCounter.IsFried();

        if (show)
        {
            Show();
        }

        else
        {
            Hide();
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
