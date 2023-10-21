using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private float warningSoundTimerMax = 0.3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float warningPoint = 0.5f;
        if (e.progressNormalize > warningPoint && stoveCounter.IsFried())
        {
            warningSoundTimer--;
            if (warningSoundTimer <= 0)
            {
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningBurningSound(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound)
        {
            audioSource.Play();
        }

        else
        {
            audioSource.Pause();
        }
    }
}
