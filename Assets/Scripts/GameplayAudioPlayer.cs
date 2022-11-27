using System.Collections;
using UnityEngine;

public class GameplayAudioPlayer : MonoBehaviour
{
    public Sound[] gameplayMusicSounds;
    public Sound buttonSound;
    void Start()
    {
        AudioManager.Instance.PlaySound(gameplayMusicSounds[Random.Range(0, gameplayMusicSounds.Length)]);
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
