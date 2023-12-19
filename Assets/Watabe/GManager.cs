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

    // �V���O���g��
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

    // �V�[���̃��[�h
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //-----------------//


    //------Jewel------//

    // �W���G���̒ǉ�
    public void AddJewelCount()
    {
        jewelCount++;
    }

    // �W���G�����̎擾
    public int GetJewelCount()
    {
        return jewelCount;
    }

    //-----------------//


    //------Time------//

    // �^�C�}�[�̍X�V
    void Update()
    {
        gameTimer += Time.deltaTime;
    }

    // �Q�[���^�C�}�[�̎擾
    public float GetGameTimer()
    {
        return gameTimer;
    }

    //-----------------//


    //-------BGM-------//

    // BGM�̍Đ�
    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }
    }

    // BGM�̒�~
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    // BGM�̉��ʂ̐ݒ�
    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }
    }

    // BGM�̉��ʂ̎擾
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