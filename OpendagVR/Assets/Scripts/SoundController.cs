using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public int id;
    public string name;
    public AudioClip clip;
    public SoundType soundType;
}

[System.Serializable]
public class SoundType
{
    public int id;
    public string name;
}

public class SoundController : MonoBehaviour {
    public Sound[] sounds;
    public SoundType[] soundTypes;

    public Sound FindSoundById(int id)
    {
        Sound s = new Sound();
        s.id = -1;
        s.name = "invalid";
        s.soundType.id = -1;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (id == sounds[i].id)
            {
                s = sounds[i];
            }
        }

        return s;
    }

    public Sound FindSoundByName(string name)
    {
        Sound s = new Sound();
        s.id = -1;
        s.name = "invalid";
        s.soundType.id = -1;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {
                s = sounds[i];
            }
        }

        return s;
    }

    public Sound FindSoundsByType(SoundType soundType)
    {
        Sound s = new Sound();

        for (int i = 0; i < sounds.Length; i++)
        {
            if (soundType.id == sounds[i].soundType.id || soundType.name == sounds[i].name)
            {
                s = sounds[i];
                break;
            }
        }

        return s;
    }

    public void PlaySound(int id, AudioSource source)
    {
        Sound s = FindSoundById(id);
        source.clip = s.clip;

        source.Play();
    }

    public void PlaySound(string name, AudioSource source)
    {
        Sound s = FindSoundByName(name);
        source.clip = s.clip;

        source.Play();
    }

    public void PlaySound(SoundType soundType, AudioSource source)
    {
        Sound s = FindSoundsByType(soundType);
        source.clip = s.clip;

        source.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
