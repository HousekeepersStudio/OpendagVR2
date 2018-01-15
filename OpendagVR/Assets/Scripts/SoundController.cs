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

	/// <summary>
	/// Finds the sound by identifier.
	/// </summary>
	/// <returns>The sound by identifier.</returns>
	/// <param name="id">Identifier.</param>
    private Sound FindSoundById(int id)
    {
        Sound s = new Sound();
        s.id = -1;
        s.Name = "invalid";
        s.typeId = -1;

        for (int i = 0; i < collection.sounds.Length; i++)
        {
			if (id == collection.sounds[i].id)
            {
				s = collection.sounds[i];
            }
        }

        return s;
    }

	/// <summary>
	/// Finds the name of the sound by its assigned name.
	/// </summary>
	/// <returns>The sound by name.</returns>
	/// <param name="name">Name.</param>
	private Sound FindSoundByName(string name)
    {
        Sound s = new Sound();
        s.id = -1;
        s.Name = "invalid";
        s.typeId = -1;

		for (int i = 0; i < collection.sounds.Length; i++)
        {
			if (name == collection.sounds[i].Name)
            {
				s = collection.sounds[i];
            }
        }

        return s;
    }

	/// <summary>
	/// Finds the sounds by sound type identifier.
	/// </summary>
	/// <returns>The sounds by type identifier.</returns>
	/// <param name="id">Type Identifier.</param>
	private Sound FindSoundsByTypeId(int id)
	{
		Sound s = new Sound();
		SoundType soundType = null;
		for (int i = 0; i < collection.soundTypes.Length; i++) {
			if (id == collection.soundTypes [i].id) {
				soundType = collection.soundTypes [i];
			}
		}
		if (soundType != null) {
			for (int i = 0; i < collection.sounds.Length; i++) {
				if (soundType.id == collection.sounds [i].typeId) {
					s = collection.sounds [i];
					break;
				}
			}
		}

		return s;
	}

	/// <summary>
	/// Finds the name of the sounds by sound type.
	/// </summary>
	/// <returns>The sounds by type name.</returns>
	/// <param name="name">Type Name.</param>
	private Sound FindSoundsByTypeName(string name)
	{
		Sound s = new Sound();
		SoundType soundType = null;
		for (int i = 0; i < collection.soundTypes.Length; i++) {
			if (name == collection.soundTypes [i].Name) {
				soundType = collection.soundTypes [i];
				break;
			}
		}
		if (soundType != null) {
			for (int i = 0; i < collection.sounds.Length; i++) {
				if (soundType.id == collection.sounds [i].typeId) {
					s = collection.sounds [i];
					break;
				}
			}
		}
		return s;
	}

	/// <summary>
	/// Plays the sound.
	/// </summary>
	/// <param name="id">Sound Id.</param>
	/// <param name="source">Audio Source.</param>
	/// <param name="volume">Volume.</param>
	/// <param name="loop">If set to <c>true</c> loop.</param>
	public void PlaySound(int id, AudioSource source, float volume = 1.0f, bool loop = false)
    {
        Sound s = FindSoundById(id);
        source.clip = s.clip;
		source.loop = loop;
		source.volume = volume;

        source.Play();
    }

	/// <summary>
	/// Plays the sound.
	/// </summary>
	/// <param name="name">Sound Name.</param>
	/// <param name="source">Audio Source.</param>
	/// <param name="volume">Volume.</param>
	/// <param name="loop">If set to <c>true</c> loop.</param>
	public void PlaySound(string name, AudioSource source, float volume = 1.0f, bool loop = false)
    {
        Sound s = FindSoundByName(name);
        source.clip = s.clip;
		source.loop = loop;
		source.volume = volume;

        source.Play();
    }

	public void PlaySoundByTypeId(int soundTypeId, AudioSource source, float volume = 1.0f, bool loop = false)
	{
		Sound s = FindSoundsByTypeId(soundTypeId);
		source.clip = s.clip;
		source.loop = loop;
		source.volume = volume;

		source.Play();
	}

	public void PlaySoundByTypeName(string soundTypeName, AudioSource source, float volume = 1.0f, bool loop = false)
    {
        Sound s = FindSoundsByTypeName(soundTypeName);
        source.clip = s.clip;
		source.loop = loop;
		source.volume = volume;

		source.Play();
    }

	/// <summary>
	/// Stops the sound.
	/// </summary>
	/// <param name="source">Audio Source.</param>
	public void StopSound(AudioSource source) {
		source.Stop ();
	}
}
