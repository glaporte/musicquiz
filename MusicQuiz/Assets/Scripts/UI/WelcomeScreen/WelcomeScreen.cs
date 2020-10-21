using System.Collections;
using System.IO;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _playlistItem = null;

    [SerializeField]
    private GameObject _scrollViewContainer = null;

    [SerializeField]
    private Color[] _playListColor = new Color[2];

    private IEnumerator Start()
    {
        yield return PlaylistsLoader.LoadPlaylists(Path.Combine(Application.streamingAssetsPath, "gamedata.json"));
        if (PlaylistsLoader.Playlists != null)
        {
            for (int i = 0; i < PlaylistsLoader.Playlists.Length; i++)
            {
                Playlist playlist = PlaylistsLoader.Playlists[i];
                GameObject newItem = GameObject.Instantiate(_playlistItem);
                newItem.transform.SetParent(_scrollViewContainer.transform, false);
                newItem.GetComponent<PlaylistItem>().SetDisplay(playlist.playlist, 
                    playlist.questions.Length,
                    _playListColor[i % 2]);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Error Loading playlists");
        }

    }

    private void Update()
    {
        
    }
}
