using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public Sound buttonSound;

    public GameObject rowPrefab;
    public Transform rowsParents;

    private void Start()
    {
        GetLeaderboard();
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

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Leaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet,PlayFabManager.Instance.OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, rowsParents);
            TMP_Text[] texts = newRow.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            if (item.DisplayName!=null)
            {
                texts[1].text = item.DisplayName;
            }
            else
            {
                texts[1].text = "Player";
            }
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

}
