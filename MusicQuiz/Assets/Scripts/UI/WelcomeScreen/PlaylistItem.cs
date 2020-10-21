using UnityEngine;
using UnityEngine.UI;

public class PlaylistItem : MonoBehaviour
{
    [SerializeField]
    private Text _title = null;

    [SerializeField]
    private Text _questionCount = null;

    [SerializeField]
    private RawImage _background = null;

    public void SetDisplay(string title, int questionCount, Color backgroundColor)
    {
        _title.text = title;
        _questionCount.text = questionCount + " questions !";
        _background.color = backgroundColor;
    }
}
