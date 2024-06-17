using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas canvas;

    void Start()
    {
        // Mulai pemutaran video
        videoPlayer.Play();
        // Menjalankan fungsi untuk menonaktifkan canvas setelah video selesai
        Invoke("DisableCanvas", (float)videoPlayer.length);
    }

    void DisableCanvas()
    {
        // Nonaktifkan canvas
        canvas.gameObject.SetActive(false);
    }
}