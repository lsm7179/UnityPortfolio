using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiFadeIn : MonoBehaviour {

	public float FadeTime = 0.8f; //Fade효과 재생시간
	public Image fadeImg;
	float start;
	float end;
	float time = 0f;
	bool isPlaying = false;

	void Awake()
	{
		fadeImg = GetComponent<Image>();
		
	}
	public void OutStartFadeAnim()
	{
		if (isPlaying == true)
		{
			return;
		}
		start = 0f;
		end = 1f;
		StartCoroutine(fadeoutplay());
	}
	
	IEnumerator fadeoutplay()
	{
		isPlaying = true;
		Color fadeColor = fadeImg.color;
		time = 0f;

		while (fadeColor.a < 1f)
		{
			time += Time.deltaTime / FadeTime;
			fadeColor.a = Mathf.Lerp(start, end, time);
			fadeImg.color = fadeColor;
			yield return null;
		}
		SceneManager.LoadScene("play");
		isPlaying = false;
		yield return null;
	}

}
