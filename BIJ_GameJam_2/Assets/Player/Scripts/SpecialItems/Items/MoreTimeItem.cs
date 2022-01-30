using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class MoreTimeItem : SpecialItem
    {
        public override int Rarity => 1;

        private void OnTriggerEnter(Collider other)
        {
            CountdownTimer.Instance.StartTime += 10.0f;

            Destroy(gameObject);
        }
    }
}
