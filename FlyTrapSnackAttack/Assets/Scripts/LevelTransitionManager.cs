using TMPro;
using UnityEngine;

public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreboard;
    private int score;

    private void Update()
    {
        score = GameManager.Instance.GetScore();
        scoreboard.text = "Level Score: " + score.ToString();
    }
}
