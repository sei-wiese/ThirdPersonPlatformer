using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnJump = new UnityEvent();
    public UnityEvent OnDash = new UnityEvent();


        void Update() {
        Vector2 moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveInput += Vector2.up;
        if (Input.GetKey(KeyCode.S)) moveInput += Vector2.down;
        if (Input.GetKey(KeyCode.A)) moveInput += Vector2.left;
        if (Input.GetKey(KeyCode.D)) moveInput += Vector2.right;
        OnMove?.Invoke(moveInput);

        // OnJump (space key)
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnJump?.Invoke();
        }

        // OnDash (LeftShift key)
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            OnDash?.Invoke();
        }
    }

}
