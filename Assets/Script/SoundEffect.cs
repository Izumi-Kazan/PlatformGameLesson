using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

	public AudioClip seClear;
	public AudioClip seScream;
	
	public void PlayClear() {
		GetComponent<AudioSource>().PlayOneShot(seClear);
	}
	
	public void PlayScream() {
		GetComponent<AudioSource>().PlayOneShot(seScream);
	}
}
