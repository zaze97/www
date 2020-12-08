using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ScreenShotPNG());
    }
    IEnumerator ScreenShotPNG()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
    }
}
  
