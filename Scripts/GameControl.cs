using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public RectTransform PauseImage;
	public RectTransform PauseMenu;

	public RectTransform ScreenMenu;
	public RectTransform SoundMenu;

	private GameObject _player;

	void Awake () {
		_player = GameObject.FindWithTag("Player").gameObject;
		//_player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
		//_player.transform.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("Cam_y"), 0);
	}
	
	void Update () {
		if(Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Pause();
			}
		}
		if(Application.platform == RuntimePlatform.WindowsEditor)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Pause();
			}
		}
	}

	public void Pause()
	{
		if (!PauseImage.gameObject.activeInHierarchy)
		{
			if (!PauseMenu.gameObject.activeInHierarchy)
			{
				PauseMenu.gameObject.SetActive(true);
				SoundMenu.gameObject.SetActive(false);
				ScreenMenu.gameObject.SetActive(false);
			}
			PauseImage.gameObject.SetActive(true);
			Time.timeScale = 0f;
			_player.SetActive(false);
		}
		else
		{
			PauseImage.gameObject.SetActive(false);
			Time.timeScale = 1f;
			_player.SetActive(true);
		}
	}

	public void SaveSettings(bool isQuit)
	{
		PlayerPrefs.SetFloat("x", _player.transform.position.x);
		PlayerPrefs.SetFloat("y", _player.transform.position.y);
		PlayerPrefs.SetFloat("z", _player.transform.position.z);

		PlayerPrefs.SetFloat("Cam_y", _player.transform.eulerAngles.y);

		if (isQuit)
		{
			Time.timeScale = 1;
			SceneManager.LoadScene("GameStart");
		}

	}
	public void PlayGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void Sounds(bool Open)
	{
		if (Open)
		{
			SoundMenu.gameObject.SetActive(true);
			ScreenMenu.gameObject.SetActive(false);
			PauseMenu.gameObject.SetActive(false);
		}
		else
		{
			SoundMenu.gameObject.SetActive(false);
			PauseMenu.gameObject.SetActive(true);
		}

	}
	public void ScreenSetting(bool Open)
	{
		if (Open)
		{
			SoundMenu.gameObject.SetActive(false);
			ScreenMenu.gameObject.SetActive(true);
			PauseMenu.gameObject.SetActive(false);
		}
		else
		{
			ScreenMenu.gameObject.SetActive(false);
			PauseMenu.gameObject.SetActive(true);
		}
	}
}
