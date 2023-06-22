using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsListSO _audioClipsListSo;
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucces += DeliveryManagerOnRecipeSucces;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounterCut;
        Player.Instance.OnPickUp += PlayerPickup;
        BaseCounter.OnDrop += PlayerDrop;
        TrashCounter.OnTrashed += OnTrashed;
    }

    private void OnTrashed(object sender, EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(_audioClipsListSo.trash, counter.transform.position);
    }

    private void PlayerDrop(object sender, EventArgs e)
    {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(_audioClipsListSo.objectDrop, counter.transform.position);
    }

    private void PlayerPickup(object sender, EventArgs e)
    {
        Player counter = sender as Player;
        PlaySound(_audioClipsListSo.objectPickup, counter.transform.position);
    }

    private void CuttingCounterCut(object sender, EventArgs e)
    {
        CuttingCounter counter = sender as CuttingCounter;
        PlaySound(_audioClipsListSo.deliveryFail, counter.transform.position);
    }

    private void DeliveryManagerOnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter counter = DeliveryCounter.Instance;
        PlaySound(_audioClipsListSo.deliveryFail, counter.transform.position);
    }

    private void DeliveryManagerOnRecipeSucces(object sender, EventArgs e)
    {
        DeliveryCounter counter = DeliveryCounter.Instance;
        PlaySound(_audioClipsListSo.chop, counter.transform.position);
    }

    public void PlaySound(AudioClip audioClip, Vector3 positon, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, positon, volume);
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 positon, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], positon, volume);
    }

    public void PlayFootSteeps(Vector3 position, float volume)
    {
        PlaySound(_audioClipsListSo.footstept, position, volume);
    }
}