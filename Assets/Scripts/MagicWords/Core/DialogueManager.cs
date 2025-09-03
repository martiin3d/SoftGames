using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Coordinates the execution of a dialogue sequence: loading data, managing avatar visibility, and presenting each line of text.
/// </summary>
public class DialogueManager : IDialogueManager
{
    private readonly IDialogueLoader _loader;
    private readonly IDialogueText _dialogueText;
    private readonly IUrlImage _avatarLeft;
    private readonly IUrlImage _avatarRight;
    private readonly ITextureCache _textureCache;

    private DialogModels _data;
    private MonoBehaviour _coroutineRunner;

    private const string AVATAR_POSITION_RIGHT = "right";

    /// <summary>
    /// Creates a new instance of <see cref="DialogueManager"/>.
    /// </summary>
    /// <param name="loader">Loader that fetches the dialogue JSON.</param>
    /// <param name="dialogueText">Component responsible for displaying text.</param>
    /// <param name="avatarLeft">UI element used to show the left‑hand avatar.</param>
    /// <param name="avatarRight">UI element used to show the right‑hand avatar.</param>
    /// <param name="textureCache">Cache that tracks images load.</param>
    /// <param name="coroutineRunner">MonoBehaviour used to start coroutines.</param>
    public DialogueManager(IDialogueLoader loader, IDialogueText dialogueText, IUrlImage avatarLeft, IUrlImage avatarRight, ITextureCache textureCache, MonoBehaviour coroutineRunner)
    {
        _loader = loader;
        _coroutineRunner = coroutineRunner;
        _dialogueText = dialogueText;
        _avatarLeft = avatarLeft;
        _avatarRight = avatarRight;
        _textureCache = textureCache;
    }

    /// <summary>
    /// Initiates the dialogue sequence. If the data has not yet been loaded, it will be fetched asynchronously. Once ready, a coroutine is started that iterates through each line of dialogue.
    /// </summary>
    /// <param name="onDialogueComplete">Called when all lines have finished displaying.</param>
    /// <param name="onFail">Called if the data could not be loaded or contains no lines.</param>
    public async void StartDialogueSequenceAsync(Action onDialogueComplete, Action onFail)
    {
        if (_data == null)
        {
            _data = await _loader.LoadDialogueAsync();
        }

        if (_data?.dialogue == null || _data.dialogue.Count == 0)
        {
            onFail?.Invoke();
        }
        else
        {
            _coroutineRunner.StartCoroutine(StartDialogueSequenceCoroutine(onDialogueComplete));
        }
    }

    /// <summary>
    /// Coroutine that walks through each dialogue entry, shows the appropriate avatar, and waits for the text component to finish rendering before proceeding.
    /// </summary>
    private IEnumerator StartDialogueSequenceCoroutine(Action onDialogueComplete)
    {
        foreach (var dialog in _data.dialogue)
        {
            ShowAvatar(dialog.name);
            yield return _dialogueText.ShowDialogueCoroutine(dialog);
        }

        Debug.Log("Dialogue sequence finished.");
        onDialogueComplete?.Invoke();
    }

    /// <summary>
    /// Activates the correct avatar UI element based on the speaker’s name. If no matching avatar is found or an error occurred loading its texture, a default placeholder (right avatar) is shown instead.
    /// </summary>
    private void ShowAvatar(string name)
    {
        _avatarLeft.SetActive(false);
        _avatarRight.SetActive(false);

        AvatarData avatar = _data.avatars.Find(x => x.name == name && !_textureCache.HasErrors(x.url));
        if (avatar != null)
        {
            if (avatar.position == AVATAR_POSITION_RIGHT)
            {
                _avatarRight.SetActive(true);
                _avatarRight.Load(avatar.url);
            }
            else
            {
                _avatarLeft.SetActive(true);
                _avatarLeft.Load(avatar.url);
            }
        }
        else
        {
            _avatarRight.SetActive(true);
        }
    }
}