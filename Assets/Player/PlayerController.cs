using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpRequested = false;
    private bool isFacingRight = true;

    // --- 他のスクリプトと連携するための変数 ---
    private PlayerAnimationController animator;
    public GameState currentState = GameState.Playing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimationController>();
    }
    
    void Update()
    {
        // プレイ中でなければ、以降の操作を一切受け付けない
        if (currentState != GameState.Playing)
        {
            return;
        }

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

        //jump
        if (Input.GetButtonDown("Jump") && animator.IsGrounded())
        {
            jumpRequested = true;
        }
    }
    
    void FixedUpdate()
    {
        // プレイ中でなければ、以降の操作を一切受け付けない
        if (currentState != GameState.Playing)
        {
            return;
        }
        
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
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
