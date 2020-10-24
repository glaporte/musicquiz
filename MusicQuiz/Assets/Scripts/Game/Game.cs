using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    public Text PageTitle = null;
    public static Game Get { get; private set; }

    public WelcomeScreen Playlist;
    public QuizScreen Quiz;

    public struct Score
    {
        public bool good;
        public float velocity;
    }

    private List<Score> _results;

    private void Awake()
    {
        _results = new List<Score>();
        Get = this;
    }

    public void AddScore(Score score)
    {
        _results.Add(score);
    }

    public void DisplayResult()
    {

    }

    public void StartGame(Playlist playlist)
    {
        Playlist.gameObject.SetActive(false);
        Quiz.gameObject.SetActive(true);
        Quiz.SetDisplay(playlist);
    }


}

