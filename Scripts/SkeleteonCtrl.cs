using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//모바일에 최적화 된 스크립트 소스 작성
public class SkeleteonCtrl : MonoBehaviour {
    /*스켈레톤 컨트롤
      자신과 플레이어의 거리를 재서 추적과 공격을 한다.*/
    [SerializeField]
    private NavMeshAgent Navi;
    private Transform SkeletonTr;
    private Transform PlayerTr;
    public float TraceDist = 30f;
    private Animator Ani;
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Canvas thisCanvas;
    private int HpInit = 100;
    private int Hp = 100;
    private GameObject hitEffect;
    private bool isDie =false;
	void Awake () {
        Navi = GetComponent<NavMeshAgent>();
        SkeletonTr = GetComponent<Transform>();
        PlayerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Ani = GetComponent<Animator>();
        //hpBar = GetComponentInChildren<Image>();
        thisCanvas = GetComponentInChildren<Canvas>();
        hitEffect = Resources.Load<GameObject>("Effect/HitParticle");
    }
	
	void Update () {
        Behavior();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("좀비가 맞는 로직");
        if (other.gameObject.CompareTag("Sword"))
        {
            Ani.SetTrigger("IsHit");
            Hit(other.transform.position);
            MinusHp();
        }
    }

    void Behavior()
    {
        if (!isDie)
        {
            float dist = Vector3.Distance(PlayerTr.position, SkeletonTr.position);
            if (dist <= TraceDist && dist > 2.0f)
            {
            
                    Navi.isStopped = false;
                    Navi.destination = PlayerTr.position;
                    Ani.SetBool("IsAttack", false);
                    Ani.SetBool("IsTrace", true);
         
            }
            else if (dist <= 2.0f)
            {
                Navi.isStopped = true;
                Ani.SetBool("IsTrace", false);
                Ani.SetBool("IsAttack", false);
            }
        }
    }

    void MinusHp()
    {
        Hp -= 35;
        hpBar.fillAmount = (float)Hp / (float)HpInit;
        Debug.Log(Hp);
        
        if(Hp <= 0)//죽었을때
        {
            Die();
        }
    }

    void Hit(Vector3 hitPos)
    {
        GameObject hitEff = Instantiate(hitEffect, hitPos, Quaternion.identity);
        Destroy(hitEff, 0.5f);
    }
    void Die()//캔버스 , hp 초기화, 내비게이션 초기화
    {
        isDie=true;
        Ani.SetTrigger("IsDie");
        Navi.isStopped = true;
        thisCanvas.enabled = false;
        Hp = 100;
        GetComponent<CapsuleCollider>().enabled = false;
        

    }
}
