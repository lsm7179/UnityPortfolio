using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //플레이어 제어
    public float moveSpeed = 7.0f;
    public float rotSpeed = 40.0f;
    float vertical, horizontal = 0f;
    private Transform playTr;
    private Animator ani;
    private bool isWalk = false;
	void Start () {
        playTr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
	}
	
	
	void FixedUpdate () {

        move();
        


    }

    //플레이어 이동 및 이동 애니메이션 제어
    void move()
    {
        vertical = Input.GetAxis("Vertical"); //ws
        horizontal = Input.GetAxis("Horizontal"); //ad
        if (vertical != 0 || horizontal != 0)
          isWalk = true;
        else isWalk = false;
        
        ani.SetBool("IsWalk", isWalk);
       float vMove = vertical, hMove = horizontal;
        if (vMove < 0) vMove *= -1;
        if (hMove < 0) hMove *= -1;
        if (vertical != 0 && horizontal != 0)
        {
            playTr.Translate(Vector3.forward * vMove * moveSpeed * Time.deltaTime);
        }
        else
        {
            playTr.Translate(Vector3.forward * vMove * moveSpeed * Time.deltaTime);
            playTr.Translate(Vector3.forward * hMove * moveSpeed * Time.deltaTime);
        } 
       

        Quaternion rotation = Quaternion.identity;
        Quaternion prevRotation = playTr.rotation;
        //0 45 90 
        if (vertical > 0.01f && horizontal == 0.0f)
        {

            rotation.eulerAngles = new Vector3(0, 0, 0);
            playTr.rotation = rotation;

        }
        else if (vertical < -0.01f && horizontal == 0.0f)
        {
            rotation.eulerAngles = new Vector3(0, 180, 0);
            playTr.rotation = rotation;
        }
        else if (vertical == 0 && horizontal > 0.01f)
        {
            rotation.eulerAngles = new Vector3(0, 90, 0);
            playTr.rotation = rotation;
        }
        else if (vertical == 0 && horizontal < -0.01f)
        {
            rotation.eulerAngles = new Vector3(0, -90, 0);
            playTr.rotation = rotation;
        }
        else if (vertical > 0.01f && horizontal > 0.01f)
        {
            rotation.eulerAngles = new Vector3(0, 45, 0);
            playTr.rotation = rotation;
        }
        else if (vertical > 0.01f && horizontal < -0.01f)
        {
            rotation.eulerAngles = new Vector3(0, -45, 0);
            playTr.rotation = rotation;
        }
        else if (vertical < -0.01f && horizontal > 0.01f)
        {
            rotation.eulerAngles = new Vector3(0, 135, 0);
            playTr.rotation = rotation;
        }
        else if (vertical < -0.01f && horizontal < -0.01f)
        {
            rotation.eulerAngles = new Vector3(0, -135, 0);
            playTr.rotation = rotation;
        }

        else
        {
            playTr.rotation = prevRotation;
        }

    }
}

