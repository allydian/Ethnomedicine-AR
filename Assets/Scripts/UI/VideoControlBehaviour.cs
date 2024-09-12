using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControlBehaviour : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }

    public void ForwardTenSeconds()
    {
        Debug.Log("Forward 10s");
        videoPlayer.time += 10.0f;
    }

    public void BackwardTenSeconds()
    {
        Debug.Log("Backward 10s");
        videoPlayer.time -= 10.0f;
    }

    public void PlayVideo()
    {
        Debug.Log("Play Video");
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
            //transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void PauseVideo()
    {
        Debug.Log("Pause Video");
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            //transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
    }

    public void UpdateVideoPlaybackSpeed(float speed)
    {
        Debug.Log("Playback speed to " + speed);
        videoPlayer.playbackSpeed = speed;
    }


}