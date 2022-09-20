using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private string url;

    public List<Sprite> Sprites { get; private set; }
    public bool Loaded
    {
        get { return Sprites.Count >= 3; }
    }

    private ImageURL imageUrl;
    private void Start()
    {
        Sprites = new List<Sprite>();
        StartCoroutine(LoadJson());
    }
    private IEnumerator LoadJson()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        imageUrl = JsonUtility.FromJson<ImageURL>(webRequest.downloadHandler.text);

        StartCoroutine(LoadImage(imageUrl.image1));
        StartCoroutine(LoadImage(imageUrl.image2));
        StartCoroutine(LoadImage(imageUrl.image3));
    }

    private IEnumerator LoadImage(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);

        yield return webRequest.SendWebRequest();

        Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

        Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height),
            Vector2.zero);

        Sprites.Add(sprite);
        Debug.Log(Loaded);
    }

}

public struct ImageURL
{
    public string image1;
    public string image2;
    public string image3;
}
