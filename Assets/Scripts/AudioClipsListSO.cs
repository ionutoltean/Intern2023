using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioClipsListSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySucces;
    public AudioClip[] footstept;
    public AudioClip stoveFry;
    public AudioClip[] warning;
    public AudioClip[] trash;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
}