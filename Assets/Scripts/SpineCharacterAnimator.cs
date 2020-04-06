using System;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(RigidbodyCharacterController))]
public class SpineCharacterAnimator : MonoBehaviour
{
  #region Inspector

  [SerializeField]
  private SkeletonAnimation spine = default;

  #endregion


  #region Fields

  private RigidbodyCharacterController _characterController;

  private Direction _currentDirection = Direction.S;

  private bool _currentIsWalking = false;

  private HandAction _currentAction = HandAction.None;

  #endregion


  #region MonoBehaviour

  private void Awake ()
  {
    _characterController = GetComponent<RigidbodyCharacterController>();
  }

  private void Start ()
  {
    spine.AnimationState.SetAnimation(0, "Idle/S", true);
  }

  private void Update ()
  {
    if ( Input.GetKeyDown(KeyCode.A) )
    {
      _currentAction = _currentAction == HandAction.None
        ? HandAction.Hold
        : HandAction.None;
      UpdateUpper();
    }

    if ( _currentAction == HandAction.Hold && Input.GetKeyDown(KeyCode.Space) )
    {
      spine.AnimationState.SetAnimation(
        1, $"Hit/{DirectionUtil.GetActualName(_currentDirection)}",
        false);
      spine.AnimationState.AddAnimation(
        1, $"Hold/{DirectionUtil.GetActualName(_currentDirection)}", true,
        0);
      return;
    }

    bool isWalking = Math.Abs(_characterController.Magnitude) > 0;

    Direction direction =
      DirectionUtil.ConvertDegrees(_characterController.AngleDeg);
    if ( isWalking && direction != _currentDirection )
    {
      _currentDirection = direction;
      spine.Skeleton.ScaleX = DirectionUtil.IsFlippable(direction) ? -1 : 1;
      UpdateBase();
      UpdateUpper();
    }

    if ( isWalking != _currentIsWalking )
    {
      _currentIsWalking = isWalking;
      UpdateBase();
    }
  }

  private void UpdateBase ()
  {
    string actionName = _currentIsWalking ? "Walk" : "Idle";
    string directionName = DirectionUtil.GetActualName(_currentDirection);
    string animName = $"{actionName}/{directionName}";
    spine.AnimationState.SetAnimation(0, animName, true);
  }

  private void UpdateUpper ()
  {
    switch ( _currentAction )
    {
      case HandAction.None:
        spine.AnimationState.SetEmptyAnimation(1, 0.2f);
        break;
      case HandAction.Hold:
        spine.AnimationState.SetAnimation(
          1, $"Hold/{DirectionUtil.GetActualName(_currentDirection)}",
          true);
        break;
    }
  }

  #endregion
}