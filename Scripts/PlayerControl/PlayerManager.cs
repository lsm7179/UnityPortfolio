using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    private Image hpBar;
    private int hpInit = 300;
    private int hp = 300;
    private Text hpText = null;
    public GameObject inventory = null;
    public GameObject GameOverPanel;
    [SerializeField]
    private Animator Ani;
    [Header("게임오버시 씬전환")]
    [SerializeField]
    private float FadeTime = 1.2f; //Fade효과 재생시간
    [SerializeField]
    private float time;
    private Image fadeImg;
    void Awake()
    {
        hpBar= GameObject.Find("HpBar").GetComponent<Image>();
        hpText = hpBar.GetComponentInChildren<Text>();
        Ani = GetComponent<Animator>();
        fadeImg = GameOverPanel.GetComponent<Image>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("MonsterSword"))
        {
            hp -= 15;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
            Ani.SetTrigger("IsHit");
        }

        if (other.name.Equals("Body"))
        {
            if (other.gameObject.GetComponentInParent<SlimeCtrl>().thisState == SlimeCtrl.SlimeState.attack)
            {
            hp -= 5;
            hpBar.fillAmount = (float)hp / (float)hpInit;
            hpText.text = hp + " / " + hpInit;
                Ani.SetTrigger("IsHit");
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
        GameOverPanel.SetActive(true);
        StartCoroutine(GameOver());

    }
    IEnumerator GameOver()
    {
        
        Color fadeColor = fadeImg.color;
        time = 0f;

        while (fadeColor.a < 1f)
        {
            Debug.Log(fadeColor.a);
            time += Time.deltaTime / FadeTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, time);
            fadeImg.color = fadeColor;
            yield return null;
        }
        SceneManager.LoadScene("GameStart");
        yield return null;
    }
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryOnOff();
        }
    }*/

    void InventoryOnOff()
    {
        if (inventory.activeInHierarchy)
        {
            inventory.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
        }
    }
}
