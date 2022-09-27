using System.Collections;
using System.Collections.Generic;
using Agava.IdleGame;
using UnityEngine;

public class UnlockableAssistant : UnlockableObject
{
    public override GameObject Unlock(Transform parent, bool onLoad, string guid)
    {
        return gameObject;
    }
}
