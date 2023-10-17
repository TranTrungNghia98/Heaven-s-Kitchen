using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailure += DeliveryManager_OnDeliveryFailure;
        Player.Instance.OnPicked += Player_OnPicked;
        CuttingCounter.OnEachCut += CuttingCounter_OnEachCut;
        BaseCounter.OnDroped += BaseCounter_OnDroped;
        TrashCounter.OnTrashed += TrashCounter_OnTrashed;
    }

    private void TrashCounter_OnTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnDroped(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.drop, baseCounter.transform.position);
    }

    private void Player_OnPicked(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        PlaySound(audioClipRefsSO.pickup, player.transform.position);
    }

    private void CuttingCounter_OnEachCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailure(object sender, System.EventArgs e)
    {
        Transform deliveryCounter = DeliveryCounter.Instance.transform;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        Transform deliveryCounter = DeliveryCounter.Instance.transform;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 audioPosition, float volume = 1.0f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], audioPosition, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 audioPosition, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(audioClip, audioPosition, volume);
    }

    public void PlayFootStepSound( Vector3 audioPosition, float volume = 1.0f)
    {
        PlaySound(audioClipRefsSO.footstep, audioPosition, volume);
    }
}
