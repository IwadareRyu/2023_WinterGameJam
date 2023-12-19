using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DreamStateScriopts : MonoBehaviour
{
    static DreamState _dreamState;
    public DreamState DreamState => _dreamState;
    [SerializeField] float _stunTime = 5f;
    float _time;
    public static UnityAction DreamWorld;
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

        if (_time == _stunTime)
        {
            _dreamState = DreamState.Normal;
        }
    }

    void ChangeDream()
    {
        _dreamState = DreamState.Dream;
    }

    void ChangeDreamNormal()
    {
        _dreamState = DreamState.Normal;
    }
}

public enum DreamState
{
    Normal,
    Dream
}
