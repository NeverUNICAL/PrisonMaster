using System;
using BehaviorDesigner.Runtime;

[Serializable]
public class SharedAssistantInput : SharedVariable<AssistantInput>
{
    public static implicit operator SharedAssistantInput(AssistantInput value) => new SharedAssistantInput { Value = value};

}
