using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrivaMapTeleporter : MonoBehaviour
{
    public GameObject Here;
    public int sceneBuildIndex;
    public int sceneBuildIndex2;
    public GameObject ArrivaMap;
    public GameObject LoadingScreen;
    public float delay;

    // Method called when the Arriva button is clicked
    public void ArrivaClickedHere()
    {
        if (Here != null)
        {
            Here.SetActive(true);
        }
    }

    // Method for switching to the fitness scene with a loading screen delay
    public void FitnesClickedHere()
    {
        Time.timeScale = 1f;
        LoadingScreen.SetActive(true);
        StartCoroutine(NextLevelAfterWait());
    }
    public void SckrClickedHere() {
        Time.timeScale = 1f;
        LoadingScreen.SetActive(true);
        StartCoroutine(NextLevelAfterWait2());
    }
    IEnumerator NextLevelAfterWait()
    {
        yield return new WaitForSeconds(delay);

        print("Switching Scene to " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        ArrivaMap.SetActive(false);
    }
    IEnumerator NextLevelAfterWait2()
    {
        yield return new WaitForSeconds(delay);

        print("Switching Scene to " + sceneBuildIndex2);
        SceneManager.LoadScene(sceneBuildIndex2, LoadSceneMode.Single);
        ArrivaMap.SetActive(false);
    }



}
