using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�÷��̾ ���� �ٸ� ������Ʈ���� ����. ��Ʈ�ѷ�, ���� ���

    public PlayerController controller;
    public PlayerConditions conditions;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        //ĳ���� �Ŵ������� ������ �÷��̾�. ���� �������.

        CharacterManager.Instance.Player = this;
    }

}
