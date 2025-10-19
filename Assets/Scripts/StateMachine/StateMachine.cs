using UnityEngine;

public class StateMachine // 메서드 공유만 해주고 단독적인 기능은 없는 서포트 클래스
{
    public EntityState currentState { get; private set; } //EntitiyState에서 가져온 currentState(현재 상태) 변수
    public bool canChangeState = true;


    public void Initialize(EntityState startState) // 상태머신 시작점 세팅 + 그 상태 실행 준비
    {
        canChangeState = true;
        currentState = startState; // 어떤 상태로 시작할지 정함 (스타트 상태)
        currentState.Enter(); // 정한 행동을 시작
    }

    public void ChangeState(EntityState newState) // 새로운 상태
    {


        if (canChangeState == false)
        {
            return;
        }

        
        currentState.Exit(); // 전의 상태에서 탈출
        currentState = newState; // 새로운 상태
        currentState.Enter(); // 등록한 해당 상태 '시작'

    }

    public void UpdateActiveState() // 상태 반복
    {   
        currentState.Update(); // 상태 반복
    }


    public void SwitchOffstateMachine() => canChangeState = false;
}