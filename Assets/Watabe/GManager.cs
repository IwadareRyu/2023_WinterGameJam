using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    //-------Var-------//

    private int jewelCount = 0;
    private float gameTimer = 0.0f;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip bgm;

    //-----------------//


    //-----Single-----//

    // シングルトン
    public static GManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //-----------------//


    //------Scene------//

    // シーンのロード
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //-----------------//


    //------Jewel------//

    // ジュエルの追加
    public void AddJewelCount()
    {
        jewelCount++;
    }

    // ジュエル数の取得
    public int GetJewelCount()
    {
        return jewelCount;
    }

    //-----------------//


    //------Time------//

    // タイマーの更新
    void Update()
    {
        gameTimer += Time.deltaTime;
    }

    // ゲームタイマーの取得
    public float GetGameTimer()
    {
        return gameTimer;
    }

    //-----------------//


    //-------BGM-------//

    // BGMの再生
    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }
    }

    // BGMの停止
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    // BGMの音量の設定
    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }
    }

    // BGMの音量の取得
    public float GetBGMVolume()
    {
        if (bgmSource != null)
        {
            return bgmSource.volume;
        }
        else
        {
            return 0;
        }
    }

    //-----------------//
}