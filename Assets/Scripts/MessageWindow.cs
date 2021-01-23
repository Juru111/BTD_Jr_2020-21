using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.SceneManagement;

public class MessageWindow : MonoBehaviour
{
    [SerializeField]
    private Image blackOut;
    [SerializeField]
    private GameObject window;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text contentText;
    [SerializeField]
    private GameObject contentPanel;
    [SerializeField]
    private GameObject settingsContentPanel;
    [SerializeField]
    private GameObject resetButton;
    [SerializeField]
    private TMP_Text resetButtonText;
    [SerializeField]
    private TMP_Text backButtonText;

    public WindowTypes currWindow { get; private set; }

    //potrzebne dla roboczego restartowania gry
    [SerializeField]
    private GameBox gameBox;

    #region static text strings

    private static string welcomeT = "Welcome chief!";
    private static string welcomeC = "Our monkey village is in danger! Lead the defense for as long as possible!";
    private static string loseT = "All lifes lost!";
    //private static string loseC = "You made it to Round X.\nCan you beat it next time?";
    StringBuilder sb = new StringBuilder();
    private string GiveLoseContent(int roundReached)
    {
        sb.Clear();
        sb.AppendFormat("You made it to Round {0}.\nCan you beat it next time, chief?", roundReached);
        return sb.ToString();
    }
    private static string winT = "Total victory!";
    private static string winC = "Woah, you beat them all!\nYou do not have the title of Chief of our village for nothing.\nGreat job!";
    private static string leaveT = "Leave?";
    private static string leaveC = "Are you sure you want to quit?";
    private static string resetT = "Reset?";
    private static string resetC = "Are you sure you want to go back to start of the Game?";
    private static string settingsT = "Quick Menu";

    private static string ok = "Ok";
    private static string yes = "Yes";
    private static string back = "Back";
    private static string reset = "Reset";
    private static string cancel = "Cancel";
    
    #endregion


    private void Start()
    {
        OpenWindow(WindowTypes.Welcome, 0);
    }


    public void OpenWindow(WindowTypes windowType, int roundReached)
    {
        currWindow = windowType;
        blackOut.enabled = true;
        window.SetActive(true);

        resetButton.SetActive(true);
        contentPanel.SetActive(true);
        settingsContentPanel.SetActive(false);
        switch (windowType)
        {
            case WindowTypes.Welcome:
                titleText.text = welcomeT;
                contentText.text = welcomeC;
                resetButton.SetActive(false);
                backButtonText.text = ok;
                break;
            case WindowTypes.Lose:
                titleText.text = loseT;
                contentText.text = GiveLoseContent(roundReached);
                resetButtonText.text = reset;
                backButtonText.text = back;
                break;
            case WindowTypes.Win:
                titleText.text = winT;
                contentText.text = winC;
                resetButtonText.text = reset;
                backButtonText.text = ok;
                break;
            case WindowTypes.Leave:
                titleText.text = leaveT;
                contentText.text = leaveC;
                resetButtonText.text = yes;
                backButtonText.text = cancel;
                break;
            case WindowTypes.Reset:
                titleText.text = resetT;
                contentText.text = resetC;
                resetButtonText.text = yes;
                backButtonText.text = cancel;
                break;
            case WindowTypes.Settings:
                titleText.text = settingsT;
                contentPanel.SetActive(false);
                settingsContentPanel.SetActive(true);
                resetButtonText.text = reset;
                backButtonText.text = back;
                break;
            default:
                break;
        }
    }

    private void CloseWindow()
    {
        if (currWindow == WindowTypes.Settings)
        {
            PlayerPrefs.SetFloat("MusicVolume", GameBox.instance.soundMenager.GiveMusicVolume());
            PlayerPrefs.SetFloat("SfxVolume", GameBox.instance.soundMenager.GiveSfxVolume());
        }
        blackOut.enabled = false;
        window.SetActive(false);
        currWindow = WindowTypes.NONE;
    }

    public void HandleResetButton()
    {
        switch (currWindow)
        {
            case WindowTypes.Welcome:
            case WindowTypes.Lose:
            case WindowTypes.Win:
            case WindowTypes.Settings:
                OpenWindow(WindowTypes.Reset, 0);
                break;
            case WindowTypes.Reset:
                DoResetGame();
                break;
            case WindowTypes.Leave:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void HandleBackButton()
    {
        CloseWindow();
    }

    private void DoResetGame()
    {
        //fajnie było by ogarnąć wszystkie parametry do początkowych w przyszłości
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public enum WindowTypes
{
    NONE,
    Welcome,
    Lose,
    Win,
    Leave,
    Reset,
    Settings
}