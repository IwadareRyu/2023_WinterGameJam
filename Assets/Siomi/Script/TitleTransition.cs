using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    //先頭のシーンに遷移する
    public void TitleScene()
    {
        GManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}
