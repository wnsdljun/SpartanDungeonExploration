using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    //ui에서 상태 나타나는 부분을 관리.
    public Condition health;
    public Condition hunger;
    public Condition stamina;

    private void Start()
    {
        //CharacterManager.Instance.Player.conditions.uICondition = this;
    }
}
