using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TextMesh Pro 관련 컴포넌트에 접근하기 위해 선언
using TMPro;

public class GameManager : MonoBehaviour
{
    // 몬스터가 출현할 위치를 저장할 List 타입 변수
    public List<Transform> points = new List<Transform>();

    // 몬스터를 미리 생성해 저장할 리스트 자료형
    public List<GameObject> monsterPool = new List<GameObject>();

    // 오브젝트 풀(Object Pool)에 생성할 몬스터의 최대 개수
    public int maxMonsters = 10;

    // 몬스터 프리팹을 연결할 변수
    public GameObject monster;

    // 몬스터의 생성 간격
    public float createTime = 3.0f;

    // 게임의 종료 여부를 저장할 멤버 변수
    private bool isGameOver;

    // 게임의 종료 여부를 저장할 프로퍼티
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    // 싱글턴 인스턴스 선언
    public static GameManager instance = null;

    // 스코어 텍스트를 연결할 변수
    public TMP_Text scoreText;
    // 누적 점수를 기록하기 위한 변수
    private int totScore = 0;

    // 스크립트가 실행되면 가장 먼저 호출되는 유니티 이벤트 함수
    void Awake()
    {
        // instance가 할당되지 않았을 경우
        if (instance == null)
        {
            instance = this;
        }
        // instance에 할당된 클래스의 인스턴스가 다를 경우 새로 생성된 클래스를 의미함
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        // 다른 씬으로 넘어가더라도 삭제하지 않고 유지함
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // 몬스터 오브젝트 풀 생성
        CreateMonsterPool();

        // SpawnPointGroup 게임오브젝트의 Transform 컴포넌트 추출
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // // SpawnPointGroup 하위에 있는 모든 차일드 게임오브젝트의 Transform 컴포넌트 추출
        // spawnPointGroup?.GetComponentsInChildren<Transform>(points);

        // SpawnPointGroup 하위에 있는 모든 차일드 게임오브젝트의 Transform 컴포넌트 추출
        foreach (Transform point in spawnPointGroup)
        {
            points.Add(point);
        }

        // 일정한 시간 간격으로 함수를 호출
        InvokeRepeating("CreateMonster", 2.0f, createTime);

        // 스코어 점수 출력
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DisplayScore(0);
    }

    void CreateMonster()
    {
        // 몬스터의 불규칙한 생성 위치 산출
        int idx = Random.Range(0, points.Count);

        // 몬스터 프리팹 생성
        //Instantiate(monster, points[idx].position, points[idx].rotation);

        // 오브젝트 풀에서 몬스터 추출
        GameObject _monster = GetMonsterInPool();
        // 추출한 몬스터의 위치와 회전을 설정
        _monster?.transform.SetPositionAndRotation(points[idx].position,
                                                   points[idx].rotation);

        // 추출한 몬스터를 활성화
        _monster?.SetActive(true);
    }

    // 오브젝트 풀에 몬스터 생성
    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            // 몬스터 생성
            var _monster = Instantiate<GameObject>(monster);
            // 몬스터의 이름을 지정
            _monster.name = $"Monster_{i:00}";
            // 몬스터 비활성화
            _monster.SetActive(false);

            // 생성한 몬스터를 오브젝트 풀에 추가
            monsterPool.Add(_monster);
        }
    }

    // 오브젝트 풀에서 사용 가능한 몬스터를 추출해 반환하는 함수
    public GameObject GetMonsterInPool()
    {
        // 오브젝트 풀의 처음부터 끝까지 순회
        foreach (var _monster in monsterPool)
        {
            // 비활성화 여부로 사용 가능한 몬스터를 판단
            if (_monster.activeSelf == false)
            {
                // 몬스터 반환
                return _monster;
            }
        }
        return null;
    }

    // 점수를 누적하고 출력하는 함수
    public void DisplayScore(int score)
    {
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE :</color> <color=#ff0000>{totScore:#,##0}</color>";
        // 스코어 저장
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }
}
