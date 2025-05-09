using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Start;
    }

    public override void Init(UIManager manager)
    {
        base.Init(manager);
    }
}
