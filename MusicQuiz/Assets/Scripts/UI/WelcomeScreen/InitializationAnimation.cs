using UnityEngine;

[RequireComponent(typeof(Animation))]
public class InitializationAnimation : MonoBehaviour
{
    private Animation _animation;

    private void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
    }

    public void InitializationFinish()
    {
        _animation.Stop();
       _animation.gameObject.SetActive(false);
    }
}
