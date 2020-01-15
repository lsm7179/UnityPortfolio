using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //플레이어 제어
    public float moveSpeed = 5.50f;
    public float rotSpeed = 17.0f;
    private Transform playTr;


    private Camera mainCamera;
    private Vector3 targetPos; // 캐릭터의 이동 타겟 위치
    float v = 0f;
    float h = 0f;


    void Awake () {
        playTr = GetComponent<Transform>();
        //mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
        mainCamera = GameObject.Find("Camera_Mouse").GetComponent<Camera>();
        targetPos = playTr.position;
    }
		
	void FixedUpdate () {
        //Move();
        MoveMobile();
    }

    public void OnStickChanged(Vector3 stickPos)
    {      //터치패드의 움직임 방향을 받아드리는 함수.
        h = stickPos.x;
        v = stickPos.y;

    }

    void MoveMobile()
    {
        Rigidbody rbody = GetComponent<Rigidbody>();
        if (rbody)
        {
            Vector3 speed = rbody.velocity;
            //rbody 의 힘과 방향을 speed에 대입
            speed.x = 6f * h;
            speed.z = 6f * v;
            rbody.velocity = speed;
            if (h != 0 || v != 0)//터치패드가 움직인다면.
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                //벡터값을 이용해서 회전
            }
        }
        //playTr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        //playTr.Rotate(Vector3.up * h * rotSpeed * Time.deltaTime);
        StartCoroutine(WalkCheck());
    }
    
    //걷는 애니메이션 체크
    IEnumerator WalkCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (v != 0f || h != 0f)//
            {
                PlayerAniCtrl.IsWalk = true;
            }
            else
            {
                PlayerAniCtrl.IsWalk = false;
            }
        }
    }

    //플레이어 이동 및 이동 애니메이션 제어
    void Move()
    {
        // 마우스 입력을 받았 을 때
        if (Input.GetMouseButtonUp(0))
        {
            // 마우스로 찍은 위치의 좌표 값을 가져온다
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                targetPos = hit.point;
                targetPos.y = 0;
            }
            PlayerAniCtrl.IsWalk = true;
        }
        // 캐릭터가 움직이고 있다면
        if (Run(targetPos))
        {
            // 회전도 같이 시켜준다
            Turn(targetPos);
        }
    }



    public bool Run(Vector3 targetPos)
    {
        // 이동하고자하는 좌표 값과 현재 내 위치의 차이를 구한다.
        float dis = Vector3.Distance(transform.position, targetPos);
       
        if (dis >= 0.01f&&PlayerAniCtrl.IsWalk) // 차이가 아직 있고 상태가 걷는 상태라면
        {
            // 캐릭터를 이동시킨다.
            transform.localPosition = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            return true;
        }
        PlayerAniCtrl.IsWalk = false;
        return false;

    }

    public void Turn(Vector3 targetPos)
    {
        // 캐릭터를 이동하고자 하는 좌표값 방향으로 회전시킨다
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        playTr.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 550.0f * Time.deltaTime);
    }
}

