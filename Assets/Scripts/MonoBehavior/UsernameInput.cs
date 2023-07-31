using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameInput : MonoBehaviour
{
    [SerializeField] private Button btn_confirm;
    [SerializeField] private TMP_InputField m_InputField;
    [SerializeField] private GameObject chatUI;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject netcodeUI;
    [SerializeField] private ChatManager m_ChatManager;
    private void Awake()
    {
        btn_confirm.onClick.AddListener(() =>
        {
            m_ChatManager.m_Username = m_InputField.text;
            if (m_InputField.text != "")
            {
                chatUI.SetActive(true);
                mainMenu.SetActive(true);
                netcodeUI.SetActive(true);
                this.gameObject.SetActive(false);
            }
        });
    }
}
