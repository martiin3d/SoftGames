using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Bootstrap component for the main menu that wires up button clicks to scene transitions.
/// </summary>
public class MainMenuBootstrap : MonoBehaviour
{
    [SerializeField] private Button _aceOfShadowsButton;
    [SerializeField] private Button _magicWordsButton;
    [SerializeField] private Button _phoenixFlameButton;

    private const string SCENE_ACE_OF_SHADOWS = "AceOfShadowsScene";
    private const string SCENE_MAGIC_WORDS = "MagicWordsScene";
    private const string SCENE_PHOENIX_FLAME = "PhoenixFlameScene";

    private void OnEnable()
    {
        _aceOfShadowsButton.onClick.AddListener(OnAceOfShadowsButtonClick);
        _magicWordsButton.onClick.AddListener(OnMagicWordsButtonClick);
        _phoenixFlameButton.onClick.AddListener(OnPhoenixFlameButtonClick);
    }

    private void OnDisable()
    {
        _aceOfShadowsButton.onClick.RemoveListener(OnAceOfShadowsButtonClick);
        _magicWordsButton.onClick.RemoveListener(OnMagicWordsButtonClick);
        _phoenixFlameButton.onClick.RemoveListener(OnPhoenixFlameButtonClick);
    }

    private void OnAceOfShadowsButtonClick()
    {
        SceneManager.LoadScene(SCENE_ACE_OF_SHADOWS);
    }

    private void OnMagicWordsButtonClick()
    {
        SceneManager.LoadScene(SCENE_MAGIC_WORDS);
    }

    private void OnPhoenixFlameButtonClick()
    {
        SceneManager.LoadScene(SCENE_PHOENIX_FLAME);
    }
}
