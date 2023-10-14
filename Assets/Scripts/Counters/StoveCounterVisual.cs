using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnVisual;
    [SerializeField] GameObject sizzlingParticles;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried; 

        if (showVisual)
        {
            stoveOnVisual.SetActive(true);
            sizzlingParticles.SetActive(true);
        }

        else
        {
            stoveOnVisual.SetActive(false);
            sizzlingParticles.SetActive(false);
        }
    }
}
