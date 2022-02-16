using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    enum State//class interface enum 列挙定数
    {
        Ready,
        Play,
        GameOver
    }

    State state;//初期値は0番目　今回はReady
    int score;

    public AzarashiController azarashi;
    public GameObject blocks;
    public Text scoreText;
    public Text stateText;


    // Start is called before the first frame update
    void Start()
    {
        Ready();
    }

    void LateUpdate()//updateの後に実行
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetButtonDown("Fire1"))
                {
                    GameStart();
                }
                break;
            case State.Play:
                if (azarashi.IsDead())
                {
                    GameOver();
                }
                break;
            case State.GameOver:
                if (Input.GetButtonDown("Fire1"))
                {
                    Reload();
                }
                break;
        }    
    }

    void Ready()
    {
        state = State.Ready;

        azarashi.SetSteerActive(false);//isKinematicをtrueにして動けなくする
        blocks.SetActive(false);

        scoreText.text = "Score : " + 0;

        stateText.gameObject.SetActive(true);
        stateText.text = "Ready";
    }

    void GameStart()
    {
        state = State.Play;

        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        azarashi.Flap();

        stateText.gameObject.SetActive(false);
        stateText.text = "";
    }

    void GameOver()
    {
        state = State.GameOver;

        ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();//scrollobjectを全部見つけてくれる ヒエラルキーにあるのを自由に使える　処理が思い　FindObでヒエラルキーから探してくれる

        foreach (ScrollObject so in scrollObjects)
        {
            so.enabled = false;//拡張for文 soは変数名  inは:の変わり
        }

        stateText.gameObject.SetActive(true);
        stateText.text = "GameOver";
    }

    void Reload()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;//シーンの名前を取得
        SceneManager.LoadScene(currentSceneName);//シーンをリロードする シーンの遷移はSceneManagerが必要
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score;
    }
}
