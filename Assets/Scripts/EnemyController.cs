using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("折り返し")]
    public GameObject sencer;

    [Header("基本設定")]
    public float speed = 1.0f; // 水平方向の移動スピード（正の値）
    public bool isRight; // true: 右移動 / false: 左移動（初期向き）

    Rigidbody2D rbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        // 見た目の左右反転。右へ動くときはスプライトの右向きに合わせて -1 を掛けて反転。
        if (isRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            // 右へ移動。y 成分は既存の落下/上昇速度を維持。
            rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
            // 見た目を右向きに（スプライト基準に応じて -1/1 を使い分け）
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // 左へ移動
            rbody.linearVelocity = new Vector2(-speed, rbody.linearVelocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    // 「何かにぶつかった」瞬間に折り返す
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) // 地面(Ground)以外に当たったら反転（＝壁/敵/プレイヤーなども含む）
        {
            isRight = !isRight;// 進行方向を反転
        }
    }
    
    //isTrigger の“センサー”が地面(Ground)から外れたら反転する
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isRight = !isRight;// 進行方向を反転
        }
    }
}
