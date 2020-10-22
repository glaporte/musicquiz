using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    public Text PageTitle = null;
    public static Game Get { get; private set; }


    private void Awake()
    {
        Get = this;
    }

    public void StartGame(Playlist playlist)
    {

    }

}

