using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessModeButton : MonoBehaviour
{
    [SerializeField] Scene _endlessScene;
    public void OnButtonPressed()
    {
        Debug.Log("LOADING SCENE");
        MenuEvents.InvokeModeChosen(gameObject.name);
        Invoke("LoadEndless", 1.5f);
    }
    private void LoadEndless()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
