using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class QuizScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _blockAnswer = null;
    [SerializeField]
    private GameObject _progressLayer = null;
    [SerializeField]
    private GameObject _goodAnswerTickPrefab = null;
    [SerializeField]
    private GameObject _badAnswerTickPrefab = null;

    [SerializeField]
    private RawImage _artist = null;

    [SerializeField]
    private Slider _slider = null;

    [SerializeField]
    private GameObject _choice = null;

    [SerializeField]
    private GameObject _grid = null;

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
    [SerializeField]
    private AudioClip _goodAnswerAudio = null;
    [SerializeField]
    private AudioClip _badAnswerAudio = null;

    private Queue<GameObject> _quizChoices;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _quizChoices = new Queue<GameObject>();
        for (int i = 0; i < Game.MAX_CHOICE; i++)
        {
            _quizChoices.Enqueue(GameObject.Instantiate(_choice));
        }
    }

    public void SetDisplay(Playlist playlist)
    {
        // Destroy progress in replay game case.
        foreach (Transform t in _progressLayer.transform)
            Destroy(t.gameObject);

        _activePlaylist = playlist;
        Game.Get.PageTitle.text = playlist.playlist;
        _activeQuestionIndex = -1;
        NextQuestion();
    }

    private void NextQuestion()
    {
        _blockAnswer.blocksRaycasts = false;
        _activeQuestionIndex++;
        if (_activeQuestionIndex < _activePlaylist.questions.Length)
        {
            SetQuestion(_activePlaylist.questions[_activeQuestionIndex]);
        }
        else
        {
            Game.Get.DisplayScore(_activePlaylist);
        }
    }

    private IEnumerator WaitLoading(Question question)
    {
        while (question.song.Audio == null || question.song.Picture == null)
        {
            yield return null;
        }
        SetQuestion(question);
    }

    private void SetQuestion(Question question)
    {
        if (question.song.Audio == null || question.song.Picture == null)
        {
            StartCoroutine(WaitLoading(question));
            return;
        }
        _audio.volume = 1f;
        _activeQuestion = question;
        int cuurentChoiceIndex = 0;
        List<GameObject> choices = new List<GameObject>();
        foreach (Choice choice in question.choices)
        {
            GameObject item = _quizChoices.Dequeue();
            item.GetComponent<QuizChoice>().SetDisplay(choice, ChoiceSelected, cuurentChoiceIndex);
            _quizChoices.Enqueue(item);
            cuurentChoiceIndex++;
            item.transform.SetParent(null);
            choices.Add(item);
        }
        choices = choices.OrderBy(x => Guid.NewGuid()).ToList();
        foreach (GameObject item in choices)
        {
            item.transform.SetParent(_grid.transform, false);
        }


        _artist.texture = question.song.Picture;
        _audio.clip = question.song.Audio;
        _audio.PlayDelayed(0.5f);
        Invoke("OutOfTime", _audio.clip.length + 0.5f);
        Invoke("EnableSelection", 0.5f);
    }

    private void EnableSelection()
    {
        _blockAnswer.blocksRaycasts = true;
    }

    private void OutOfTime()
    {
        ChoiceSelected(-1);
    }
    
    public void ChoiceSelected(int answerIndex)
    {
        CancelInvoke();
        _audio.volume = 0.33f;
        _resultLayer.SetActive(true);
        _blockAnswer.blocksRaycasts = false;
        bool good = answerIndex == _activeQuestion.answerIndex;
        if (answerIndex == _activeQuestion.answerIndex)
        {
            _badAnswer.SetActive(false);
            _goodAnswer.SetActive(true);
            Game.Get.PlayAudioFX(_goodAnswerAudio, 1f);
           Instantiate(_goodAnswerTickPrefab).transform.SetParent(_progressLayer.transform, false);
        }
        else
        {
            _goodAnswer.SetActive(false);
            _badAnswer.SetActive(true);
            Game.Get.PlayAudioFX(_badAnswerAudio, 1f);
            Instantiate(_badAnswerTickPrefab).transform.SetParent(_progressLayer.transform, false);
        }
        Game.Get.AddScore(new Game.Score { good = good, time = _audio.time, velocity = 1f - (_audio.time / _audio.clip.length), question = _activeQuestion });
        StartCoroutine(AnimChoiceSelected());
    }

    private IEnumerator AnimChoiceSelected()
    {
        yield return new WaitForSeconds(1.5f);
        _resultLayer.SetActive(false);
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
