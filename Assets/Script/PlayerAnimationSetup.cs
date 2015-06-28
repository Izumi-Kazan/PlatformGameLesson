using UnityEngine;
using System.Collections;

public class PlayerAnimationSetup : MonoBehaviour {
    // スライディング時間
    public float slidingTime = 1.0f;

    // Animatorコンポネント
    Animator animator;

    // キャラクタコントローラ
    CharacterController characterController;

    // スライディング実行時間
    float slidingTimeLeft = 0;

    // コライダの中心
    Vector3 colliderCenter;

    // コライダの高さ
    float colliderHeight;

	// Use this for initialization
	void Start () {
        // キャラクタコントローラの取得
        characterController = GetComponent<CharacterController>();

        // コライダの中心点の取得
        colliderCenter = characterController.center;

        // コライダの高さを取得
        colliderHeight = characterController.height;

        // Animationコンポネント
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // キー入力の取得
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool isJump = Input.GetKey(KeyCode.Space);
        bool isSliding = Input.GetKey(KeyCode.C);

        // TreasureControllerにパラメータをセット：メカニムのアニメーションステートを遷移する
        animator.SetBool("isGround", characterController.isGrounded);
        animator.SetBool("isJumpStart", false);
        animator.SetBool("isSlidingStart", false);

        // スライディング実行時間を減少
        if (slidingTimeLeft > 0) slidingTimeLeft -= Time.deltaTime;

        //
        animator.SetFloat("slidingTime", slidingTimeLeft);


        if (characterController.isGrounded)
        {

            animator.SetFloat("Speed", v);

            if (isJump)
            {
                animator.SetBool("isJumpStart", true);
            }

            if (isSliding)
            {
                animator.SetBool("isSlidingStart", true);
                slidingTimeLeft = slidingTime;
                animator.SetFloat("slidingTime", slidingTime);
            }
        }

        // 現在のアニメーションステートを取得
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);


        // アニメーションの状態がスライディングになっているかどうかチェク
        if (state.fullPathHash == Animator.StringToHash("Base Layer.Sliding"))
        {
            // スライディング状態の時、カレントコライダの中心から下方向に向かって、コライダの高さを半分に
            Vector3 newColliderCenter = colliderCenter;
            newColliderCenter.y *= 0.5f;
            characterController.center = newColliderCenter;
            characterController.height = colliderHeight * 0.5f;
        } else
        {
            // スライディング状態ではない時は、コライダの中心位置および高さを初期値に戻す
            characterController.center = colliderCenter;
            characterController.height = colliderHeight;
        }
    }
}
