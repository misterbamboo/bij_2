using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class MoreTimeItem : SpecialItem
    {
        public override int Rarity => 1;
        public override int RespawnDelay => Delay;

        private void OnTriggerEnter(Collider other)
        {
            CountdownTimer.Instance.StartTime += Delay;

            Destroy(gameObject);
        }
    }
}
