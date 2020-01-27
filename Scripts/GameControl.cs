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
	public static bool isSaveing= false;
	public static GameControl gameControl;
	public int killNum = 9;
	[Header("챕터완료시 씬전환")]
	public GameObject GameClearPanel;
	public Text ClearText;
	[SerializeField]
	private float FadeTime = 1.2f; //Fade효과 재생시간
	[SerializeField]
	private float time;
	[SerializeField]
	private Image fadeImg;
	void Awake () {
		gameControl = this;
		_player = GameObject.FindWithTag("Player").gameObject;
		if (isSaveing)
		{
		_player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
		_player.transform.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("Cam_y"), 0);
		}
		fadeImg = GameClearPanel.GetComponent<Image>();
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

	//킬 체크 후 챕터완료 화면으로 이동
	public void KillChk()
	{
		killNum--;
		if (killNum <= 0)
		{
			//완료 화면으로 이동
			StartCoroutine(Clear());
		}
	}
	IEnumerator Clear()
	{

		Color fadeColor = fadeImg.color;
		time = 0f;
		ClearText.text = "ChapterClear";
		GameClearPanel.SetActive(true);
		while (fadeColor.a < 1f)
		{
			time += Time.deltaTime / FadeTime;
			fadeColor.a = Mathf.Lerp(0f, 1f, time);
			fadeImg.color = fadeColor;
			yield return null;
		}
		SceneManager.LoadScene("GameStart");
		yield return null;
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
			isSaveing = true;
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
