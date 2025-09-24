using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // 連携したいスクリプトを格納する変数
    private PlayerController playerController;
    private PlayerAnimationController animationController;
    private Rigidbody2D rb;
    public GameState currentState = GameState.Playing;

    void Start()
    {
        // プレイヤーに付いている各コンポーネントを自動で取得
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Is TriggerがオンのColliderに触れた瞬間に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 触れた相手のタグで処理を分岐
        if (other.CompareTag("Goal"))
        {
            HandleGoal();
        }
        else if (other.CompareTag("Dead"))
        {
            HandleGameOver();
        }
    }

    private void HandleGoal()
    {
        // PlayerControllerを無効にして、プレイヤーの操作を停止
        if (playerController != null)
        {
            playerController.ChangeState(GameState.Goal);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        // PlayerAnimationControllerにゴールアニメーションの再生を依頼
        if (animationController != null)
        {
            animationController.PlayGoalAnimation();
        }
    }

    private void HandleGameOver()
    {
        // PlayerControllerを無効にして、プレイヤーの操作を停止
        if (playerController != null)
        {
            playerController.ChangeState(GameState.GameOver);
        }

        // PlayerAnimationControllerにゲームオーバーアニメーションの再生を依頼
        if (animationController != null)
        {
            animationController.PlayGameOverAnimation();
        }
    }
}