using UnityEngine;
using TMPro; // TextMeshPro

public class GameManager : MonoBehaviour {
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText; // Text to showing score

    public void AddScore(int amount) {
        score += amount;
        if (scoreText != null) {
            scoreText.text = "Score: " + score;
        }
    }
}
