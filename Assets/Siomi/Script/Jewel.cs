using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Jewel : MonoBehaviour
{
    [SerializeField] bool IsDream = false;
    [SerializeField] bool IsFound = false;
    [SerializeField, Header("オーディオソースの設定")] AudioSource _audioSource;
    [SerializeField, Header("出したい音")] AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += DreamChange;
        DreamStateScripts.DreamWorldEnd += DreamChange;

        this.gameObject.SetActive(false);
    }

    /// <summary> 夢と現実を切り替えたときの処理 </summary>
    public void DreamChange()
    {
        IsDream = !IsDream;
        if (IsDream || IsFound)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    /// <summary> ジュエルにあたったときの処理 </summary>
    public void JewelFound()
    {
        if(IsDream)
        {
            IsFound = true;
        }    
        else
        {
            //ゲームマネージャーのジュエルスコアを更新してデストロイする
            GManager.Instance.AddJewelCount();
            //音を出す
            _audioSource.PlayOneShot(_audioClip);
            Destroy(this.gameObject);
        }

    }
}
