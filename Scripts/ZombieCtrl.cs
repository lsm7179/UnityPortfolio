using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//모바일 게임의 최적화 소스 
//오브젝트 풀링 기법 
public enum ZomBieState {idle=1,trace,attack,hit,die};

public class ZombieCtrl : MonoBehaviour
{
    public ZomBieState zombiestate = ZomBieState.idle;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform thisTr;
    [SerializeField]
    private NavMeshAgent Navi;
    [SerializeField]
    private Animator Ani;
    
    [SerializeField]
    private Image HpBar;
    public Canvas thisCanvas;
    public float attackdist = 3.0f;
    public float tracedist = 30f;
    public bool IsDie = false;
    private int HpInit = 100;
    private int Hp = 100;
    private GameObject hitEffect;
    public GameObject MpSphere = null;
    public GameObject HpSphere = null;


    void Awake()
    {
        
        _player = GameObject.FindWithTag("Player")
              .GetComponent<Transform>();
        thisTr = GetComponent<Transform>();
        Navi = GetComponent<NavMeshAgent>();
        Ani = GetComponent<Animator>();
        HpBar = GameObject.FindWithTag("HpBar").GetComponent<Image>();
        
        hitEffect = Resources.Load<GameObject>("Effect/HitParticle");
        MpSphere = Resources.Load<GameObject>("Effect/MpSphere");
        HpSphere = Resources.Load<GameObject>("Effect/HpSphere");

        Navi.destination = _player.position;
         HpBar.color = Color.green;
    }
    private void OnEnable() //Start()함수보다 빠르게 호출 
    {                       //오브젝트가 활성화 되었을 때 
        StartCoroutine(ZombieStateCheck());
        StartCoroutine(ZombieStateAction());
    }
    IEnumerator ZombieStateCheck()
    {
        while(!IsDie)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(_player.position,
                                                  thisTr.position);
            if (dist <= attackdist)
                zombiestate = ZomBieState.attack;
            else if (dist <= tracedist)
                zombiestate = ZomBieState.trace;
            else
                zombiestate = ZomBieState.idle;

        }


    }
    IEnumerator ZombieStateAction()
    {
        while(!IsDie)
        {
            switch(zombiestate)
            {
                case ZomBieState.idle:
                    Navi.isStopped = true;
                    Ani.SetBool("IsTrace", false);
                    break;
                case ZomBieState.trace:
                    Navi.isStopped = false;
                    Navi.destination = _player.position;
                    Ani.SetBool("IsTrace", true);
                    Ani.SetBool("IsAttack", false);
                    break;
                case ZomBieState.attack:
                    Navi.isStopped = true;
                    Ani.SetBool("IsAttack", true);
                    break;
            }
            yield return null;

        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            Ani.SetTrigger("IsHit");
            Hit(other.transform.position);
            MinusHp();
            other.enabled = false;
        }
    }
    void MinusHp()
    {
        Hp -= 35;
        HpBar.fillAmount = (float)Hp / (float)HpInit;
        if (HpBar.fillAmount <= 0.3f)
            HpBar.color = Color.red;
        else if (HpBar.fillAmount <= 0.5f)
            HpBar.color = Color.yellow;
        if (Hp <= 0)
            Die();
    }
    void Hit(Vector3 hitPos)
    {
        GameObject hitEff = Instantiate(hitEffect, hitPos, Quaternion.identity);
        Destroy(hitEff, 0.5f);
    }
   
    void Die()
    {
        zombiestate = ZomBieState.die;
        Navi.isStopped = true;
        Ani.SetTrigger("IsDie");
        IsDie = true;
        GetComponent<CapsuleCollider>().enabled = false;
        thisCanvas.enabled = false;
        GameControl.gameControl.KillChk();//킬 체크
        StopAllCoroutines();//모든 스타트 코루틴 중지
        //StartCoroutine(PushPool());
    }
    /*IEnumerator PushPool()
    {
        yield return new WaitForSeconds(2.0f);
        DestoryEffect();
        //mp, hp 회복구
        //MpSphere.SetActive(true);
        float mpx = transform.position.x + Random.Range(-2, 2);
        float mpz = transform.position.z + Random.Range(-2, 2);
        float hpx = transform.position.x + Random.Range(-2, 2);
        float hpz = transform.position.z + Random.Range(-2, 2);
        GameObject MpSphere_ = (GameObject)Instantiate(MpSphere, new Vector3(mpx, MpSphere.transform.position.y, mpz), Quaternion.identity);
        MpSphere_.name = "MpSphere";
        GameObject HpSphere_ = (GameObject)Instantiate(HpSphere, new Vector3(hpx, HpSphere.transform.position.y, hpz), Quaternion.identity);
        HpSphere_.name = "HpSphere";
        yield return new WaitForSeconds(1.0f);
        IsDie = false;
        zombiestate = ZomBieState.idle;
        thisCanvas.enabled = true;
        hpBar.fillAmount = 1.0f;
        Hp = 100;
        GetComponent<CapsuleCollider>().enabled = true;
        gameObject.SetActive(false);
    }

    void DestoryEffect()
    {
        GameObject DestoryEffect_ = Instantiate(DestroyEffect, this.transform.position, Quaternion.identity);
        Destroy(DestoryEffect_, 0.8f);
    }*/
}
