using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float _footStepTimer;
    private float _footStepTimerMax = .1f;

    private void Start()
    {
        _player = transform.GetComponent<Player>();
    }

    private void Update()
    {
        _footStepTimer -= Time.deltaTime;
        Debug.Log("_footStepTimer|" + _footStepTimer + "|" + Time.deltaTime);
        if (_footStepTimer < 0f)
        {
            _footStepTimer = _footStepTimerMax;
            if(_player.IsWalking())
            {
                float volume=1;
                SoundManager.Instance.PlayFootStepSound(transform.position,volume); 
            }
        }
    }
}
