using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject mainImage; //アナウンスをする画像
    public GameObject buttonPanel; //ボタンをグループ化しているパネル

    public GameObject retryButton; //リトライボタン
    public GameObject nextButton; //ネクストボタン

    public Sprite gameClearSprite; //ゲームクリアの絵


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    //メイン画像を非表示にするためのだけのメソッド
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
