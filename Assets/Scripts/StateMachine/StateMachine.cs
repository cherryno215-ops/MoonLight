using UnityEngine;

public class StateMachine // �޼��� ������ ���ְ� �ܵ����� ����� ���� ����Ʈ Ŭ����
{
    public EntityState currentState { get; private set; } //EntitiyState���� ������ currentState(���� ����) ����
    public bool canChangeState = true;


    public void Initialize(EntityState startState) // ���¸ӽ� ������ ���� + �� ���� ���� �غ�
    {
        canChangeState = true;
        currentState = startState; // � ���·� �������� ���� (��ŸƮ ����)
        currentState.Enter(); // ���� �ൿ�� ����
    }

    public void ChangeState(EntityState newState) // ���ο� ����
    {


        if (canChangeState == false)
        {
            return;
        }

        
        currentState.Exit(); // ���� ���¿��� Ż��
        currentState = newState; // ���ο� ����
        currentState.Enter(); // ����� �ش� ���� '����'

    }

    public void UpdateActiveState() // ���� �ݺ�
    {   
        currentState.Update(); // ���� �ݺ�
    }


    public void SwitchOffstateMachine() => canChangeState = false;
}