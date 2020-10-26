using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField]
    private Text _uiScore = null;

    [SerializeField]
    private GameObject _summaryLayer = null;

    [SerializeField]
    private Button _replayButton = null;

    [SerializeField]
    private GameObject _questionAnswerSummaryPrefab = null;

    [SerializeField]
    private Queue<ScoreItem> _answers;

    private void Awake()
    {
        _replayButton.onClick.AddListener(() => { Game.Get.Replay(); });
        _answers = new Queue<ScoreItem>();
        for (int i = 0; i < Game.MAX_QUESTION; i++)
        {
            GameObject obj = Instantiate(_questionAnswerSummaryPrefab);
            _answers.Enqueue(obj.GetComponent<ScoreItem>());
            obj.transform.SetParent(_summaryLayer.transform, false);
        }
    }

    public void Display(List<Game.Score> scores)
    {
        float sum = 0;
        foreach (Game.Score score in scores)
        {
            ScoreItem scoreItem = _answers.Dequeue();
            scoreItem.Display(score);
            if (score.good)
            {
                sum += 1;
                sum += score.velocity;
            }
            _answers.Enqueue(scoreItem);
        }
        _uiScore.text = sum.ToString("0.00");
    }

}
