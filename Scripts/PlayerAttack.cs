using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator ani;
    //플레이어가 공격하는 모션 제어
    float timer;
    float waitingTime;
    void Start () {
        ani = GetComponent<Animator>();
        timer = 0.0f;
        waitingTime = 0.3f;
    }
	
	void Update () {

		if(Input.GetMouseButtonDown(0))
        {

            ani.SetBool("IsAttack",true);


        }
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            timer = 0.0f;
            ani.SetBool("IsAttack", false);
        }
    }
}
