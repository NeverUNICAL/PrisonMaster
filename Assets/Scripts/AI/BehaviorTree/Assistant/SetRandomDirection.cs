using Agava.IdleGame;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetRandomDirection : Action
{
    [SerializeField] private StackView _stackView;
    public SharedVector2 Direction;

    public override TaskStatus OnUpdate()
    {
        Direction.Value = _stackView.transform.position;
        return TaskStatus.Success;
    }
}
