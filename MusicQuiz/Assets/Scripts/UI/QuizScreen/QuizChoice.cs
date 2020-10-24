using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuizChoice : MonoBehaviour
{
    [SerializeField]
    private Text _artist = null;
    [SerializeField]
    private Text _title = null;

    private Button _button;

    public void SetDisplay(Choice choice, Action<int> callbackAnswer, int answerIndex)
    {
        _title.text = choice.title;
        _artist.text = choice.artist;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            callbackAnswer(answerIndex);
        });
    }

}
