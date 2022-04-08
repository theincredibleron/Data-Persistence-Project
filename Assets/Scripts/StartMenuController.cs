using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class StartMenuController : MonoBehaviour
{
    [SerializeField] InputField m_playerNameInput;
    // Start is called before the first frame update

    void Awake() {
        if (GameManager.Instance != null)
            m_playerNameInput.text = GameManager.Instance.PlayerName;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SetPlayerName()
    {
        GameManager.Instance.PlayerName = m_playerNameInput.text;
    }

    public void StartGame()
    {
        SetPlayerName();
        SceneManager.LoadScene(1);
    }
}
