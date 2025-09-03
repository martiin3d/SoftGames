using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Loads dialogue data from a remote URL and deserialises it into a <see cref="DialogModels"/> object.
/// </summary>
public class DialogueLoader : IDialogueLoader
{
    private string _url;

    /// <summary>
    /// Creates a new loader that will fetch the dialogue from the supplied URL.
    /// </summary>
    /// <param name="url">The absolute or relative HTTP/HTTPS endpoint hosting the JSON.</param>
    public DialogueLoader(string url)
    {
        _url = url;
    }

    /// <summary>
    /// Asynchronously requests the dialogue file, waits for completion, and returns the deserialized <see cref="DialogModels"/> instance.
    /// If the request fails, a debug error is logged and null is returned.
    /// </summary>
    /// <returns>A task that resolves to the loaded <see cref="DialogModels"/> or null on failure.</returns>
    public async Task<DialogModels> LoadDialogueAsync()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_url))
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading dialogue: " + request.error);
                return null;
            }

            string json = request.downloadHandler.text;
            return JsonUtility.FromJson<DialogModels>(json);
        }
    }
}
