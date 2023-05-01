using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndLevel : MonoBehaviour
{
    private AudioSource _audio;

    private void Start() {
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Player>(out Player player)) {
            _audio.Play();
        }
    }
}
