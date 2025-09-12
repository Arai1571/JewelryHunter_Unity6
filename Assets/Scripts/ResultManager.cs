using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リザルト画面のScore Textオブジェクトが持つTextMesh Proのテキスト欄にGameManagerのStaric変数であるTotalScoreを代入。ただしString型に型変換が必要
        scoreText.GetComponent<TextMeshProUGUI>().text = GameManager.totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
