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
    
    private AudioSource _audio;


    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetDisplay(Playlist playlist)
    {
        Game.Get.PageTitle.text = playlist.playlist;
        _questionNumber.text = playlist.questions.Length.ToString();
        SetQuestion(playlist.questions[0], 1);
    }

    private void SetQuestion(Question question, int index)
    {
        foreach (Choice choice in question.choices)
        {
            GameObject item = GameObject.Instantiate(_choice);
            item.GetComponent<QuizChoice>().SetDisplay(choice);
        }

        _questionNumber.text = index.ToString();
        _artist.texture = question.song.Picture;
        _audio.PlayOneShot(question.song.Audio);
    }

    private void Update()
    {
        if (_audio.isPlaying)
        {
            _slider.value = _audio.time / _audio.clip.length;
        }
    }
}
