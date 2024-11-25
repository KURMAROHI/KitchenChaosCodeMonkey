using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Player _player;
    private readonly string _isWalking = "IsWalking";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(Animator.StringToHash(_isWalking), _player.IsWalking());
    }
}
