using Agava.IdleGame;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetMovementInput : Action
{
    [SerializeField] private StackView _stackView;
    public SharedAssistantInput SelfAssistantInput;
    public SharedVector2 Direction;

    public override TaskStatus OnUpdate()
    {
        SelfAssistantInput.Value.MovementInput = _stackView.transform.position;
        return TaskStatus.Success;
    }
}
