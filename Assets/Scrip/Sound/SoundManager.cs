using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Guns;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }



    [Header("----Audio----")]
    public AudioSource musicSource;
    public AudioSource musicSource2;
    public AudioSource SFXSource;
    public AudioSource EnemySource, EnemySource2, TurtleSpikeSource;
    public AudioSource PlayerSource;
    public AudioSource PlayerSource_2;

    [Header("BackGround")]
    public AudioClip BackGround_1;
    public AudioClip GolemChaseClip_BackGround;

    [Header("---SFX---")]

    public AudioClip SciBlueSound_Shooting;
    public AudioClip SciBlueSound_Reload;
    public AudioClip SciBlueSound_EmptyMagazine,
       AkaiSound_Shooting,
       Explosion_Sound,
       GrenadeSound, SmokeBoomSound, ThrowSomeThing;
    public AudioClip DragDropClip;
    public AudioClip LevelUpClip;
    public AudioClip HealClip;

    [Header("GolemClip")]
    public AudioClip GolemWalk;
    public AudioClip GolemChase;
    public AudioClip GolemAttack;
    public AudioClip GolemHurt, GolemHurt2;
    public AudioClip GolemDeath;

    [Header("Spike Turtle")]
    public AudioClip TurtleAttackClip;
    public AudioClip TurtleChaseClip;
    public AudioClip TurtleHurtClip;
    public AudioClip TurtleDeadClip;

    [Header("PlayerClip")]
    public AudioClip playerHurt;
    public AudioClip playerHurt2;
    public AudioClip playerDeath;
    public AudioClip playerDeadSoundTrack;

    [Header("Melee Clip")]
    public AudioClip wood_ChopClip;
    public AudioClip mellee_clip;
    public AudioClip BrokenWeap_clip;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {

        musicSource2.clip = GolemChaseClip_BackGround;
        musicSource.Play();
        inItVolumeSetting();
    }

    private void Update()
    {
        inItVolumeSetting();
    }
    private void inItVolumeSetting()
    {
        musicSource.volume = SettingManager.Instance.musicSlider.value;
        musicSource2.volume = SettingManager.Instance.musicSlider.value;

        EnemySource.volume = SettingManager.Instance.EffectSlider.value;
        EnemySource2.volume = SettingManager.Instance.EffectSlider.value;
        SFXSource.volume = SettingManager.Instance.EffectSlider.value;
        TurtleSpikeSource.volume = SettingManager.Instance.EffectSlider.value;
        PlayerSource_2.volume = SettingManager.Instance.EffectSlider.value;
        PlayerSource.volume = SettingManager.Instance.EffectSlider.value;
        

    }
    public void Play_BrokenItemSound()
    {
        SFXSource.PlayOneShot(BrokenWeap_clip);
    }

    public void Play_HealingSound()
    {
        SFXSource.PlayOneShot(HealClip);
    }
    //--- Chop Sound --
    public void Play_LevelUpSound()
    {
        SFXSource.PlayOneShot(LevelUpClip);
    }
    public void Play_PopUpItemSound()
    {
        SFXSource.PlayOneShot(DragDropClip);
    }

    public void PlayWood_chopSound()
    {
        PlayerSource_2.PlayOneShot(wood_ChopClip);
    }

    //====
    public void PlaySciBlueSound_Shooting()
    {
        SFXSource.PlayOneShot(SciBlueSound_Shooting);
    }
    public void PlaySciBlueSound_Reloading()
    {
        SFXSource.PlayOneShot(SciBlueSound_Reload);
    }

    public void PlaySciBlueSound_Empty()
    {
        SFXSource.PlayOneShot(SciBlueSound_EmptyMagazine);

    }
    public void PlayAkaiSound_Shooting()
    {
        SFXSource.PlayOneShot(AkaiSound_Shooting);
    }

    public void PlayExplosionBullet()
    {
        SFXSource.PlayOneShot(Explosion_Sound);
    }

    public void PlayGrenadeExplosion()
    {
        SFXSource.PlayOneShot(GrenadeSound);
    }

    public void PlaySmokeBoom()
    {
        SFXSource.PlayOneShot(SmokeBoomSound);
    }

    public void PlayThrowSomeThing()
    {
        SFXSource.PlayOneShot(ThrowSomeThing);
    }

    //-----------------------GOLEMSOUND------------------

    public void GolemSound_Death()
    {
        EnemySource2.PlayOneShot(GolemDeath);
    }
    public void GolemSound_Hurt()
    {
        EnemySource2.PlayOneShot(GolemHurt);
    }

    public void GolemSound_Hurt2()
    {
        EnemySource2.PlayOneShot(GolemHurt2);

    }
    public void GolemSound_Walk()
    {
        EnemySource.PlayOneShot(GolemWalk);
    }
    public void GolemSound_Chase()
    {
        EnemySource.PlayOneShot(GolemChase);
    }
    public void GolemSound_Attack()
    {
        EnemySource.PlayOneShot(GolemAttack);
    }

    //------------ Spike Turtle ----------
    public void Turtle_Attack()
    {
        TurtleSpikeSource.PlayOneShot(TurtleAttackClip);
    }
    public void Turtle_Chase()
    {
        TurtleSpikeSource.PlayOneShot(TurtleChaseClip);
    }
    public void Turtle_Hurt()
    {
        TurtleSpikeSource.PlayOneShot(TurtleHurtClip);
    }
    public void Turtle_Dead()
    {
        TurtleSpikeSource.PlayOneShot(TurtleDeadClip);
    }
    //------------PLAYERSOUND-------------

    public void Play_PlayerHurt()
    {

        int randomvalue = Random.Range(0, 2);
        if (randomvalue == 0)
        {
            PlayerSource.PlayOneShot(playerHurt);

        }
        else
        {
            PlayerSource.PlayOneShot(playerHurt2);
        }
    }
    public void Play_PlayerDeath()
    {
        PlayerSource.PlayOneShot(playerDeath);
    }

    public void Play_PlayerDeadSoundTrack()
    {
        musicSource.clip = playerDeadSoundTrack;
        musicSource.Play();
        
    }

    public void Play_BackGroundMusic()
    {
        musicSource.Stop();
        musicSource.clip = BackGround_1;
        musicSource.Play();
    }
    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Sci_max:
                PlaySciBlueSound_Shooting();
                break;

            case WeaponModel.Akai_max:
                PlayAkaiSound_Shooting();
                break;

        }
    }
}
