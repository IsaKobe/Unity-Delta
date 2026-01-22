using UnityEngine;

public class FloorCollider : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<newInput>(out newInput input))
        {
            input.canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<newInput>(out newInput input))
        {
            input.canJump = false;
        }
    }
}
