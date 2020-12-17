using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 02-ChinarDownLoadTexture：
/// 用来测试服务器上传
/// </summary>
public class ChinarDownLoadTexture : MonoBehaviour
{
    private string url = "http://images0.cnblogs.com/blog2015/686199/201505/311920537358907.jpg";

    public Image downImage;


    void Start()
    {
        StartCoroutine(GetTexture(url));
    }


    /// <summary>
    /// 请求图片
    /// </summary>
    /// <param name="url">图片地址,like 'http://images0.cnblogs.com/blog2015/686199/201505/311920537358907.jpg '</param>
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        DownloadHandlerTexture downloadTexture = new DownloadHandlerTexture(true);
        uwr.downloadHandler = downloadTexture;
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            print(uwr.error);
        }
        else
        {
            Texture2D t = downloadTexture.texture;
            downImage.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.one);
        }
    }

}
