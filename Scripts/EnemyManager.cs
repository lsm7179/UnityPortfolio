using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private GameObject skeleton;

    public float createTime = 3.0f;
    public bool isGameOver = false;
    public int MaxCount = 3;
    public List<GameObject> skeletonPool = new List<GameObject>();

    //싱글톤 구현
    public static EnemyManager _instance = null;
    public static EnemyManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(EnemyManager)) as EnemyManager;
                if(_instance == null)
                {
                    Debug.LogError("There's no active EnemyManager object");
                }
            }
            return _instance;
        }
    }

	void Start () {
        //하이라키 상에 SpawnPoint 오브젝트 명을 찾은 다음 하위 오브젝트의 트랜스폼 컴퍼넌트를 배열에 대입
        points = GameObject.Find("SpawnGroup").GetComponentsInChildren<Transform>();
        skeleton = Resources.Load<GameObject>("Prefabs/Skeleton");
        if (points.Length > 0)
        {
            StartCoroutine(CreateSkeleton());
        }
        for(int i = 0; i < MaxCount; i++)
        {
            GameObject skeleton_ = (GameObject)Instantiate(skeleton);
            skeleton.name = "Skeleton_" + (i + 1).ToString();
            skeleton_.SetActive(false);
            skeletonPool.Add(skeleton_);
        }
	}
	
    IEnumerator CreateSkeleton()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(createTime);
            if (isGameOver) yield break;
            foreach(GameObject _skel in skeletonPool)
            {
                if (!_skel.activeSelf)
                {
                    int idx = Random.Range(1, points.Length);
                    _skel.transform.position = points[idx].position;
                    _skel.SetActive(true);
                    //Debug.Log(_skel.name.ToString() + " " + _skel.active.ToString());
                    break;
                }
            }
        }
    }

}
