using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour {

	public GameObject _light;
	public Transform DropDown;

	void Update() {
		if (DropDown.GetComponent<Dropdown>().value == 0)
		{
			_light.GetComponent<Light>().shadows = LightShadows.Soft;
		}
		if (DropDown.GetComponent<Dropdown>().value == 1)
		{
			_light.GetComponent<Light>().shadows = LightShadows.Hard;
		}
		if (DropDown.GetComponent<Dropdown>().value == 2)
		{
			_light.GetComponent<Light>().shadows = LightShadows.None;
		}

	}
}
