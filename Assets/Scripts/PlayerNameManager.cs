using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    private string playerName;
    public TMP_InputField inputFieldPlayerName;

    public void SetPlayerName()
    {
        GetPlayerName();
        PlayerPrefs.SetString("PlayerName",playerName);
        PlayFabManager.Instance.SubmitNameButton();
    }

    private void GetPlayerName()
    {
        playerName = inputFieldPlayerName.text;
    }
    
}
