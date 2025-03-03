using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField] private int scoreValue = 1;    // Score awarded when the coin is collected

    private GameManager gm;

    void Start() {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (gm != null) {
                gm.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
