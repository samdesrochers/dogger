using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	public AudioSource SongSource; 
	public AudioSource SoundSource;
	
	public AudioClip[] SongClips;
	public AudioClip[] SoundClips;
	
	// For reference on how to load a clip programmaticaly
	private AudioClip loadedClip;
	
	public enum Sounds
	{
		lolz,
		thunder
	};
	
	private int songIndex = 0;
	
	void Start () 
	{
		SongSource.volume = 0.7f;
		SoundSource.volume = 0.7f;
		
		loadedClip = (AudioClip) Resources.Load ("Audio/Sounds/sound-thunder");
	}
	
	void Update()
	{
		if(Input.GetKey("m")) {
			PlayNextSong();
		}
		
		if(Input.GetKey("l")) {
			PlaySound((int)Sounds.lolz);
		}
		
		if(Input.GetKey("t")) {
			PlaySound((int)Sounds.thunder);
		}
		
		if(Input.GetKey("c")) {
			PlaySound(loadedClip);
		}
	}
	
	void PlayNextSong() 
	{
		SongSource.clip = SongClips[songIndex];
		SongSource.Play();
		
		songIndex = (songIndex + 1) % SongClips.Length;
	}
	
	/// <summary>
	/// Plays a pre loaded sound found in the Sound enum - Index pairing.
	/// </summary>
	/// <param name="index">Index.</param>
	void PlaySound(int index)
	{
		SoundSource.clip = SoundClips[index];
		SoundSource.Play();
	}
	
	/// <summary>
	/// Plays a sound from an existing AudioClip
	/// </summary>
	/// <param name="clip">Clip.</param>
	void PlaySound(AudioClip clip)
	{
		SoundSource.clip = clip;
		SoundSource.Play();
	}
}