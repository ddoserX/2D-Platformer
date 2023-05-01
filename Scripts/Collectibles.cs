using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Player>(out Player player)) {
            gameObject.SetActive(false);
        }
    }
}
