using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //PlayerについているRigidbody2Dを扱うための変数

    float axisH; //入力の方向を記憶するための変数
    public float speed=3.0f; //プレイヤーのスピードを調節

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得。Unity空間から呼び出し


    }

    // Update is called once per frame
    void Update()
    {
        //もしも水平方向のキー「← 」「→」 が押されたら、Playerが動くように設定。!=0→「０でなければ押されたとみなす」
        //省いても成立するのでコメントアウト
        // if (Inpu.GetAxisRaw("Horizontal") != 0){}

        //Velocityの元となる値の取得。（右なら1.0f 左なら-1.0f、何もなければ０）入力を感知する
        axisH = Input.GetAxisRaw("Horizontal");

    }

    //１秒間に50回(50fps)繰り返すように制御しながら行う繰り返しメソッド（ユーザごとのPC環境で変化しないように制御するフレームレートを追加）
    private void FixedUpdate()
    {
        //Velocityに値を代入。一時的にメモリに値を確保して目的となる変数してもらう。new演算子。感知された入力に基づいてプレイヤーを動かす
        rbody.linearVelocity = new Vector2(axisH*speed, rbody.linearVelocity.y);
    }
}
