using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;  // モバイルジョイスティック用

public class PlayerControllerSetup : MonoBehaviour {
	
	// 前進速度
	public float forwardSpeed = 5F;
	// 後退速度
	public float backwardSpeed = 2F;
	// 旋回速度
	public float rotateSpeed = 2F;
	// ジャンプ力
	public float jumpSpeed = 8F;
	// 重力
	public float gravity = 20F;
	
	private bool isJump = false;

	// キャラクターコントローラー
	CharacterController character;
	// 移動量
	Vector3 velocity;
	
	// ジャンプ
	void Jump () {
		Debug.Log("JUMP");
		if ( character.isGrounded ) isJump = true;
	}
	
	void Start () {
		// CharacterController コンポーネントを取得する
		character = GetComponent<CharacterController>();
	}
	
	void Update () {
		float v = CrossPlatformInputManager.GetAxis("Vertical");	// 上下のキー入力
		float h = CrossPlatformInputManager.GetAxis("Horizontal");	// 左右のキー入力

		if (character.isGrounded) {	// キャラクターが地面に着地しているか
			velocity = new Vector3(0, 0, v);		// 上下のキー入力からZ軸方向の移動量を取得
			// キャラクターのローカル空間での方向に変換
			velocity = transform.TransformDirection(velocity);
			
			if (v > 0) {
				velocity *= forwardSpeed;		// 移動速度を掛ける
			} else if (v < 0) {
				velocity *= backwardSpeed;	// 移動速度を掛ける
			}
			
			if (Input.GetKey(KeyCode.Space)) {	// スペースキーを入力したら
				// ジャンプ
				isJump = true;
			}
		}
		
		// Jump
		if (isJump) {
			velocity.y = jumpSpeed;		// 移動量のY軸方向にジャンプ力をセット
			isJump = false;	
		}
		
		velocity.y -= gravity * Time.deltaTime;		// 移動量に重力を加える
		// キャラクターコントローラーを移動させる
		// 戻り値で、キャラクタの何処にぶつかったかを判断している
		CollisionFlags flag = character.Move(velocity * Time.deltaTime);
		if (flag == CollisionFlags.None) {
			// 何も衝突していない
		}
		if ((flag & CollisionFlags.Above) == CollisionFlags.Above) {
			// キャラクターの上部に衝突した
		}
		if ((flag & CollisionFlags.Sides) == CollisionFlags.Sides) {
			// キャラクターの側面に衝突した
		}
		if ((flag & CollisionFlags.Below) == CollisionFlags.Below) {
			// キャラクターの下部に衝突した 
		}
		// 左右のキー入力でキャラクタをY軸で旋回させる
		transform.Rotate(0, h * rotateSpeed, 0);	
	}
}
