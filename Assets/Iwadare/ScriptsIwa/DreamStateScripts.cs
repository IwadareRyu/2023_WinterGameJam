using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DreamStateScripts : MonoBehaviour
{
    static DreamState _dreamState;
    public static DreamState DreamState => _dreamState;
    [SerializeField] float _dreamTime = 5f;
    float _time;
    [Tooltip("���̎n�܂�")]
    public static UnityAction DreamWorld;
    [Tooltip("���̏I���")]
    public static UnityAction DreamWorldEnd;
    // Start is called before the first frame update
    void Awake()
    {
        _dreamState = DreamState.Normal;
    }

    private void OnEnable()
    {
        DreamWorld += ChangeDream;
        DreamWorldEnd += ChangeDreamNormal;
    }

    private void OnDisable()
    {
        DreamWorld -= ChangeDream;
        DreamWorldEnd -= ChangeDreamNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (_dreamState == DreamState.Dream)
        {
            _time += Time.deltaTime;
        }

        if (_time > _dreamTime)
        {
            _time = 0f;
            DreamWorldEnd.Invoke();
        }
    }


    void ChangeDream()
    {
        Debug.Log("Welcome to Dream�I");
        _dreamState = DreamState.Dream;
    }

    void ChangeDreamNormal()
    {
        Debug.Log("���̎��Ԃ͏I���...");
        _dreamState = DreamState.Normal;
    }
}

public enum DreamState
{
    Normal,
    Dream
}
