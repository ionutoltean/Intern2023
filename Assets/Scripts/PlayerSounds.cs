using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float foostepTime;
    private float foostepTimeMax=.25f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        foostepTime -= Time.deltaTime;
        if (foostepTime < 0f && _player.IsWalking())
        {
            foostepTime = foostepTimeMax;
            SoundManager.Instance.PlayFootSteeps(transform.position, 1f);
        }
    }
}