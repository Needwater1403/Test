using TMPro;
using Unity.Netcode;
using UnityEngine;


public class ChatManager : NetworkBehaviour
{
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private TMP_Text m_Text;
    public string m_Username;

    public void SendMessage()
    {
        AddTextServerRpc(m_InputField.text);
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddTextServerRpc(string text)
    {
        AddTextClientRpc(text);
    }

    [ClientRpc]
    public void AddTextClientRpc(string text)
    {
        AddText(text);
    }
    void AddText(string chat)
    {
        string lastText = m_Text.text;
        m_Text.SetText(lastText + "\n" + m_Username + ": " + chat);
    }
    public override void OnNetworkDespawn()
    {
        m_Text.SetText("");
        base.OnNetworkDespawn();
    }
}
