using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour {

	public static SoundControl soundctrl;

	public float sfxVolume = 1.0f;
	public bool isSfxMute = false;
	
	void Start () {
		soundctrl = this;
	}

	public void Volume(float volume)
	{
		sfxVolume = volume;
	}

	public void PlayerSfx(Vector3 pos, AudioClip sfx)
	{
		if (isSfxMute) return;
		GameObject soundObj = new GameObject("Sfx");
		//동적할당 동적 생성
		soundObj.transform.position = pos;
		AudioSource audioSource = soundObj.AddComponent<AudioSource>();

		audioSource.clip = sfx;
		audioSource.minDistance = 10f;
		audioSource.maxDistance = 30f;
		audioSource.volume = sfxVolume;
		audioSource.Play();
		Destroy(soundObj, sfx.length);
	}

}
