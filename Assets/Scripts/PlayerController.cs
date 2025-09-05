using JetBrains.Annotations;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの能力値")]
    public float speed = 3.0f; //プレイヤーのスピードを調節
    public float jumpPower = 9.0f; //ジャンプ力の調節。表でも触れるようにパブリック

    [Header("地面判定の対象レイヤー")]
    public LayerMask groundLayer; //地面レイヤーを指名するための変数

    Rigidbody2D rbody; //PlayerについているRigidbody2Dを扱うための変数
    Animator animator; //Animatorコンポーネントを扱うための変数

    float axisH; //入力の方向を記憶するための変数
    bool goJump = false; //ジャンプフラグ（true:真on、false:偽off）。初期値はoffにしておく
    bool onGround = false; //レイキャスト用。地面にいるかどうかの判定（地面にいるtrue,地面にいないfalse）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得。Unity空間から呼び出し
        animator = GetComponent<Animator>(); //Animatorコンポーネントの情報を取得。
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームのステータスがplayingではないなら
        if (GameManager.gameState != "playing")
        {
            return; //その１フレームを強制終了
        }

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
        //地面判定をサークルキャストで行って、その結果を変数onGroundに代入
        onGround = Physics2D.CircleCast(
            transform.position, //発射位置＝プレイヤーの位置（基準点）
            0.2f,               //調査する縁の半径の指定
            new Vector2(0, 1.0f), //発射方向※下方向
            0,                   //発射距離
            groundLayer          //対象となるレイヤー情報※LayerMask
            );

        //Velocityに値を代入。一時的にメモリに値を確保して目的となる変数してもらう。new演算子。感知された入力に基づいてプレイヤーを動かす。x＝axisH*speed、y＝rbody.linearVelocity.y
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        //ジャンプフラグが立ったら
        if (goJump)
        {
            //ジャンプさせる→プレイヤーを上に押し出す
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; //フラグをOffに戻す
        }

        //if (onGround)//地面の上にいるとき
        //{
        if (axisH == 0) //左右が押されていない
        {
            animator.SetBool("Run", false); //Idleアニメに切り替え
        }
        else //左右が押されている
        {
            animator.SetBool("Run", true); //Runアニメに切り替え
        }
        //}
    }
    //ジャンプボタンが押された時に呼び出されるメソッド
    void Jump()
    {
        if (onGround)
        {
            goJump = true; //ジャンプフラグをON
            animator.SetTrigger("Jump");
        }
    }

    //isTrigger特性を持っているColliderとぶつかったら処理される
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag=="Goal")
        if (collision.gameObject.CompareTag("Goal"))
        {
            GameManager.gameState = "gameclear";
            Debug.Log("ゴールに接触した！");
            Goal();
        }
    }

    //ゴールした時のメソッド
    public void Goal()
    {
        animator.SetBool("Clear", true); //クリアアニメに切り替え
        GameStop(); //プレイヤーのVelocityを止めるメソッド
    }

    void GameStop()
    {
        //速度を０にリセット
        rbody.linearVelocity=new Vector2(0,0);
        //rbody.lineaVelocity = Vector2.zero;
    }


  
}
