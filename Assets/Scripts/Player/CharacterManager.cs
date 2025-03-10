using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //ĳ���� �Ŵ���.
    //�̱�������, �ٸ������� �÷��̾ ������ �� �ְ� ������.

    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            //���� �� ���� �������� �ʴ´ٸ� ���� �����.
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
