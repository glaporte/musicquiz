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
    private Question _activeQuestion;
    private int _activeQuestionIndex;


    [SerializeField]
    private GameObject _goodAnswer = null;
    [SerializeField]
    private GameObject _badAnswer = null;
    [SerializeField]
    private GameObject _resultLayer = null;

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

    private void NextQuestion()
    {
        if (_activeQuestionIndex < _activePlaylist.questions.Length)
        {
            _activeQuestionIndex++;
            SetQuestion(_activePlaylist.questions[_activeQuestionIndex], _activeQuestionIndex);
        }
        else
        {

        }
    }

    private void SetQuestion(Question question, int index)
    {
        _activeQuestion = question;
        _activeQuestionIndex = index;
        foreach (Choice choice in question.choices)
        {
            GameObject item = GameObject.Instantiate(_choice);
            item.GetComponent<QuizChoice>().SetDisplay(choice, ChoiceSelected, _activeQuestionIndex);
            item.transform.SetParent(_grid.transform, false);
        }

        _questionNumber.text = _activeQuestionIndex.ToString() + " on " + _activePlaylist.questions.Length + " questions.";
        _artist.texture = question.song.Picture;
        _audio.clip = question.song.Audio;
        _audio.PlayDelayed(0.5f);
    }
    
    public void ChoiceSelected(int answerIndex)
    {
        _resultLayer.SetActive(true);
        bool good = answerIndex == _activeQuestion.answerIndex;
        if (answerIndex == _activeQuestion.answerIndex)
        {
            _badAnswer.SetActive(false);
            _goodAnswer.SetActive(true);
        }
        else
        {
            _goodAnswer.SetActive(false);
            _badAnswer.SetActive(true);
        }
        Game.Get.AddScore(new Game.Score { good = good, velocity = _audio.time / _audio.clip.length });
        _audio.Stop();
        NextQuestion();
    }

    private void Update()
    {
        if (_audio.isPlaying)
        {
            _slider.value = _audio.time / _audio.clip.length;
        }
    }
}
