using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour {

    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private GameObject slime;

    public float createTime = 3.0f;
    public bool isGameOver = false;
    public int MaxCount = 5;
    public List<GameObject> slimePool = new List<GameObject>();

    //싱글톤
    public static SlimeManager _instance = null;
    public static SlimeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(SlimeManager)) as SlimeManager;
                if(_instance == null)
                {
                    Debug.LogError("There's no active SlimeManager object");
                }
            }
            return _instance;
        }
    }

	void Start () {
        //하이라키 상에 SpawnPoint 오브젝트 명을 찾은 다음 하위 오브젝트의 트랜스폼 컴퍼넌트를 배열에 대입
        points = GameObject.Find("SlimSpawn").GetComponentsInChildren<Transform>();
        slime = Resources.Load<GameObject>("Prefabs/slime");
        for(int i = 0; i < MaxCount; i++)
        {
            GameObject slime_ = (GameObject)Instantiate(slime);
            slime_.name = "slime_" + (i + 1).ToString();
            slime_.SetActive(false);
            slimePool.Add(slime_);
        }
	}

    public IEnumerator CreateSlimeOnce()
    {
        yield return new WaitForSeconds(0.5f);
        int idx = 1;
        foreach (GameObject _slime in slimePool)
        {
            if (!_slime.activeSelf)
            {
                _slime.transform.position = points[idx].position;
                _slime.SetActive(true);
                idx++;
            }
        }
    }

    IEnumerator CreateSlime()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(createTime);
            if (isGameOver) yield break;
            foreach(GameObject _slime in slimePool)
            {
                if (!_slime.activeSelf)
                {
                    int idx = Random.Range(1, points.Length-1);
                    _slime.transform.position = points[idx].position;
                    _slime.SetActive(true);
                    break;
                }
            }
        }
    }

}
