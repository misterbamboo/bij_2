using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems
{
    public class MoreTimeItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            CountdownTimer.Instance.StartTime += 10.0f;

            Destroy(gameObject);
        }
    }
}
