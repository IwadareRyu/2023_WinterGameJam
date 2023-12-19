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
                //�ړ�
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");

                if (_x != 0 || _y != 0)
                {
                    _tmpx = _x;
                    _tmpy = _y;
                }

                if (Input.GetButtonDown("Fire1"))
                {

                }// �������e

                if (Input.GetButtonDown("Fire2"))
                {

                }// �N���b�J�[

                if (Input.GetButtonDown("Fire3"))
                {
                    BoarSpeedUp();
                }// �C�m�V�V�X�s�A�b�v

                if (Input.GetButtonDown("Skill"))
                {
                    DreamStateScripts.DreamWorld.Invoke();
                }// �����E

            }
        }
    }

    private void FixedUpdate()
    {
        if (_stunStateScripts.StunState != StunState.Stun)
        {
            //�ړ�����
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

    /// <summary>�C�m�V�V���[�h�ɂȂ�Ƃ��ɌĂ΂�郁�\�b�h</summary>
    void BoarSpeedUp()
    {
        _speedState = BoarState.Boar;
        _x = _tmpx;
        _y = _tmpy;
        Debug.Log("Boar");
    }

    /// <summary>Stun����Ƃ��ɌĂяo����郁�\�b�h</summary>
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
                Debug.Log("�Ђ��肠��I");
            }
            else
            {
                Debug.Log("Get�����I");
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

    /// <summary>�C�m�V�V���[�h���ۂ�</summary>
    enum BoarState
    {
        Human,
        Boar,
    }

}
