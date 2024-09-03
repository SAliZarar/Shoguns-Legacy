using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSrc;
    public AudioSource sfxSrc;

    public AudioClip attack;
    public AudioClip takedmg;
    public AudioClip jump;

    public AudioClip MenuBGM;
    public AudioClip V1;
    public AudioClip V2;
    public AudioClip V3;
    public AudioClip V4;

    public string currentBGM = null;

    private void Start()
    {
        if(LevelData.audio == false)
        {
            bgmSrc.volume = 0f;
        }
        else
        {
            bgmSrc.volume = 0.5f;
        }
        SetBGM("menu");
        DontDestroyOnLoad(gameObject);
    }

    public void MuteBGM()
    {
        bgmSrc.volume = 0f;
    }

    public void UnMuteBGM()
    {
        bgmSrc.volume = 0.5f;
    }
    public void PlayAttackSFX()
    {
        sfxSrc.PlayOneShot(attack);
    }

    public void PlayJumpSFX()
    {
        sfxSrc.PlayOneShot(jump);
    }

    public void PlayTakeDmgSFX()
    {
        sfxSrc.PlayOneShot(takedmg);
    }

    public void SetBGM(string version)
    {
        switch(version)
        {
            case "menu":
                bgmSrc.clip = MenuBGM;
                currentBGM = version;
                bgmSrc.loop = true;
                bgmSrc.Play();
                Debug.Log("sound changed to menu");
                break;
            case "v1":
                bgmSrc.clip = V1;
                currentBGM = version;
                bgmSrc.loop = true;
                bgmSrc.Play();
                Debug.Log("sound changed to v1");
                break;
            case "v2":
                bgmSrc.clip = V2;
                currentBGM = version;
                bgmSrc.loop = true;
                bgmSrc.Play();
                Debug.Log("sound changed to v2");
                break;
            case "v3":
                bgmSrc.clip = V3;
                currentBGM = version;
                bgmSrc.loop = true;
                bgmSrc.Play();
                Debug.Log("sound changed to v3");
                break;
            case "v4":
                bgmSrc.clip = V4;
                currentBGM = version;
                bgmSrc.loop = true;
                bgmSrc.Play();
                Debug.Log("sound changed to v4");
                break;
        }
    }
}
