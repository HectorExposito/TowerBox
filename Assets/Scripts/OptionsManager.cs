using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour
{
    public Sound buttonSound;
    public void ResetTopScore()
    {
        PlayerPrefs.SetInt("TopScore",0);
        AudioManager.Instance.PlaySound(buttonSound);
    }

    public void ChangeSceneAfterButtonSound(string sceneToLoad)
    {
        AudioManager.Instance.PlaySound(buttonSound);
        StartCoroutine(ChangeSceneAfterButtonSoundCoroutine(sceneToLoad));
    }

    private IEnumerator ChangeSceneAfterButtonSoundCoroutine(string sceneToLoad)
    {
        yield return new WaitForSeconds(buttonSound.clip.length);
        GameSceneManager.Instance.ChangeScene(sceneToLoad);
    }
}
