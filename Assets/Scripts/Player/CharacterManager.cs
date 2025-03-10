using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //캐릭터 매니저.
    //싱글톤으로, 다른곳에서 플레이어에 접근할 수 있게 도와줌.

    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            //내가 이 세상에 존재하지 않는다면 나를 만들어.
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player
    {
        get => _player;
        set => _player = value;
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
