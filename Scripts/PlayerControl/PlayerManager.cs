using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public Image hpBar;
    public int hpInit = 500;
    public int hp = 500;
    public int mpInit = 100;
    public int mp = 100;
    public Text hpText = null;
    public Text mpText = null;
    public Image img_Skill;
    public Image mpBar;
    public Text skillText;

    [SerializeField]
    private Animator Ani;
    [Header("게임오버시 씬전환")]
    public GameObject GameOverPanel;
    [SerializeField]
    private float FadeTime = 1.2f; //Fade효과 재생시간
    [SerializeField]
    private float time;
    private Image fadeImg;
   
    public Collider swordCollider;
    private TrailRenderer swordRenderer;
    [SerializeField]
    private AudioClip AttackSound;
    //플레이어가 공격하는 모션 제어
    private GameObject bladeEffactPrefab;
    
    private Rigidbody rbody = null;
    //싱글톤
    public static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
                if (_instance == null)
                {
                    Debug.LogError("There's no active PlayerManager object");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        hpBar= GameObject.Find("HpBar").GetComponent<Image>();
        hpText = hpBar.GetComponentInChildren<Text>();
        Ani = GetComponent<Animator>();
        fadeImg = GameOverPanel.GetComponent<Image>();
        bladeEffactPrefab = Resources.Load<GameObject>("Effect/BladeStorm");
        AttackSound = Resources.Load<AudioClip>("Sounds/sword_throw");
        rbody = GetComponent<Rigidbody>();
        swordRenderer = swordCollider.gameObject.GetComponent<TrailRenderer>();
        swordCollider.enabled = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("MonsterSword"))
        {
            hp -= 7;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
            //Ani.SetTrigger("IsHit");
        }

        if (other.name.Equals("Body"))
        {
            if (other.gameObject.GetComponentInParent<SlimeCtrl>().thisState == SlimeCtrl.SlimeState.attack)
            {
            hp -= 3;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
                //Ani.SetTrigger("IsHit");
            }
        }
        if (hp <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            Ani.SetTrigger("IsDie");
            StartCoroutine(IsDie());
        }
    }

    //죽는 처리
    IEnumerator  IsDie()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        GameOverPanel.SetActive(true);
        Color fadeColor = fadeImg.color;
        time = 0f;

        while (fadeColor.a < 1f)
        {
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, time);
            fadeImg.color = fadeColor;
            yield return null;
        }
        SceneManager.LoadScene("GameStart");
        yield return null;
    }

    /*void InventoryOnOff()
    {
        if (inventory.activeInHierarchy)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }*/

    public void Attack()
    {
        //if (Input.GetMouseButtonDown(0) && check)
        //if (Input.GetKeyDown(KeyCode.A) && check)

        StartCoroutine(WaitForAttack(0.16f));
        swordRenderer.enabled = true;
        swordCollider.enabled = true;
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
        if (img_Skill.fillAmount >= 1.0f && mp >= 20)
        {
            Ani.SetTrigger("IsSkill");
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
