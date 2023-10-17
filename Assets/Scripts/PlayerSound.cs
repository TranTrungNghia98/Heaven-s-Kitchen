using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] Player player;
    private float footStepTimer;
    private float footStepTimerMax = .1f;

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootStepSound(transform.position);
            }
            
        }
    }
}
