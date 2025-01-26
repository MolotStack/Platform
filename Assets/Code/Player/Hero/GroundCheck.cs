using UnityEditor.UIElements;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded => _isGround;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private Collider2D _checkCollider;

    private bool _isGround;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider == null) return;

        _isGround = _checkCollider.IsTouchingLayers(_groundLayer);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _isGround = false;
    }
}
