using UnityEngine;

public class CoinRotate : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 100f; // rotate speed

    void Update() {
        // Rotate the coin around the X-axis or Y-axis as needed
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        // transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        // transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
