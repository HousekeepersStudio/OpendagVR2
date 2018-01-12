using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string Name;
	public int id;
	public AudioClip clip;
	public SoundType soundType;
}

[System.Serializable]
public class SoundType
{
	public string Name;
	public int id;
}

public class SoundCollection : MonoBehaviour {
	public Sound[] sounds;
	public SoundType[] soundTypes;
}
