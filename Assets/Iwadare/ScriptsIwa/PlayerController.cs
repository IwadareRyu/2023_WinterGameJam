using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _boarSpeed = 5f;
    [SerializeField] float _dreamSpeed = 4f;
    BoarState _speedState = BoarState.Human;


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

            if (_speedState == BoarState.Human)
            {
                //移動
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");

                if (_x != 0 || _y != 0)
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
                    DreamStateScripts.DreamWorld.Invoke();
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
            if (_speedState == BoarState.Human && DreamStateScripts.DreamState == DreamState.Normal)
            {
                _rb.velocity = horizontal.normalized * _speed + vertical.normalized * _speed;
            }
            else if (_speedState == BoarState.Boar)
            {
                _rb.velocity = horizontal.normalized * _boarSpeed + vertical.normalized * _boarSpeed;
            }
            else
            {
                _rb.velocity = horizontal.normalized * _dreamSpeed + vertical.normalized * _dreamSpeed;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>イノシシモードになるときに呼ばれるメソッド</summary>
    void BoarSpeedUp()
    {
        _speedState = BoarState.Boar;
        _x = _tmpx;
        _y = _tmpy;
        Debug.Log("Boar");
    }

    /// <summary>Stunするときに呼び出されるメソッド</summary>
    void Stun()
    {
        _stunStateScripts.ChangeStunState();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            if (DreamStateScripts.DreamState == DreamState.Dream)
            {
                Debug.Log("ひかりあれ！");
            }
            else
            {
                Debug.Log("Getだぜ！");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_speedState == BoarState.Boar)
        {
            Stun();
            if (collision.gameObject.TryGetComponent<StunStateScripts>(out var stun))
            {
                stun.ChangeStunState();
            }
            _speedState = BoarState.Human;
            Debug.Log("SpeedNormal");
        }
    }

    /// <summary>イノシシモードか否か</summary>
    enum BoarState
    {
        Human,
        Boar,
    }

}
