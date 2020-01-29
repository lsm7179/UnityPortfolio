using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GMStartManager : MonoBehaviour {

    public Text buttonText;
    public GameObject _player;

    public void Awake()
    {
        Screen.SetResolution(1280, 800, true);
        if (GameControl.isSaveing)
        {
         buttonText.text = "다시하기";
         _player.transform.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
         _player.transform.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("Cam_y"), 0);
        }
    }

	void Update()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
                Application.Quit();
            }
		}
		
	}

}
