using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamStateScriopts : MonoBehaviour
{
    DreamState _dreamState;
    public DreamState DreamState => _dreamState;
    [SerializeField] float _stunTime = 5f;
    float _time;
    // Start is called before the first frame update
    void Awake()
    {
        _dreamState = DreamState.Normal;
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
}

public enum DreamState
{
    Normal,
    Dream
}
