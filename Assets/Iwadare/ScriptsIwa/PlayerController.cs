using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed;
    [SerializeField] float _dreamSpeed;
    Rigidbody2D _rb;
    float _x, _y;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");

        
        if(Input.GetButtonDown("Fire1"))
        {

        }// �������e

        if(Input.GetButtonDown("Fire2"))
        {

        }// �N���b�J�[
        
        if(Input.GetButtonDown("Fire3"))
        {

        }// �C�m�V�V�X�s�A�b�v

        if(Input.GetButtonDown("Skill"))
        {
            DreamStateScriopts.DreamWorld.Invoke();
        }// �����E
    }

    private void FixedUpdate()
    {
        //�ړ�����
        var horizontal = Vector2.right * _x;
        var vertical = Vector2.up * _y;
        _rb.velocity = horizontal.normalized * _speed + vertical.normalized * _speed;
    }
}
