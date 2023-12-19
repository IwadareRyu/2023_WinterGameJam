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
            Debug.Log("ジュエルを取った");
            Destroy(this.gameObject);
        }

    }
}
