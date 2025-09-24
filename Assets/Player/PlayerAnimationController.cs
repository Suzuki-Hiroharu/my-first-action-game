using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    //アニメーションの名前
    public string runAnime = "Player_run";
    public string stopAnime = "Player_stop";
    public string jumpAnime = "Player_jump";
    public string goalAnime = "Player_goal";
    public string deadAnime = "Player_dead";

    //地面判定用の設定
    public Transform groundCheckPoint;     // 地面判定センサーの位置
    public float groundCheckRadius = 0.2f; // 地面判定センサーの半径
    public LayerMask groundLayer;          // 地面レイヤーを指定する変数
    private bool isGrounded = false;               // 地面に接しているかどうかのフラグ

    //内部で使う変数
    private Rigidbody2D rb;
    private float moveInput;
    private string nowAnime; // これから再生したいアニメーションの名前
    private string oldAnime; // 現在再生されているアニメーションの名前
    private bool isActionInProgress = false; // ゴールなどの特殊な行動中かどうかのフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // 初期状態を設定
        nowAnime = stopAnime;
        oldAnime = nowAnime;
        animator.Play(nowAnime);
    }

    // Update is called once per frame
    void Update()
    {
        // 特殊な行動中は、通常のUpdate処理を無視する
        if (isActionInProgress)
        {
            return;
        }
        
        // （通常の待機・走り・ジャンプなどのアニメーション切り替え処理）
        // --- 状態をチェック ---
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // --- 再生するアニメーションを決定 ---
        if(isGrounded){
            if(moveInput != 0){
            nowAnime = runAnime;
            }
            else
            {
            nowAnime = stopAnime;
            }
        }
        else{
            nowAnime = jumpAnime;
        }

        // --- 状態が変化した場合のみ、アニメーションを再生 ---
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }
    
    // --- 他のスクリプトから呼び出すための公開機能 ---
    public void PlayGoalAnimation()
    {
        isActionInProgress = true; // 特殊行動を開始
        animator.Play(goalAnime);
    }
    
    public void PlayGameOverAnimation()
    {
        isActionInProgress = true; // 特殊行動を開始
        animator.Play(deadAnime);
    }

     // --- 他のスクリプトに現在の接地状態を教えるための公開メソッド ---
    public bool IsGrounded()
    {
        return isGrounded;
    }

}
