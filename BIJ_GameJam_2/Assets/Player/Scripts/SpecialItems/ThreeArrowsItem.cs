using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems
{
    public class ThreeArrowsItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var otherBow = other.GetComponentInChildren<Bow>();

            otherBow.BoosterTime += 5.0f;
        }
    }
}
