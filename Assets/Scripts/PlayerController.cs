using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //PlayerについているRigidbody2Dを扱うための変数

    float axisH; //入力の方向を記憶するための変数
    public float speed = 3.0f; //プレイヤーのスピードを調節

    public float jumpPower = 9.0f; //ジャンプ力の調節。表でも触れるようにパブリック
    bool goJump = false; //ジャンプフラグ（true:真on、false:偽off）。初期値はoffにしておく

    bool onGround = false; //地面にいるかどうかの判定（地面にいるtrue,地面にいないfalse）

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

        if (axisH > 0)
        {
            //右を向く
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (axisH < 0)
        {
            //左を向く
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //GetButtonDownメソッド→引数に指定下ボタンが押されたらtrueを返す、押されてなければfalseを返す
        if (Input.GetButtonDown("Jump"))
        {
            Jump(); //Jumpメソッドの発動
        }

    }

    //１秒間に50回(50fps)繰り返すように制御しながら行う繰り返しメソッド（ユーザごとのPC環境で変化しないように制御するフレームレートを追加）
    void FixedUpdate()
    {
        //Velocityに値を代入。一時的にメモリに値を確保して目的となる変数してもらう。new演算子。感知された入力に基づいてプレイヤーを動かす。x＝axisH*speed、y＝rbody.linearVelocity.y
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //ジャンプフラグが立ったら
        if (goJump == true)
        {
            //ジャンプさせる→プレイヤーを上に押し出す
            rbody.AddForce(new Vector2(0,jumpPower), ForceMode2D.Impulse);
            goJump = false; //フラグをOffに戻す
        }
    }
    //ジャンプボタンが押された時に呼び出されるメソッド
    void Jump()
    {
        goJump = true; //ジャンプフラグをON
    }
}
