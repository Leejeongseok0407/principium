using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;


    float duration = 1f;

    float bossDuration = 0;
    AudioSource bossSource;
    [SerializeField]
    AudioClip[] bossMusic;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        //foreach(Sound s in sounds)
        //{
        //    s.source = gameObject.AddComponent<AudioSource>();
        //    s.source.clip = s.clip;
        //    s.source.volume = s.volume;
        //    s.source.pitch = s.pitch;
        //    s.source.loop = s.loop;
        //}
        bossSource = GetComponent<AudioSource>();
    }

    public static AudioManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Play(string _name)
    {
        Debug.Log(_name);
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Pause()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Pause();
        }
        bossSource.Pause();

        //Sound s = Array.Find(sounds, sound => sound.name == _name);
        //if (s == null)
        //{
        //    Debug.LogWarning("Sound: " + name + " not found!");
        //    return;
        //}
        //s.source.Pause();
    }

    public void UnPause()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].UnPause();
        }
        bossSource.UnPause();

        //Sound s = Array.Find(sounds, sound => sound.name == _name);
        //if (s == null)
        //{
        //    Debug.LogWarning("Sound: " + name + " not found!");
        //    return;
        //}
        //s.source.UnPause();
    }

    public void Stop(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void FadeOut(string _name)
    {
        StartCoroutine(IFadeOut(_name));
    }

    public void FadeIn(string _name)
    {
        StartCoroutine(IFadeIn(_name));
    }

    private IEnumerator IFadeOut(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            yield return null;
        }
        else
        {
            float startVol = s.source.volume;
            while (s.source.volume > 0f)
            {
                s.source.volume -= startVol * Time.deltaTime / duration;
                yield return null;
            }
            if (s.source.volume < 0f)
                s.source.volume = 0f;
            s.source.Stop();
            s.source.volume = startVol;
            yield return new WaitForSeconds(duration);
        }
    }

    private IEnumerator IFadeIn(string _name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            yield return null;
        }
        else
        {
            float maxVol = s.source.volume;
            s.source.volume = 0f;
            s.source.Play();
            while (s.source.volume < maxVol)
            {
                s.source.volume += maxVol * Time.deltaTime / duration;
                yield return null;
            }
            if (s.source.volume >= maxVol)
                s.source.volume = maxVol;
            yield return new WaitForSeconds(duration);
        }
    }

    public void BossLoop()
    {
        StartCoroutine(IBossLoop());
    }

    public void BossStop()
    {
        StopAllCoroutines();
        bossSource.Stop();
    }

    IEnumerator IBossLoop()
    {
        bossSource.volume = 0.3f;
        int i = 0;
        bossSource.clip = bossMusic[0];
        bossSource.Play();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //if (!bossSource.isPlaying)
            //{
            //    i = i + 1;
            //    if (i >= 3)
            //        i = 2;
            //    bossSource.clip = bossMusic[i];
            //    bossSource.Play();
            //}
            if(i == 0 && bossSource.time>= 66f)
            {
                i++;
                bossSource.clip = bossMusic[i];
                bossSource.Play();
            }
            if(i == 1 && bossSource.time >= 36f)
            {
                i++;
                bossSource.clip = bossMusic[i];
                bossSource.Play();
            }
            if(i == 2 && bossSource.time >= 65f)
            {
                break;
            }
        }
    }

    public void BossReturn(float _time)
    {
        if (bossSource.time - _time >= 0f)
            bossSource.time = bossSource.time - _time;
        else
            bossSource.time = 0f;
    }
}
