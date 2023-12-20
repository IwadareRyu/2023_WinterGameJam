using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _boarSpeed = 5f;
    [SerializeField] float _dreamSpeed = 4f;
    [SerializeField] Transform _rotaObj;
    [SerializeField] Transform _cracerInsPos;
    [SerializeField] Cracker _crakerPrefab;
    [SerializeField] CrackerItem _crakerItem;
    Animator _anim;
    bool _isWalk;
    bool _bearBool;
    bool _actionBool;
    Rigidbody2D _rb;
    float _x, _y;
    float _tmpx, _tmpy;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _tmpy = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunStateScripts.StunState == StunState.Normal)
        {

            if (!_bearBool && !_actionBool)
            {
                //移動
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");

                if (_x != 0 || _y != 0)
                {
                    _tmpx = _x;
                    _tmpy = _y;
                    _isWalk = true;
                }
                else
                {
                    _isWalk = false;
                }

                if (Input.GetButtonDown("Fire1"))
                {

                }// 時限爆弾

                if (Input.GetButtonDown("Fire2"))
                {
                    StunGun();
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
            if (!_bearBool && DreamStateScripts.DreamState == DreamState.Normal)
            {
                _rb.velocity = horizontal.normalized * _speed + vertical.normalized * _speed;
            }
            else if (_bearBool)
            {
                _rb.velocity = horizontal.normalized * _boarSpeed + vertical.normalized * _boarSpeed;
            }
            else
            {
                _rb.velocity = horizontal.normalized * _dreamSpeed + vertical.normalized * _dreamSpeed;
            }
            DirectionTarget();
            _anim.SetFloat("x",_tmpx);
            _anim.SetFloat("y",_tmpy);
            _anim.SetBool("walk",_isWalk);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    void DirectionTarget()
    {
        var rota = _rotaObj.eulerAngles;
        if (_tmpx != 0)
        {
            if (_tmpx > 0)
            {
                rota.z = 0f;
            }
            else
            {
                rota.z = 180f;
            }
        }
        else
        {
            if (_tmpy > 0)
            {
                rota.z = 90f;
            }
            else
            {
                rota.z = 270f;
            }
        }
        _rotaObj.eulerAngles = rota;
    }

    /// <summary>イノシシモードになるときに呼ばれるメソッド</summary>
    void BoarSpeedUp()
    {
        _bearBool = true;
        _x = _tmpx;
        _y = _tmpy;
        Debug.Log("Boar");
    }

    void StunGun()
    {
        StartCoroutine(_crakerItem.Action());
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
        if (_bearBool)
        {
            Stun();
            if (collision.gameObject.TryGetComponent<StunStateScripts>(out var stun))
            {
                stun.ChangeStunState();
            }
            _bearBool = false;
            Debug.Log("SpeedNormal");
        }
    }

}
