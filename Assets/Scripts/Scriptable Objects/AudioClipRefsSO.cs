using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    public AudioClip[] drop;
    public AudioClip[] pickup;
    public AudioClip sizzleLoop;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
