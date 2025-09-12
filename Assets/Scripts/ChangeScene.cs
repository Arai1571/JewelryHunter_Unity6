using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指名 
    public bool toTitle;     //タイトルへの切り替えかどうかのフラグ

    //シーンを切り替えるう機能を持ったメソッド作成
    public void Load()
    {
        //シーンが切り替わる際はいずれにしてもステージスコアはリセットされる
        GameManager.stageScore = 0;

        //totitleフラグがtrueになっている場合はタイトルに戻ることが予想されるのでトータルスコアもリセット
        if (toTitle) GameManager.totalScore = 0;
        

        //引数に指定した名前のシーンに切り替えしてくれるメソッド呼び出し
        SceneManager.LoadScene(sceneName);
    }
    
}
