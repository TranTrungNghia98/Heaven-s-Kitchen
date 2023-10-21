using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveProgressBarWarningUI : MonoBehaviour
{
    private const string IS_WARNING = "isWarning";

    [SerializeField] StoveCounter stoveCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float warningPoint = 0.5f;
        bool show = e.progressNormalize >= warningPoint && stoveCounter.IsFried();

        animator.SetBool(IS_WARNING, show);
    }
}
