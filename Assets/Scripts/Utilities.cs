using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Utilities
{
    public static void DownloadImage(string url, Action<Sprite> callback)
    {
        Downloader l_Downloader = new GameObject().AddComponent<Downloader>();
        l_Downloader.name = url;
        l_Downloader.StartCoroutine(DownloadImageCoroutine(l_Downloader, url, callback));
    }

    private static IEnumerator DownloadImageCoroutine(Downloader downloder, string url, Action<Sprite> callback)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Download image failed for " + url);

            // destroy behaviour that invokes this download
            GameObject.Destroy(downloder.gameObject);

            // invoke callback
            callback?.Invoke(null);
        }
        else
        {
            // destroy behaviour that invokes this download
            GameObject.Destroy(downloder.gameObject);

            // convert texture to sprite
            Texture2D l_Texture = DownloadHandlerTexture.GetContent(www);
            Sprite l_Sprite = Sprite.Create(l_Texture, new Rect(0f, 0f, l_Texture.width, l_Texture.height), new Vector2(0.5f, 0.5f), 100f);

            // invoke callback
            callback?.Invoke(l_Sprite);
        }
    }
}
