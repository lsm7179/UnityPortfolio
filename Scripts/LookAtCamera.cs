using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private Transform mainCamTr;
    private Transform thisCanvasTr;

	void Start () {
        mainCamTr = Camera.main.transform;
        thisCanvasTr = GetComponent<Transform>();
	}
	
	void Update () {
        thisCanvasTr.LookAt(mainCamTr);
	}
}
