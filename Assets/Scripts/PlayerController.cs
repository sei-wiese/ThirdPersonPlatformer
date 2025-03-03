using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 100f;         // 基本の移動速度（調整済み）
    [SerializeField] private Transform cameraTransform;      // カメラのTransform
    [SerializeField] private InputManager inputManager;      // InputManager の参照

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();

        // InputManager の OnMove イベントにリスナーを登録
        if (inputManager != null) {
            inputManager.OnMove.AddListener(MovePlayer);
        }
    }

    // WASD の入力に応じた移動処理（シンプル版：左シフトによる速度倍率なし）
    private void MovePlayer(Vector2 input) {
        if (input.sqrMagnitude < 0.01f) return;

        // カメラの前方と右方向を取得（Y成分を除外して正規化）
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        // 入力に基づいた移動方向を算出
        Vector3 moveDir = (camForward * input.y) + (camRight * input.x);
        moveDir.Normalize();

        // Rigidbody を使って移動
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        // キャラクターの向きを移動方向に更新
        transform.forward = moveDir;
    }
}
