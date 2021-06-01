using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    // 델리게이트 선언
    delegate float SumHandler(float a, float b);

    // 델리게이트 타입의 변수 선언
    SumHandler sumHandler;

    // 덧셈 연산을 하는 함수
    float Sum(float a, float b)
    {
        return a + b;
    }

    void Start()
    {
        // 델리게이트 변수에 함수(메서드) 연결(할당)
        sumHandler = Sum;

        // 델리게이트 실행
        float sum = sumHandler(10.0f, 5.0f);

        // 결괏값 출력
        Debug.Log($"Sum = {sum}");
    }
}