using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource playerAudioSource;
    public AudioSource wormAudioSource;
    public AudioSource audioSource;
    public AudioSource dragonrAudioSource;

    public AudioClip BGM;
    [Header("Playeraudio")]
    public AudioClip attack, jump, dash, fireball, hit, getHit, heal, death, swichWeapon, shuriken;
    [Header("Worm")]
    public AudioClip wormBGM, wormFire, wormDeath;
    [Header("Dragon")]
    public AudioClip dragonBGM,dragonCome, dragonBreath, dragonDeath, bossFireBall, fireExplosion, terrainFire;
    [Header("Other")]
    public AudioClip getItem, crankDown, blockBroken, chestOpen, over,gameover, click, button;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    //BGM
    public void BGMplay()
    {
        audioSource.clip = BGM;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void BGMpause()
    {
        audioSource.clip = BGM;
        audioSource.Pause();
    }
    //Player
    public void AttackAudio()
    {
        playerAudioSource.clip = attack;
        playerAudioSource.Play();
    }
    public void JumpAudio()
    {
        playerAudioSource.clip = jump;
        playerAudioSource.Play();

    }
    public void DashAudio()
    {
        playerAudioSource.clip = dash;
        playerAudioSource.Play();
    }
    public void FireballAudio()
    {
        playerAudioSource.clip = fireball;
        playerAudioSource.PlayDelayed(0.6f);
    }
    public void HitAudio()
    {
        playerAudioSource.clip = hit;
        playerAudioSource.Play();
    }
    public void GetHitAudio()
    {
        playerAudioSource.clip = getHit;
        playerAudioSource.Play();
    }
    public void DeathAudio()
    {
        playerAudioSource.clip = death;
        playerAudioSource.Play();
    }
    public void SwitchWeaponAudio()
    {
        playerAudioSource.clip = swichWeapon;
        playerAudioSource.Play();
    }
    public void ShurikenAudio()
    {
        playerAudioSource.clip = shuriken;
        playerAudioSource.Play();
    }
    public void HealAudio()
    {
        playerAudioSource.clip = heal;
        playerAudioSource.Play();
    }
    //Worm
    public void WormBGMplay()
    {
        wormAudioSource.clip = wormBGM;
        wormAudioSource.Play();
    }
    public void WormBGMstop()
    {
        wormAudioSource.clip = wormBGM;
        wormAudioSource.Stop();
    }
    public void WormDeathAudio()
    {
        wormAudioSource.clip = wormDeath;
        wormAudioSource.Play();
        
    }
    //Dragon
    public void DragonBGMplay()
    {
        dragonrAudioSource.clip = dragonBGM;
        dragonrAudioSource.Play();
    }
    public void DragonBGMstop()
    {
        dragonrAudioSource.clip = dragonBGM;
        dragonrAudioSource.Stop();
    }
    public void DragonComeAudio()
    {
        dragonrAudioSource.clip = dragonCome;
        dragonrAudioSource.Play();
    }
    public void DragonBreathAudio()
    {
        dragonrAudioSource.clip = dragonBreath;
        dragonrAudioSource.Play();
    }
    public void DragonDeathAudio()
    {
        dragonrAudioSource.clip = dragonDeath;
        dragonrAudioSource.Play();
    }
    public void BossFireBallAudio()
    {
        dragonrAudioSource.clip = bossFireBall;
        dragonrAudioSource.Play();
    }
    public void FireExplosionAudio()
    {
        dragonrAudioSource.clip = fireExplosion;
        dragonrAudioSource.Play();
    }
    public void TerrainFireAudio()
    {
        dragonrAudioSource.clip = terrainFire;
        dragonrAudioSource.Play();
    }
    //Other
    public void CrankDownAudio()
    {
        audioSource.clip = crankDown;
        audioSource.Play();
    }
    public void GetItemAudio()
    {
        audioSource.clip = getItem;
        audioSource.Play();
    }
    public void BlockBrokenAudio()
    {
        audioSource.clip = blockBroken;
        audioSource.Play();
    }
    public void ChestOpenAudio()
    {
        audioSource.clip = chestOpen;
        audioSource.Play();
    }
    public void ClickAudio()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    public void ButtonAudio()
    {
        audioSource.clip = button;
        audioSource.Play();
    }
    public void OverAudio()
    {
        audioSource.clip = over;
        audioSource.PlayDelayed(2);
    }
    public void GameOverAudio()
    {
        audioSource.clip = gameover;
        audioSource.PlayDelayed(2);
    }
}
