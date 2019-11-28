using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkeleteonCtrl : MonoBehaviour {
    /*스켈레톤 컨트롤
      자신과 플레이어의 거리를 재서 추적과 공격을 한다.*/
    [SerializeField]
    private NavMeshAgent Navi;
    private Transform SkeletonTr;
    private Transform PlayerTr;
    public float TraceDist = 30f;
    private Animator Ani;
    private Image hpBar;
	void Start () {
        Navi = GetComponent<NavMeshAgent>();
        SkeletonTr = GetComponent<Transform>();
        PlayerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Ani = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(PlayerTr.position, SkeletonTr.position);
        if(dist <= TraceDist&& dist > 2.0f)
        {
            Navi.isStopped = false;
            Navi.destination = PlayerTr.position;
            Ani.SetBool("IsAttack", false);
            Ani.SetBool("IsTrace", true);
        }
        else if(dist <= 2.0f)
        {
            Navi.isStopped = true;
            Ani.SetBool("IsTrace", false);
            Ani.SetBool("IsAttack", false);
        }
	}
}
