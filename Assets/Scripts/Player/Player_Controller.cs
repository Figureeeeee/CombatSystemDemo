using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : FSMController
{
    public override Enum CurrentState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private Player_Input input;
    private new Player_Audio audio;

    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
    }
}
