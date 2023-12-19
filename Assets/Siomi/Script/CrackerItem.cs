using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerItem : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̎g�p�C���^�[�o��")] float _interval;
    float _timer;
    /// <summary> UI�p�̃A�C�e���C���^�[�o���̊��� </summary>
    public float _uiPercent;
    [SerializeField, Header("�N���b�J�[�̃v���n�u")] GameObject _crackerPrefab;
    [SerializeField, Header("���˓_")] GameObject _centerShotPos;
    [SerializeField, Header("���̂����蔻��")] float _boxHorizontal = 5f;
    [SerializeField, Header("�c�̂����蔻��")] float _boxVertical = 5f;
    [SerializeField, Header("�N���b�J�[���C���X�^���X����ʒu")] Transform _crackerPos;
    private void Start()
    {
        _timer = _interval;
    }

    void Update()
    {
        //�C���^�[�o�������^�C�}�[���������ꍇ�ɏo���B
        if (!(_timer >= _interval))
            _timer += Time.deltaTime;
        //UI�̃X���C�_�[�ɕ\������
        UiKousin();
    }

    void UiKousin()
    {
        _uiPercent = _timer / _interval;
    }
    public void Action()
    {
        if (_timer >= _interval)
        {
            //�N���b�J�[�̏���������
            // �w��͈͂̃R���C�_�[��S�Ď擾����
            var cols = Physics2D.OverlapBoxAll(_centerShotPos.transform.position, new Vector2(_boxHorizontal, _boxVertical), _centerShotPos.transform.rotation.z);

            //�v���C���[�ƃG�l�~�[��T��
            foreach (var c in cols)
            {
                if (c.TryGetComponent<StunStateScripts>(out var stunState))
                {
                    //�X�^��������
                    stunState.ChangeStunState();
                }
            }
            //�N���b�J�[���C���X�^���X����
            Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
            _timer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_centerShotPos.transform.position, new Vector3(_boxHorizontal, _boxVertical, 0));
    }
}
