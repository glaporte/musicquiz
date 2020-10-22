using UnityEngine;
using UnityEngine.UI;

public class QuizChoice : MonoBehaviour
{
    [SerializeField]
    private Text _artist = null;
    [SerializeField]
    private Text _title = null;
    public void SetDisplay(Choice choice)
    {
        _title.text = choice.title;
        _artist.text = choice.artist;
    }
}
