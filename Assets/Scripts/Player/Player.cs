using UnityEngine;

public class Player : MonoBehaviour
{
    //�÷��̾ ���� �ٸ� ������Ʈ���� ����. ��Ʈ�ѷ�, ���� ���

    public PlayerController controller;
    public PlayerConditions conditions;

    private void Awake()
    {
        //ĳ���� �Ŵ������� ������ �÷��̾�. ���� �������.

        CharacterManager.Instance.Player = this;
    }

}
