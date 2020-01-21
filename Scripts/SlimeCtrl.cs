using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//모바일 최적화 된 스크립트 소스 작성
public class SlimeCtrl : MonoBehaviour {
	//슬라임과 플레이어의 거리를 재어 추적과 공격하기

	public float speed = 2.0f;
	public float rotSpeed = 5.0f;
	public float TraceDist = 11f;
	[SerializeField]
	private Transform slimeTr;
	[SerializeField]
	private Transform playerTr;

	private Animator Ani;
	[SerializeField]
	private Image hpBar;
	[SerializeField]
	private Canvas thisCanvas;
	[SerializeField]
	private GameObject hitEffect;
	private int HpInit = 130;
	private int Hp = 130;

	public enum SlimeState { idle = 0, trace, attack, die };
	public SlimeState thisState = SlimeState.idle;
	public GameObject MpSphere = null;
	public GameObject HpSphere = null;
	private GameObject DestroyEffect = null;
	void Awake () {
		slimeTr=GetComponent<Transform>();
		playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
		Ani = GetComponent<Animator>();
		hpBar = GetComponentInChildren<Image>();
		thisCanvas = GetComponentInChildren<Canvas>();
		hitEffect = Resources.Load<GameObject>("Effect/HitParticle");
		this.enabled = true;
		DestroyEffect = Resources.Load<GameObject>("Effect/DestoryParticle");
		MpSphere = Resources.Load<GameObject>("Effect/MpSphere");
		HpSphere = Resources.Load<GameObject>("Effect/HpSphere");
	}

	void FixedUpdate()
	{
		Trace();
	}

	//슬라임이 active 상태일때 메소드가 실행된다.
	void OnEnable()
	{
		StartCoroutine(Action());
	}

	//몬스터 액션
	IEnumerator Action()
	{
		
		while (thisState!=SlimeState.die)
		{
			yield return new WaitForSeconds(0.2f);
			float dist = Vector3.Distance(playerTr.position, slimeTr.position);
			if (dist <= TraceDist && dist > 2.5f)
			{
				thisState = SlimeState.trace;
				Ani.SetBool("IsAttack", false);
				Ani.SetBool("IsTrace", true);

			}
			else if (dist <= 2.5f)
			{
				thisState = SlimeState.attack;
				slimeTr.rotation = Quaternion.Slerp(slimeTr.rotation, Quaternion.LookRotation(playerTr.position - slimeTr.position), Time.deltaTime * 3);
				Ani.SetBool("IsTrace", false);
				Ani.SetBool("IsAttack", true);
				yield return new WaitForSeconds(1.5f);
			}
			else
			{
				thisState = SlimeState.idle;
				Ani.SetBool("IsTrace", false);
				Ani.SetBool("IsAttack", false);
			}
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Sword"))
		{
			Ani.SetTrigger("IsHit");
			Hit(other.transform.position);
			MinusHp();
		}
	}

	void MinusHp()
	{
		Hp -= 35;
		hpBar.fillAmount = (float)Hp / (float)HpInit;

		if (Hp <= 0)//죽었을때
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
		thisState = SlimeState.die;
		Ani.SetBool("IsTrace", false);
		Ani.SetTrigger("IsDie");
		thisCanvas.enabled = false;
		
		StopAllCoroutines();
		StartCoroutine(PushPool());
	}
	IEnumerator PushPool()
	{
		GetComponentInChildren<CapsuleCollider>().enabled = false;
		yield return new WaitForSeconds(1.5f);
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
		thisState = SlimeState.idle;
		thisCanvas.enabled = true;
		hpBar.fillAmount = 1.0f;
		Hp = 130;
		GetComponentInChildren<CapsuleCollider>().enabled = true;
		gameObject.SetActive(false);
	}

	void DestoryEffect()
	{
		GameObject DestoryEffect_ = Instantiate(DestroyEffect, this.transform.position, Quaternion.identity);
		Destroy(DestoryEffect_, 0.8f);
	}

	/// <summary>
	/// 추적하는 함수
	/// </summary>
	void Trace()
	{
		if (thisState != SlimeState.trace || Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			//Ani.SetBool("IsTrace", false); 
			return;
		}
		slimeTr.rotation = Quaternion.Slerp(slimeTr.rotation, Quaternion.LookRotation(playerTr.position - slimeTr.position), Time.deltaTime * 3);
		slimeTr.Translate(Vector3.forward * speed * Time.deltaTime);
	}


}
