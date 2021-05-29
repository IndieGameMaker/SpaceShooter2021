using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    // 총알 프리팹
    public GameObject bullet;
    // 총알 발사 좌표
    public Transform firePos;

    void Start()
    {

    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Bullet 프리팹을 동적으로 생성(생성할 객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
