using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class ThreeArrowsItem : SpecialItem
    {
        public override int Rarity => 5;

        private void OnTriggerEnter(Collider other)
        {
            var otherBow = other.GetComponentInChildren<Bow>();

            otherBow.BoosterTime += 5.0f;

            Destroy(gameObject);
        }
    }
}
