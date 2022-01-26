using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LionStudios.Suite.Analytics;
using GameAnalyticsSDK;
using LionStudios.Suite.Debugging;
public class LoadLevel : MonoBehaviour
{
    public GameObject loadingScreen;
   
    public Text loadText;
    public string sceneToLoad;


    public int currentSavedLevelNumber;
  //  public CanvasGroup canvasGroup;

    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
      //  LionDebugger.Hide();

        Scene sceneLoaded = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(sceneLoaded.buildIndex);
        // loadingOperation = SceneManager.LoadSceneAsync(sceneLoaded.buildIndex);

        if (sceneLoaded.buildIndex == 0)
        {
            currentSavedLevelNumber = PlayerPrefs.GetInt("SavedLevelNumber");

            if (currentSavedLevelNumber == 0)
            {
                currentSavedLevelNumber = 1;
            }
            else
            {

            }

            StartCoroutine(StartLoad());
        }
        else
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PlayerController.playerController.levelNumber.ToString()); //GameAnal
        }


        

    }


    public void ReloadLevel()
    {

        Scene sceneLoaded = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneLoaded.buildIndex);



        PlayerPrefs.SetInt("Attempt", PlayerController.playerController.attemptNum + 1);
        PlayerController.playerController.attemptNum = PlayerPrefs.GetInt("Attempt");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PlayerController.playerController.levelNumber.ToString()); //GameAnal
                                                                                                                                   // score = (int)curProgress;
        LionAnalytics.LevelRestart(PlayerController.playerController.levelNumber, PlayerController.playerController.attemptNum, PlayerController.playerController.score); //lionAnal

       

       

    }


    public void NextLevel()
    {
      
        PlayerPrefs.SetInt("levelNumber", PlayerController.playerController.levelNumber + 1);
        PlayerController.playerController.levelNumber = PlayerPrefs.GetInt("levelNumber");
      //  Debug.Log("Next Level Number " + PlayerController.playerController.levelNumber);

        if (PlayerController.playerController.levelNumber <= 8)
        {
            SceneManager.LoadScene(PlayerController.playerController.levelNumber);
        }
        else
        {
            int nextLevel = Random.Range(1, 7);
            SceneManager.LoadScene(nextLevel);
         //   Debug.Log("Rnd Next Level Number " + nextLevel);
        }

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, PlayerController.playerController.levelNumber.ToString()); //GameAnal
                                                                                                                                   // score = (int)curProgress;
        LionAnalytics.LevelComplete(PlayerController.playerController.levelNumber, PlayerController.playerController.attemptNum, PlayerController.playerController.score); //lionAnal

        PlayerPrefs.SetInt("Attempt", 0);
    }


    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(currentSavedLevelNumber);
        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = 0;
        float time = 0;
        
        


        while (time < duration)
        {
            loadingScreen.GetComponent<Image>().fillAmount = Mathf.Lerp(startValue, targetValue, time / duration);
            //  Image panelImage = gameObject.GetComponent<Image>();
            // panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, loadingScreen.GetComponent<Slider>().value);
            //  gameObject.GetComponent<Image>().color = panelImage.color;
            // int loadValue  = (int)loadingScreen.GetComponent<Image>().fillAmount * 100; /// To Converted float Value to Int 
            // loadText.text = loadingScreen.GetComponent<Image>().fillAmount * 100 + "%";
            loadText.text = Mathf.Floor( loadingScreen.GetComponent<Image>().fillAmount * 101) + "%";
            // Debug.Log("Load Value " + loadValue);
            time += Time.deltaTime;
            yield return null;
        }
        loadingScreen.GetComponent<Image>().fillAmount = targetValue;
     //   canvasGroup.alpha = targetValue;
    }


   
    // Update is called once per frame
    void Update()
    {
      
    }
}
