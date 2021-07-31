using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    public ToggleGroup difficulty;
    public uint setScore = 10; //Set 10 at default in editor


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayButtonPressed()
    {
        var a = difficulty.ActiveToggles();
        Toggle active = null;

        foreach (var b in a)
        {
            if (b != null)
                active = b;   
        }
        GameData.instance.SetSharedData(new GameData.ShareData(NameToInt(active.name), setScore));

        StartCoroutine(LoadAsyncScene());

    }

    public void SetScoreInput(int s)
    {
        if (s < 0)
        {
            if (setScore != 10)
            {
                setScore -= 10; 
            }
        }
        else
        {
            if (setScore != 90)
            {
                setScore += 10;
            }
        }
        inputField.text = setScore.ToString();
    }

    uint NameToInt(string name)
    {
        uint a = 0;
        switch (name)
        {
            case "Easy":
                a = 0;
                break;
            case "Medium":
                a = 1;
                break;
            case "Hard":
                a = 2;
                break;
            default:
                break;
        }

        return a;
    }

    private IEnumerator LoadAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
