using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    public Text PageTitle = null;
    public static Game Get { get; private set; }

    public WelcomeScreen Playlist;
    public QuizScreen Quiz;

    private void Awake()
    {
        Get = this;
    }

    public void StartGame(Playlist playlist)
    {
        Playlist.gameObject.SetActive(false);
        Quiz.gameObject.SetActive(true);
        Quiz.SetDisplay(playlist);
    }

}

