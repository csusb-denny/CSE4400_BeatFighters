using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject InstructionsPanel;

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Instructions()
    {
        InstructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        InstructionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game pressed.");
    }
}
