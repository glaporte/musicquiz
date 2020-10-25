using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Playlists
{
    public Playlist[] playlists;
}

[System.Serializable]
public class Playlist
{
    public string id;
    public Question[] questions;
    public string playlist;
}

[System.Serializable]
public class Question
{
    public string id;
    public int answerIndex;
    public Choice[] choices;
    public Song song;
}

[System.Serializable]
public class Choice
{
    public string artist;
    public string title;
}

[System.Serializable]
public class Song
{
    public string id;
    public string title;
    public string artist;
    public string picture;
    public string sample;
    public Texture2D Picture { get; set; }
    public AudioClip Audio { get; set; }

}


public static class PlaylistsLoader
{
    public static Playlist[] Playlists { get; private set; }

    private static string jsonData;

    public static IEnumerator LoadPlaylists(string jsonUri)
    {
        if (Playlists == null)
        {
            UnityWebRequest www = UnityWebRequest.Get(jsonUri);
            yield return www.SendWebRequest();
            jsonData = www.downloadHandler.text;
            Playlists = JsonUtility.FromJson<Playlists>(jsonData).playlists;
        }
    }
    public static void LoadPlaylistContent(Playlist playlist, bool firstQuestionOnly)
    {
        foreach (Question question in playlist.questions)
        {
            if (question.song.Picture == null)
            {
                Game.Get.StartCoroutine(DownloadPicture(question.song));
            }

            if (question.song.Audio == null)
            {
                Game.Get.StartCoroutine(DownloadAudio(question.song));
            }
            if (firstQuestionOnly)
                break;
        }
    }

    private static IEnumerator DownloadPicture(Song song)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(song.picture);
        yield return www.SendWebRequest();
        song.Picture = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
    }

    private static IEnumerator DownloadAudio(Song song)
    {
        UnityWebRequest www2 = UnityWebRequestMultimedia.GetAudioClip(song.sample, AudioType.WAV);
        yield return www2.SendWebRequest();
        song.Audio = ((DownloadHandlerAudioClip)www2.downloadHandler).audioClip;
    }
}
