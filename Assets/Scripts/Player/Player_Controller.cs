using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    // 移动
    Player_Move,
}

public class Player_Controller : FSMController
{
    public override Enum CurrentState { get => playerState; set => playerState = (PlayerState)value; }

    private PlayerState playerState;
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }

    public CharacterController characterController { get; private set; }

    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        characterController = GetComponent<CharacterController>();

        // 默认是移动状态
        ChangeState(PlayerState.Player_Move);
    }
}
