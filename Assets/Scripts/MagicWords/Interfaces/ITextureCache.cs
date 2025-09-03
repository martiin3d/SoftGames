using UnityEngine;

public interface ITextureCache
{
    /// <summary>
    /// Stores the given texture under the specified URL key.
    /// </summary>
    /// <param name="url">The source URL used as the cache key.</param>
    /// <param name="texture">The texture to be cached.</param>
    void Add(string url, Texture2D texture);

    /// <summary>
    /// Releases all resources held by the cache.
    /// </summary>
    void Dispose();

    /// <summary>
    /// Retrieves a sprite created from the texture previously cached with the given URL.
    /// </summary>
    /// <param name="url">The URL key used when adding the texture.</param>
    /// <returns>The sprite corresponding to the cached texture.</returns>
    Sprite GetSprite(string url);

    /// <summary>
    /// Indicates whether a previous load attempt for the specified URL failed and was recorded as an error. Implementations typically store this flag so that subsequent requests can be skipped or handled differently.
    /// </summary>
    /// <param name="url">The URL to check.</param>
    /// <returns>True if the URL has been marked as having errors; otherwise false.</returns>
    bool HasErrors(string url);

    /// <summary>
    /// Checks whether a texture for the given URL is already present in the cache.
    /// </summary>
    /// <param name="url">The URL key to query.</param>
    /// <returns>True if the texture exists; otherwise false.</returns>
    bool IsInCache(string url);

    /// <summary>
    /// Marks a specific URL as having failed during loading.
    /// </summary>
    /// <param name="url">The URL that encountered an error.</param>
    void MarkUrlAsError(string url);
}