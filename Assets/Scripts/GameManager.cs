using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string gameState; //静的メンバ

    public static int totalScore; //ゲーム全般を通してのスコア
    public static int stageScore; //そのステージに獲得したスコア

    //startよりも前に処理される
    void Awake()
    {
        //ゲームの初期状態をplaying
        gameState = "playing";
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}