using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour
{
    public const int MAX_QUESTION = 5;
    public const int MAX_CHOICE = 4;

    [SerializeField]
    private Animation _animation = null;
    [SerializeField]
    public Text PageTitle = null;

    private AudioSource _audio;
    public static Game Get { get; private set; }

    public WelcomeScreen Playlist;
    public QuizScreen Quiz;
    public ScoreScreen ScoreScreen;

    public struct Score
    {
        public bool good;
        public float time;
        public float velocity;
        public Question question;
    }

    private List<Score> _results;

    private void Awake()
    {
        _results = new List<Score>();
        _audio = GetComponent<AudioSource>();
        Get = this;
        Initiatilization();
    }

    public void Initiatilization()
    {
        _audio.Play();
        Playlist.gameObject.SetActive(true);
        Quiz.gameObject.SetActive(false);
        ScoreScreen.gameObject.SetActive(false);
        _animation.gameObject.SetActive(true);
        _animation.Play();
    }

    public void Replay()
    {
        ScoreScreen.gameObject.SetActive(false);
        Playlist.gameObject.SetActive(true);
    }

    public void PlayAudioFX(AudioClip clip, float vol)
    {
        _audio.PlayOneShot(clip, vol);
    }

    public void AddScore(Score score)
    {
        _results.Add(score);
    }

    public void DisplayScore(Playlist playlist)
    {
        Quiz.gameObject.SetActive(false);
        ScoreScreen.gameObject.SetActive(true);
        ScoreScreen.Display(_results);
    }

    public void StartGame(Playlist playlist)
    {
        _results.Clear();
        PlaylistsLoader.LoadPlaylistContent(playlist, false);
        Playlist.gameObject.SetActive(false);
        Quiz.gameObject.SetActive(true);
        Quiz.SetDisplay(playlist);
    }


}

