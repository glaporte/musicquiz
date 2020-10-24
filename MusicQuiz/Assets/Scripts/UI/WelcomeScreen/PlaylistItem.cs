using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaylistItem : MonoBehaviour
{
    [SerializeField]
    private Text _title = null;

    [SerializeField]
    private Text _questionCount = null;

    [SerializeField]
    private RawImage _background = null;

    private Playlist _playlist;

    public void SetDisplay(Playlist playlist, int questionCount, Color backgroundColor)
    {
        _playlist = playlist;
        _title.text = playlist.playlist;
        _questionCount.text = questionCount + " questions !";
        _background.color = backgroundColor;


        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => Game.Get.StartGame(playlist));
    }
}
