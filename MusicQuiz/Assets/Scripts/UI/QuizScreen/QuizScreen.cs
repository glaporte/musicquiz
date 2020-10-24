using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class QuizScreen : MonoBehaviour
{
    [SerializeField]
    private Text _questionNumber = null;

    [SerializeField]
    private RawImage _artist = null;

    [SerializeField]
    private Slider _slider = null;

    [SerializeField]
    private GameObject _choice = null;

    [SerializeField]
    private GameObject _grid = null;

    [SerializeField]
    private Button _nextQuestion = null;
    
    private AudioSource _audio;

    private Playlist _activePlaylist;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetDisplay(Playlist playlist)
    {
        _activePlaylist = playlist;
        Game.Get.PageTitle.text = playlist.playlist;
        SetQuestion(playlist.questions[0], 1);
    }

    private void SetQuestion(Question question, int index)
    {
        foreach (Choice choice in question.choices)
        {
            GameObject item = GameObject.Instantiate(_choice);
            item.GetComponent<QuizChoice>().SetDisplay(choice, ChoiceSelected, index);
            item.transform.SetParent(_grid.transform, false);
        }

        _questionNumber.text = index.ToString() + " on " + _activePlaylist.questions.Length + " questions.";
        _artist.texture = question.song.Picture;
        _audio.clip = question.song.Audio;
        _audio.PlayDelayed(0.5f);
    }
    
    public void ChoiceSelected(int answerIndex)
    {

    }

    private void Update()
    {
        if (_audio.isPlaying)
        {
            _slider.value = _audio.time / _audio.clip.length;
        }
    }
}
