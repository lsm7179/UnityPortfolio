using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMoveCtrl : MonoBehaviour {


    public GameObject player;
    private static Vector3 offset;
    private Transform camTr;
	void Start () {
        offset = transform.position - player.transform.position;
        camTr = GetComponent<Transform>();
        PlayerPrefs.SetFloat("c_x", offset.x);
        PlayerPrefs.SetFloat("c_y", offset.y);
        PlayerPrefs.SetFloat("c_z", offset.z);
       offset = new Vector3(PlayerPrefs.GetFloat("c_x"), PlayerPrefs.GetFloat("c_y"), PlayerPrefs.GetFloat("c_z"));
    }
	
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        //cameraRotate();

	}




    //카메라 회전 제어
    void cameraRotate()
    {
     
        
        //Quaternion.Euler()
        if (Input.GetKeyDown(KeyCode.E))
        {
            camTr.RotateAround(player.transform.position, transform.position ,15f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            camTr.RotateAround(player.transform.position, transform.position, -15f);
        }
    }


    //카메라 회전 제어
    void cameraRotatePrev()
    {
        Quaternion rotation = Quaternion.identity;
        Quaternion prevRotation = camTr.rotation;


        //Quaternion.Euler()
        if (Input.GetKeyDown(KeyCode.E))
        {
            rotation.eulerAngles = new Vector3(camTr.eulerAngles.x, camTr.eulerAngles.y + 15, camTr.eulerAngles.z);
            camTr.rotation = rotation;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotation.eulerAngles = new Vector3(camTr.eulerAngles.x, camTr.eulerAngles.y - 15, camTr.eulerAngles.z);
            camTr.rotation = rotation;
        }
    }
}
