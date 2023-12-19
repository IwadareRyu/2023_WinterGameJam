using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    //-------Var-------//

    // 初期タイマー（秒）
    [SerializeField] private float initialTimer = 300.0f;
    // タイマーを表示するテキスト
    [SerializeField] private Text timerText;
    // 1秒あたりのスコア
    [SerializeField] private float TimeScore = 100.0f;

    // ジュエル1つあたりのスコア
    [SerializeField] private int jewelScore = 10000;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip bgm;

    [SerializeField] private string titleSceneName;
    [SerializeField] private string mainSceneName;
    [SerializeField] private string gameOverSceneName;

    private int jewelCount = 0;
    private float gameTimer;
    private int score = 0;

    //-----Single-----//

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

    private void Start()
    {
        ResetTimer();
    }

    // タイマーの更新
    void Update()
    {
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = "Time: " + gameTimer.ToString("F2");

            if (gameTimer <= 0)
            {
                CalculateScore(); // スコアの計算
                SceneManager.LoadScene(gameOverSceneName);
            }
        }
    }

    //------Scene------//

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //------Jewel------//

    public void AddJewelCount()
    {
        jewelCount++;
    }

    public int GetJewelCount()
    {
        return jewelCount;
    }

    //------Score-----//

    // スコアの計算
    public void CalculateScore()
    {
        score = jewelCount * jewelScore - (int)(gameTimer * TimeScore);

        if (score <= 0)
        {
            score = 0;
        }
    }

    // スコアの取得
    public int GetScore()
    {
        return score;
    }

    //------Time------//

    public void ResetTimer()
    {
        gameTimer = initialTimer;
    }

    public float GetGameTimer()
    {
        return gameTimer;
    }

    //-------BGM-------//

    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
            bgmSource.loop = true;
        }
    }

    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }
    }

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
}
