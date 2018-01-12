using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundController : MonoBehaviour {
	public static SoundCollection collection;

	public SoundController() {
		collection = GameObject.Find ("GameManager").GetComponent<SoundCollection> ();
	}

	private static SoundController _instance = null;
	public static SoundController Instance()
	{
		if (_instance == null)
			_instance = new SoundController();
		return _instance;
	}

    private Sound FindSoundById(int id)
    {
        Sound s = new Sound();
        s.id = -1;
        s.Name = "invalid";
        s.soundType.id = -1;

        for (int i = 0; i < collection.sounds.Length; i++)
        {
			if (id == collection.sounds[i].id)
            {
				s = collection.sounds[i];
            }
        }

        return s;
    }

	private Sound FindSoundByName(string name)
    {
        Sound s = new Sound();
        s.id = -1;
        s.Name = "invalid";
        s.soundType.id = -1;

		for (int i = 0; i < collection.sounds.Length; i++)
        {
			if (name == collection.sounds[i].Name)
            {
				s = collection.sounds[i];
            }
        }

        return s;
    }

	private Sound FindSoundsByType(SoundType soundType)
    {
        Sound s = new Sound();

		for (int i = 0; i < collection.sounds.Length; i++)
        {
			if (soundType.id == collection.sounds[i].soundType.id || soundType.Name == collection.sounds[i].Name)
            {
				s = collection.sounds[i];
                break;
            }
        }

        return s;
    }

	public void PlaySound(int id, AudioSource source, bool loop = false)
    {
        Sound s = FindSoundById(id);
        source.clip = s.clip;
		source.loop = loop;

        source.Play();
    }

	public void PlaySound(string name, AudioSource source, bool loop = false)
    {
        Sound s = FindSoundByName(name);
        source.clip = s.clip;
		source.loop = loop;

        source.Play();
    }

	public void PlaySound(SoundType soundType, AudioSource source, bool loop = false)
    {
        Sound s = FindSoundsByType(soundType);
        source.clip = s.clip;
		source.loop = loop;

        source.Play();
    }

	public void StopSound(AudioSource source) {
		source.Stop ();
	}
}
