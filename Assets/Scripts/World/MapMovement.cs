using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MapMovement : MonoBehaviour, IPausable
{
    [SerializeField] Animator animator;
    [SerializeField] float speed;
    public static float Speed;

    [RuntimeInitializeOnLoadMethod]
    public static void ReloadDomain() => Speed = 0;

    void Start()
    {
        Speed = speed;
    }

    public void OnPause()
    {
        Speed = 0;
        animator.speed = 0;
    }

    public void OnResume()
    {
        Speed = speed;
        animator.speed = 1;
    }
}
