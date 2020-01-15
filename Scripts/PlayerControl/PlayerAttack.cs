using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    private Animator ani;
    //플레이어가 공격하는 모션 제어
    private GameObject bladeEffactPrefab;
    public Transform effectPosTr;
    public Collider swordCollider;
    public bool check = true;
    public TrailRenderer swordRenderer;
    [SerializeField]
    AudioClip AttackSound;
    //public float skillCoolTime = 3.0f;
    public Image img_Skill;
    public Image mpBar;
    public int mpInit = 100;
    public int mp = 100;
    public Text mpText = null;
    private Rigidbody rbody = null;
    public Text skillText;
    void Start () {
        swordCollider.enabled = false;
        ani = GetComponent<Animator>();
        bladeEffactPrefab = Resources.Load<GameObject>("Effect/BladeStorm");
        swordRenderer = swordCollider.gameObject.GetComponent<TrailRenderer>();
        AttackSound = Resources.Load<AudioClip>("Sounds/sword_throw");
        rbody = GetComponent<Rigidbody>();
    }
	
	/*void FixedUpdate () {
        Attack();
    }*/

    public void Attack()
    {
        //if (Input.GetMouseButtonDown(0) && check)
        //if (Input.GetKeyDown(KeyCode.A) && check)
        
        StartCoroutine(WaitForAttack(0.16f));
        swordRenderer.enabled = true;
        swordCollider.enabled = true;
        PlayerAniCtrl.IsWalk = false;
        PlayerAniCtrl.IsAttack = true;
        StartCoroutine(WaitForIt(0.8f));



    }
    IEnumerator WaitForAttack(float time)
    {
        yield return new WaitForSeconds(time);
    }
        IEnumerator WaitForIt(float time)
    {
        yield return new WaitForSeconds(time);
        SoundControl.soundctrl.PlayerSfx(transform.position, AttackSound);
        swordCollider.enabled = false;
        swordRenderer.enabled = false;
        PlayerAniCtrl.IsAttack = false;
        swordRenderer.widthMultiplier = 1;
    }

    public void OnSkillDown()
    {
        //if (Input.GetKeyDown(KeyCode.Q) && check)
        if (img_Skill.fillAmount >= 1.0f&&mp>=20)
        {
            ani.SetTrigger("IsSkill");
            mp -= 20;
            mpBar.fillAmount = (float)mp / (float)mpInit;
            mpText.text = mp + " / " + mpInit;
            swordRenderer.widthMultiplier = 10;
            swordRenderer.enabled = true;
            swordCollider.enabled = true;
            PlayerAniCtrl.IsWalk = false;
            PlayerAniCtrl.IsAttack = false;
            StartCoroutine(CoolTime(3f));
            StartCoroutine(WaitForIt(1f));
        }
        else
        {
            if (!skillText.enabled)
            {
            StartCoroutine(SkillTextVoid());
            }
        }
    }
    IEnumerator SkillTextVoid()
    {
        
        skillText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        skillText.enabled = false;
    }
    IEnumerator CoolTime(float cool)
    {
        float temp = 0.0f;
        while (temp <= cool)
        {
            //0에서 1까지 이고 
            //3초동안을 0에서 1로 표현해야함.
            temp += Time.deltaTime;
            img_Skill.fillAmount = (temp / cool);
            yield return new WaitForFixedUpdate();
        }
    }

}
