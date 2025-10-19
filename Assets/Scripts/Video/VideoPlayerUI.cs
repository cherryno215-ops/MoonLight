using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerUI : MonoBehaviour
{
    [SerializeField] private string videoFileName = "Intro.mp4";
    [SerializeField] private string nextSceneName = "Start";

    private void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        vp.loopPointReached += OnVideoEnd;
        vp.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (nextSceneName == "no")
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
