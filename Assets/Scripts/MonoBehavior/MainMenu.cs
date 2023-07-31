using CortexDeveloper.ECSMessages.Service;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : NetworkBehaviour
{
    private EntityManager _entityManager;
    [SerializeField] private TextMeshProUGUI txt_score1;
    [SerializeField] private TextMeshProUGUI txt_score2;
    [SerializeField] private TextMeshProUGUI txt_gameState;
    [SerializeField] private GameObject btn_Start;
    [SerializeField] private GameObject btn_Back;
    [SerializeField] private GameObject netcodeUI;
    private Entity _scoreBoard;
    private Entity _winStatus;

    private void Start()
    {
 
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _scoreBoard = _entityManager.CreateEntityQuery(typeof(ScoreComponent)).GetSingletonEntity();
        _winStatus = _entityManager.CreateEntityQuery(typeof(WinStatus)).GetSingletonEntity();
    }
    void Update()
    {

        var score1 = _entityManager.GetComponentData<ScoreComponent>(_scoreBoard).p1_Score;
        var score2 = _entityManager.GetComponentData<ScoreComponent>(_scoreBoard).p2_Score;
        var status = _entityManager.GetComponentData<WinStatus>(_winStatus).status;
        txt_score1.text = "Player 1: " + score1.ToString();
        txt_score2.text = "Player 2: " + score2.ToString();      
        switch(status)
        {
            case 0:
                {
                    txt_gameState.text = "Player 2 Win!!";
                    break;
                }
            case 2:
                {
                    txt_gameState.text = "Player 1 Win!!";
                    break;
                }
            case 1:
                {
                    txt_gameState.text = "Tie!!";
                    break;
                }
            default:
                {
                    txt_gameState.text = "";
                    break;
                }
        }
    }
    public void StartGame()
    { 
        txt_score1.gameObject.SetActive(true);
        txt_score2.gameObject.SetActive(true);
        txt_gameState.gameObject.SetActive(true);
        btn_Start.gameObject.SetActive(false);
        btn_Back.gameObject.SetActive(true);
        netcodeUI.SetActive(false);
        MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(_entityManager, new StartCommand
        {
            startGame = true,
        });
    }
    public void test()
    {
        txt_score1.gameObject.SetActive(true);
        txt_score2.gameObject.SetActive(true);
        btn_Start.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        txt_score1.gameObject.SetActive(false);
        txt_score2.gameObject.SetActive(false);
        txt_gameState.gameObject.SetActive(false);
        btn_Start.gameObject.SetActive(true);
        btn_Back.gameObject.SetActive(false);
        netcodeUI.SetActive(false);
        MessageBroadcaster.PrepareMessage().AliveForOneFrame().PostImmediate(_entityManager, new ResetCommand
        {
            resetGame = true,
        });
    }
}
