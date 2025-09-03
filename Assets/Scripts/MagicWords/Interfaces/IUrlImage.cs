/// <summary>
/// Contract for a component that can load an image from a URL and display it via a Sprite.
/// </summary>
/// 
public interface IUrlImage
{
    /// <summary>
    /// Injects the texture cache used by this component to store or retrieve downloaded textures.
    /// </summary>
    /// <param name="textureCache">The cache instance that will hold loaded textures.</param>
    void Initialize(ITextureCache textureCache);

    /// <summary>
    /// Begins loading an image from the specified URL.
    /// </summary>
    /// <param name="url">The full HTTP/HTTPS path to the image.</param>
    void Load(string url);

    /// <summary>
    /// Shows or hides the visual representation of the image. When set to false, the component may deactivate its GameObject or otherwise make it invisible.
    /// </summary>
    /// <param name="active">If true, the image is visible; if false, it is hidden.</param>
    void SetActive(bool active);
}