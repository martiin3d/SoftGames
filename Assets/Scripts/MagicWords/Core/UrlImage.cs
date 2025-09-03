using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Component that loads an image from a URL, caches the texture and displays it in a UI <see cref="Image"/>.
/// It supports lazy loading (load when enabled) and automatic fallback to a default sprite on failure.
/// </summary>
public class UrlImage : MonoBehaviour, IUrlImage
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite _defaultImage;

    private string _url;

    private bool _loadOnEnable;
    private Texture2D _texture;

    private ITextureCache _cache;

    /// <summary>
    /// Injects a texture cache and hides the GameObject until it is explicitly activated.
    /// </summary>
    /// <param name="textureCache">The cache used to store and retrieve textures.</param>
    public void Initialize(ITextureCache textureCache)
    {
        _cache = textureCache;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows or hides the GameObject. When hidden, the sprite is reset to the default image.
    /// </summary>
    /// <param name="active">True to show; false to hide.</param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        if (_cache != null && _loadOnEnable)
        {
            Load();
            _loadOnEnable = false;
        }
    }

    private void OnDisable()
    {
        // Reset to the default image when deactivated.
        _image.sprite = _defaultImage;
    }

    /// <summary>
    /// Triggers a load operation for the currently stored URL.
    /// If the object is inactive, the request will be queued until enable.
    /// </summary>
    private void Load()
    {
        StopAllCoroutines();

        if (!string.IsNullOrWhiteSpace(_url) && !_cache.HasErrors(_url))
        {
            if (!_cache.IsInCache(_url))
            {
                StartCoroutine(DownloadImage());
            }
            else
            {
                _image.sprite = _cache.GetSprite(_url);
            }
        }
    }

    /// <summary>
    /// Public entry point to request loading a new image.
    /// </summary>
    /// <param name="url">The URL of the image to load.</param>
    public void Load(string url)
    {
        _url = url;
        if (gameObject.activeInHierarchy)
        {
            _loadOnEnable = false;
            Load();
        }
        else
        {
            _loadOnEnable = true;
        }
    }

    /// <summary>
    /// Coroutine that downloads an image from the web, caches it, and updates the UI sprite.
    /// Handles errors by marking the URL in the cache and falling back to the default image.
    /// </summary>
    private IEnumerator DownloadImage()
    {
        _loadOnEnable = false;
        _texture = null;
        using (UnityWebRequest request = new UnityWebRequest(_url, UnityWebRequest.kHttpVerbGET))
        {
            request.downloadHandler = new DownloadHandlerTexture();
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                _image.sprite = _defaultImage;
                _cache.MarkUrlAsError(_url);
                Debug.LogError(request.error);
            }
            else
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        _texture = DownloadHandlerTexture.GetContent(request);

                        if (_texture != null)
                        {
                            _texture.Apply(false, true);
                            _cache.Add(_url, _texture);
                            _image.sprite = _cache.GetSprite(_url);
                        }
                        else
                        {
                            _cache.MarkUrlAsError(_url);
                            Debug.LogError($"[UrlImage] Texture is NULL!!");
                        }
                    }
                    catch (Exception ex)
                    {
                        _cache.MarkUrlAsError(_url);
                        Debug.LogException(ex);
                    }
                }
                else
                {
                    _cache.MarkUrlAsError(_url);
                    Debug.LogError($"[UrlImage] Download failed with error: {request.error}");
                }
            }
            request.Dispose();
        }
    }
}