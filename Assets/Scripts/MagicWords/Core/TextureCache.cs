using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In-memory cache for textures and sprites used throughout the game.
/// Provides fast lookup, automatic cleanup on disposal, and a simple error tracking mechanism for failed image loads.
/// </summary>
public class TextureCache : ITextureCache
{
    private Dictionary<string, Texture2D> _cache;
    private List<string> _errorTextures;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureCache"/> class.
    /// Sets up internal collections for cached textures and error URLs.
    /// </summary>
    public TextureCache()
    {
        _errorTextures = new List<string>();
        _cache = new Dictionary<string, Texture2D>();
    }

    /// <summary>
    /// Frees all Unity objects stored in the cache and clears internal data structures.
    /// This method should be called when the cache is no longer needed.
    /// </summary>
    public void Dispose()
    {
        foreach (var texture in _cache.Values)
        {
            UnityEngine.Object.Destroy(texture);
        }

        _errorTextures.Clear();
        _cache.Clear();
        _cache = null;
    }

    /// <summary>
    /// Retrieves a sprite created from the cached texture at the specified URL.
    /// </summary>
    /// <param name="url">The key used when the texture was added to the cache.</param>
    /// <returns>A new <see cref="Sprite"/> instance based on the cached texture.</returns>
    /// </exception>
    public Sprite GetSprite(string url)
    {
        Texture2D texture = _cache[url];
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

        return sprite;
    }

    /// <summary>
    /// Adds a texture to the cache or updates an existing entry.
    /// </summary>
    /// <param name="url">The key under which the texture will be stored.</param>
    /// <param name="texture">The <see cref="Texture2D"/> instance to cache.</param>
    public void Add(string url, Texture2D texture)
    {
        if (!_cache.ContainsKey(url))
        {
            _cache.Add(url, texture);
        }
        else
        {
            _cache[url] = texture;
        }
    }

    /// <summary>
    /// Marks a URL as having encountered an error during loading.
    /// This information can be queried later to avoid repeated attempts.
    /// </summary>
    /// <param name="url">The URL that failed to load.</param>
    public void MarkUrlAsError(string url)
    {
        _errorTextures.Add(url);
    }

    /// <summary>
    /// Determines whether a texture is already present in the cache.
    /// </summary>
    /// <param name="url">The key to look up.</param>
    /// <returns>True if the texture exists; otherwise, false.</returns>
    public bool IsInCache(string url)
    {
        return _cache.ContainsKey(url);
    }

    /// <summary>
    /// Checks whether a given URL has previously been recorded as an error.
    /// </summary>
    /// <param name="url">The URL to check.</param>
    /// <returns>True if the URL is in the error list; otherwise, false.</returns>
    public bool HasErrors(string url)
    {
        return _errorTextures.Contains(url);
    }
}