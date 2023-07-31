using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManagerUI : MonoBehaviour
{
    [SerializeField] private Button btn_host;
    [SerializeField] private Button btn_client;
    [SerializeField] private Button btn_server;
    private void Awake()
    {
        btn_host.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Start as Host!");
        });

        btn_client.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Start as Client!");
        });

        btn_server.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            Debug.Log("Start as Server!");
        });
    }
}
