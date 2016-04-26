using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

	public AudioClip level1Music;

	private AudioSource source;

	// Use this for initialization
	void Awake () 
	{
		source = GetComponent<AudioSource> ();
	}

	void OnLevelWasLoaded(int level) 
	{
		if (level == 1) 
		{
			source.clip = level1Music;
			source.Play ();
		}
	}
}
