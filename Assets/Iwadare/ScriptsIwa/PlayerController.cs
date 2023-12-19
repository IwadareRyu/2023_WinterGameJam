using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _boarSpeed = 5f;
    [SerializeField] float _dreamSpeed = 4f;
    SpeedState _speedState = SpeedState.Normal;


    Rigidbody2D _rb;
    float _x, _y;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunStateScripts.StunState == StunState.Normal)
        {

            if (_speedState == SpeedState.Normal)
            {
                //移動
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");

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
        if (_stunStateScripts.StunState == StunState.Normal)
        {
            //移動処理
            var horizontal = Vector2.right * _x;
            var vertical = Vector2.up * _y;
            if (_speedState == SpeedState.Normal)
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
        _x = transform.forward.x;
        _y = transform.forward.y;
    }

    enum SpeedState
    {
        Normal,
        Boar,
    }
}
