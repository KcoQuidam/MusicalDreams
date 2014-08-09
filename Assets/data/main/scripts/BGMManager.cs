using UnityEngine;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour {

/** Unity configuration ***********************/

	public AudioClip[] bgms;
	public float[] volumes;

	public string startBGM = "";

/** Callbacks Unity ***************************/

	/* Unity */ void Start() {
		MainVolume = 1;

		for(int i = 0 ; i < bgms.Length; i++) {
			AudioClip clip = bgms[i];
			AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();

			audioSource.clip = clip;
			audioSource.loop = true;
			audioSource.volume = 0;

			audioSources.Add(clip.name, audioSource);
			audioVolumes.Add(clip.name, volumes[i]);
		}

		AudioSource first; float firstVolume;
		if (audioSources.TryGetValue (this.startBGM, out first) && audioVolumes.TryGetValue(this.startBGM, out firstVolume)) {
			this.current = first;
			first.volume = firstVolume*MainVolume;
		}

		foreach (AudioSource audioSource in audioSources.Values) {
			audioSource.Play();
		}
	}

	/* Unity */ void Update() {
	}

/** Publique properties ***********************/

	public float MainVolume {
		get;
		set;
	}

/** Action methods ****************************/

	public void ChangeBGM(string bgmName)
	{
		if (bgmName.Equals (current.clip.name)) {
			return;
		}

		AudioSource next; float nextVolume;
		if (audioSources.TryGetValue (bgmName, out next) && audioVolumes.TryGetValue(bgmName, out nextVolume)) {
			current.volume = 0;
			next.volume = nextVolume*MainVolume;

			current = next;
		}
	}
	
/** Private attributes ************************/

	private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
	private Dictionary<string, float> audioVolumes = new Dictionary<string, float>();
	private AudioSource current;
}
