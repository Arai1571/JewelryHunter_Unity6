using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static string gameState; //静的メンバ

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