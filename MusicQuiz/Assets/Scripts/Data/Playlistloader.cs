using System;
using System.Collections;
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
}
