using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어에 붙은 다른 컴포넌트들을 관리. 컨트롤러, 상태 등등

    public PlayerController controller;
    public PlayerConditions conditions;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        //캐릭터 매니저에서 관리할 플레이어. 나를 등록해줌.

        CharacterManager.Instance.Player = this;
    }

}
