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
    private Queue<GameObject> _answers;

    private void Awake()
    {
        _replayButton.onClick.AddListener(() => { Game.Get.Replay(); });
        _answers = new Queue<GameObject>();
        for (int i = 0; i < Game.MAX_QUESTION; i++)
        {
            _answers.Enqueue(Instantiate(_questionAnswerSummaryPrefab));
        }
    }

    public void Display( List<Game.Score> scores)
    {
        foreach (Game.Score score in scores)
        {
            //_answer = _answers.Dequeue();
        }
    }

}
