using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpRequested = false;

    //キャラクターの向きを管理する変数を追加
    private bool isFacingRight = true;

     //地面判定用の変数を追加
    public Transform groundCheckPoint;     // 地面判定センサーの位置
    public float groundCheckRadius = 0.2f; // 地面判定センサーの半径
    public LayerMask groundLayer;          // 地面レイヤーを指定する変数
    private bool isGrounded = false;               // 地面に接しているかどうかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // 毎フレーム、地面に接しているかチェックする ---
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        moveInput = Input.GetAxis("Horizontal");

        // moveInputの値に応じてキャラクターを反転させる
        if (moveInput > 0 && !isFacingRight)
        {
            // 右を向いていない時に右入力があったら、右を向かせる
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            // 右を向いている時に左入力があったら、左を向かせる
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }

    //反転処理をまとめたメソッドを追加
    void Flip()
    {
        // 現在の向きを逆にする
        isFacingRight = !isFacingRight;
        
        // ローカルスケールのXの値だけを反転させる
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
