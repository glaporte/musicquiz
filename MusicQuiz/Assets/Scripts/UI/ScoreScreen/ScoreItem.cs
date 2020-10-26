using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    [SerializeField]
    private Text _songName = null;

    [SerializeField]
    private Text _answer = null;

    [SerializeField]
    private RawImage _background = null;

    public void Display(Game.Score score)
    {
        _songName.text = score.question.song.title;
        if (score.good)
        {
            _answer.text = score.time.ToString("0.0") + "s";
            _background.color = new Color(0, 1, 0, .5f);
        }
        else
        {
            _answer.text = "-";
            _background.color = new Color(1, 0, 0, .5f);
        }
    }

}
