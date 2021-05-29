using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라가야 할 대상을 연결할 변수
    public Transform targetTr;
    // Main Camera 자신의 Transform 컴포넌트
    private Transform camTr;

    // 따라갈 대상으로부터 떨어질 거리
    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    // Y축으로 이동할 높이
    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    void Start()
    {
        // Main Camera 자신의 Transform 컴포넌트를 추출
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 추적해야 할 대상의 뒤쪽으로 distance만큼 이동
        // 높이를 height만큼 이동
        camTr.position = targetTr.position
                         + (-targetTr.forward * distance)
                         + (Vector3.up * height);

        // Camera를 피벗 좌표를 향해 회전
        camTr.LookAt(targetTr.position);
    }
}