using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject mainImage; //アナウンスをする画像
    public GameObject buttonPanel; //ボタンをグループ化しているパネル

    public GameObject retryButton; //リトライボタン
    public GameObject nextButton; //ネクストボタン

    public Sprite gameClearSprite; //ゲームクリアの絵
    public Sprite gameOverSprite; //ゲームオーバーの絵

    TimeController timeCnt; //TimeController.csを扱うための変数。表でいじる必要がないので、publicにはしないで、下記のメソッドでのみいじればOK
    public GameObject timeText; //ヒエラルキーTimeBarの中に入っているTimeTextを扱うための変数

    public GameObject scoreText; //スコアテキスト

    AudioSource audio;
    SoundController soundController; //自作したスクリプト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeCnt = GetComponent<TimeController>();

        buttonPanel.SetActive(false); //存在を非表示

        //時間差でメソッドを発動する
        Invoke("InactiveImage", 1.0f);

        UpdateScore();                //トータルスコアが出るように更新

        //AudioSource,SoundControllerの取得
        audio = GetComponent<AudioSource>();
        soundController = GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == "gameclear")
        {
            buttonPanel.SetActive(true); //ボタンパネルの復活
            mainImage.SetActive(true); //メイン画像の復活
            //メイン画像オブジェクトのImageコンポーネントが所持している変数Spriteに"ステージクリア"の絵を代入
            mainImage.GetComponent<Image>().sprite = gameClearSprite;
            //リトライボたんオブジェクトのbuttonコンポーネントが所持している変数interavtableを無効（ボタン機能を無効）
            retryButton.GetComponent<Button>().interactable = false;

            //ステージクリアによってステージスコアが確定したので
            //トータルスコアに加算
            GameManager.totalScore += GameManager.stageScore;
            GameManager.stageScore = 0;      //次に備えてステージスコアは０にリセットする

            timeCnt.isTimeOver = true;//TimeCOntrollerにてカウントしているタイムのカウントを停止
            //一旦displayTimeの数字を変数timesに渡す
            float times = timeCnt.displayTime;

            if (timeCnt.isCountDown)
            {
                //残時間をそのままタイムボーナスとしてトータルスコアに加算する
                GameManager.totalScore += (int)times * 10;
            }
            else //カウントアップの場合はトータルスコアに加算しない
            {
                float gameTime = timeCnt.gameTime;    //基準時間の取得
                GameManager.totalScore += (int)(gameTime - times) * 10; //基準時間ー経過時間
            }

            UpdateScore();  //UIに最終的な数字を反映

            //サウンドをストップ
            audio.Stop();
            //SoundCOntrollerの変数に指名したゲームクリアの音を選択して鳴らす
            audio.PlayOneShot(soundController.bgm_GameClear);

            //1回だけ加算すれば良いので、gameclearのフラグを早々に変化させる
            GameManager.gameState = "gameend";
        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //ボタンパネルの復活
            mainImage.SetActive(true); //メイン画像の復活
            //メイン画像オブジェクトのImageコンポーネントが所持している変数Spriteに"ゲームオーバー"の絵を代入
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            //ネクストボタンオブジェクトのbuttonコンポーネントが所持している変数interavtableを無効（ボタン機能を無効）
            nextButton.GetComponent<Button>().interactable = false;

            //カウントを止める
            timeCnt.isTimeOver = true;

             //サウンドをストップ
            audio.Stop();
            //SoundCOntrollerの変数に指名したゲームクリアの音を選択して鳴らす
            audio.PlayOneShot(soundController.bgm_GameOver);

            GameManager.gameState = "gameend";

        }

        else if (GameManager.gameState == "playing")
        {
            //一旦displayTimeの数字を変数timesに渡す
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();

            if (timeCnt.isCountDown)
            {
                if (timeCnt.displayTime <= 0)
                {
                    //プレイヤーを見つけてきて、そのPlayer ControllerコンポーネントのGameOverメソッドをやらせている
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().GameOver();
                    GameManager.gameState = "gameover";
                }
            }
            else
            {
                if (timeCnt.displayTime >= timeCnt.gameTime)
                {
                    GameManager.gameState = "gameover";
                }
            }

            //スコアをリアルタイムに更新する
            UpdateScore();
        }
    }

    //メイン画像を非表示にするためのだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
    
    //スコアボードを更新
    void UpdateScore()
    {
        int Score = GameManager.stageScore + GameManager.totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = Score.ToString();
    }
}
