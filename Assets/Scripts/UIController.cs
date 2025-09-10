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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeCnt = GetComponent<TimeController>(); 

        buttonPanel.SetActive(false); //存在を非表示

        //時間差でメソッドを発動する
        Invoke("InactiveImage", 1.0f);
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
        }

        else if (GameManager.gameState == "gameover")
        {
            buttonPanel.SetActive(true); //ボタンパネルの復活
            mainImage.SetActive(true); //メイン画像の復活
            //メイン画像オブジェクトのImageコンポーネントが所持している変数Spriteに"ゲームオーバー"の絵を代入
            mainImage.GetComponent<Image>().sprite = gameOverSprite;
            //ネクストボタンオブジェクトのbuttonコンポーネントが所持している変数interavtableを無効（ボタン機能を無効）
            nextButton.GetComponent<Button>().interactable = false;
        }

        else if (GameManager.gameState == "playing")
        {
            //一旦displayTimeの数字を変数timesに渡す
            float times = timeCnt.displayTime;
            timeText.GetComponent<TextMeshProUGUI>().text = Mathf.Ceil(times).ToString();
        }
    }

    //メイン画像を非表示にするためのだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
