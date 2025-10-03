using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _prepareJumpClip;
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioSource _chargeSource;

    public AudioClip PrepareJumpClip => _prepareJumpClip;
    public AudioClip JumpClip => _jumpClip;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    public void PlayCharge(bool value)
    {
        if (value)
            _chargeSource.Play();
        else
            _chargeSource.Stop();
    }
}