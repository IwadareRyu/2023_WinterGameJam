using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Jewel : MonoBehaviour
{
    [SerializeField] bool IsDream = false;
    [SerializeField] bool IsFound = false;
    
    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += DreamChange;
        DreamStateScripts.DreamWorldEnd += DreamChange;

        this.gameObject.SetActive(false);
    }

    /// <summary> ���ƌ�����؂�ւ����Ƃ��̏��� </summary>
    public void DreamChange()
    {
        IsDream = !IsDream;
        if (IsDream || IsFound)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    /// <summary> �W���G���ɂ��������Ƃ��̏��� </summary>
    public void JewelFound()
    {
        if(IsDream)
        {
            IsFound = true;
        }    
        else
        {
            //�Q�[���}�l�[�W���[�̃W���G���X�R�A���X�V���ăf�X�g���C����
            Debug.Log("�W���G���������");
            Destroy(this.gameObject);
        }

    }
}
