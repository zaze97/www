using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpWrapper : MonoBehaviour
{
    /// <summary>
    /// 请求数据
    /// </summary>
    /// <param name="url">链接</param>
    /// <param name="onSuccess">成功事件</param>
    /// <param name="onFail">失败事件</param>
    public void GET(string url, Action<UnityWebRequest> onSuccess, Action<UnityWebRequest> onFail = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        StartCoroutine(WaitForResponse(request, onSuccess, onFail));
    }


    /// <summary>
    /// 开启一个协程，发送请求
    /// 将一个表上传到远程的服务器
    /// </summary>
    /// <returns></returns>
    public void POST(string url, Dictionary<string, string> post=null, Action<UnityWebRequest> onSuccess=null, Action<UnityWebRequest> onFail = null)
    {
        WWWForm form = new WWWForm();
        post = new Dictionary<string, string>();
        post.Add("key", "value");
        post.Add("name", "Chinar");
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            {
                form.AddField(post_arg.Key, post_arg.Value);
            }
        }
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        StartCoroutine(WaitForResponse(request, onSuccess, onFail));
    }

    public void Put(string url, string Chinar, Action<UnityWebRequest> onSuccess, Action<UnityWebRequest> onFail = null)
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(Chinar);
        using (UnityWebRequest uwr = UnityWebRequest.Put("url", myData))
        {
            StartCoroutine(WaitForResponse(uwr, onSuccess, onFail));
        }
    }
    IEnumerator WaitForResponse(UnityWebRequest request, Action<UnityWebRequest> onSuccess, Action<UnityWebRequest> onFail = null)
    {
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError("UnityWebRequest Error: " + request.error);
            if (onFail != null) onFail(request);
        }
        else
        {
            Debug.Log("UnityWebRequest Get: " + request.downloadedBytes);
            onSuccess(request);
        }
    }
}
