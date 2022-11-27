using UnityEngine;
using TMPro;
using System.Collections;
public class MenuManager : MonoBehaviour
{
    public TMP_Text topScoreText;

    public Sound menuSound;
    public Sound buttonSound;
    private void Start()
    {
        topScoreText.text = PlayerPrefs.GetInt("TopScore").ToString();

        AudioManager.Instance.PlaySound(menuSound);
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
