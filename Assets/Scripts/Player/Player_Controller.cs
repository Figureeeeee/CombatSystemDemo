using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    // 默认
    Player_None,
    // 移动
    Player_Move,
    // 攻击
    Player_Attack,
}

public class Player_Controller : FSMController<PlayerState>
{
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }

    public CharacterController characterController { get; private set; }

    // 普通攻击配置
    public Conf_SkillData StandAttackConf;

    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        characterController = GetComponent<CharacterController>();

        // 默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    // 检查攻击状态
    public bool CheckAttack()
    {
        return input.GetAttackKeyDown();
    }

    public void StandAttack()
    {
        model.StartAttack(StandAttackConf);
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audio.PlayAudio(audioClip);
    }
}
