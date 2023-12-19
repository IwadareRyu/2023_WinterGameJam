using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStateScripts : MonoBehaviour
{
    StunState _stunState;
    StunState StunState => _stunState;
    [SerializeField] float _stunTime = 5f;
    float _time;

    // Start is called before the first frame update
    private void Awake()
    {
        _stunState = StunState.Normal;
    }

    private void Update()
    {
        if (_stunState == StunState.Stun)
        {
            _time += Time.deltaTime;
        }

        if (_time == _stunTime)
        {
            _stunState = StunState.Normal;
        }
    }

    /// <summary>Stunó‘ÔØ‚è‘Ö‚¦</summary>
    public void ChangeStunState()
    {
        _stunState = StunState.Stun;
    }

}

public enum StunState
{
    Normal,
    Stun
}
