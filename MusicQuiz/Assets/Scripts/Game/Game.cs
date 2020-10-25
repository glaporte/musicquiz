using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Game : MonoBehaviour
{
    [SerializeField]
    private Animation _animation = null;
    [SerializeField]
    public Text PageTitle = null;

    private AudioSource _audio;
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
        _audio = GetComponent<AudioSource>();
        Get = this;
        Initiatilization();
    }

    public void Initiatilization()
    {
        _audio.Play();
        Playlist.gameObject.SetActive(true);
        Quiz.gameObject.SetActive(false);
        _animation.gameObject.SetActive(true);
        _animation.Play();
    }

    public void PlayAudioFX(AudioClip clip, float vol)
    {
        _audio.PlayOneShot(clip, vol);
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
        PlaylistsLoader.LoadPlaylistContent(playlist, false);
        Playlist.gameObject.SetActive(false);
        Quiz.gameObject.SetActive(true);
        Quiz.SetDisplay(playlist);
    }


}

