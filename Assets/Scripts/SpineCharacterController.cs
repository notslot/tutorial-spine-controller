using Spine.Unity;
using UnityEngine;

public class SpineCharacterController : MonoBehaviour
{
  #region Inspector

  [SerializeField]
  private SkeletonAnimation spine = default;

  [SerializeField]
  [Min(1)]
  private float speed = 2;

  #endregion


  #region Fields

  private Rigidbody _physics;

  private Vector3 _velocity;

  #endregion


  #region MonoBehaviour

  private void Awake ()
  {
    _physics = GetComponent<Rigidbody>();
  }

  private void Update ()
  {
    UpdateVelocity();
  }

  private void FixedUpdate ()
  {
    _physics.velocity = _velocity;
  }

  #endregion


  #region Methods

  private void UpdateVelocity ()
  {
    int x = 0, z = 0;

    if ( Input.GetKey(KeyCode.RightArrow) )
      x += 1;
    if ( Input.GetKey(KeyCode.LeftArrow) )
      x -= 1;

    if ( Input.GetKey(KeyCode.UpArrow) )
      z += 1;
    if ( Input.GetKey(KeyCode.DownArrow) )
      z -= 1;

    _velocity = new Vector3(x, 0, z).normalized * speed;
  }

  #endregion
}