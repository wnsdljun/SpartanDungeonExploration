using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    //ui���� ���� ��Ÿ���� �κ��� ����.
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    private void Start()
    {
        //CharacterManager.Instance.Player.conditions.uICondition = this;
    }
}
