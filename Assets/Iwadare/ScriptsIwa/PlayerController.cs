using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _boarSpeed = 5f;
    [SerializeField] float _dreamSpeed = 4f;
    SpeedState _speedState = SpeedState.SpeedNormal;


    Rigidbody2D _rb;
    float _x, _y;
    float _tmpx, _tmpy;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tmpy = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunStateScripts.StunState == StunState.Normal)
        {

            if (_speedState == SpeedState.SpeedNormal)
            {
                //移動
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");
                
                if(_x != 0 || _y != 0)
                {
                    _tmpx = _x;
                    _tmpy = _y;
                }

                if (Input.GetButtonDown("Fire1"))
                {

                }// 時限爆弾

                if (Input.GetButtonDown("Fire2"))
                {

                }// クラッカー

                if (Input.GetButtonDown("Fire3"))
                {
                    BoarSpeedUp();
                }// イノシシスピアップ

                if (Input.GetButtonDown("Skill"))
                {
                    DreamStateScriopts.DreamWorld.Invoke();
                }// 夢世界

            }
        }
    }

    private void FixedUpdate()
    {
        if (_stunStateScripts.StunState != StunState.Stun)
        {
            //移動処理
            var horizontal = Vector2.right * _x;
            var vertical = Vector2.up * _y;
            if (_speedState == SpeedState.SpeedNormal)
            {
                _rb.velocity = horizontal.normalized * _speed + vertical.normalized * _speed;
            }
            else
            {
                _rb.velocity = horizontal.normalized * _boarSpeed + vertical.normalized * _boarSpeed;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    void BoarSpeedUp()
    {
        _speedState = SpeedState.Boar;
        _x = _tmpx;
        _y = _tmpy;
        Debug.Log("Boar");
    }

    void Stun()
    {
        _stunStateScripts.ChangeStunState();
    }

    enum SpeedState
    {
        SpeedNormal,
        Boar,
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_speedState == SpeedState.Boar)
        {
            Stun();
            if (collision.gameObject.TryGetComponent<StunStateScripts>(out var stun))
            {
                stun.ChangeStunState();
            }
            _speedState = SpeedState.SpeedNormal;
            Debug.Log("SpeedNormal");
        }
    }
}
