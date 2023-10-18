using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingClockTimerUI : MonoBehaviour
{
    [SerializeField] Image clockImage;

    private void Update()
    {
        clockImage.fillAmount = GameManager.Instance.GetPlayingTimerNomalized();
    }
}
