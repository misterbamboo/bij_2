using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class MoreTimeItem : SpecialItem
    {
        [SerializeField] public GameObject textPrefab;
        public override int Rarity => 1;
        public override int RespawnDelay => Delay;

        private bool isPickedUp = false;

        void Start()
        {
            textPrefab = GameObject.FindWithTag("MoreTimeItem");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player" || this.isPickedUp)
            {
                return;
            }
            
            CountdownTimer.Instance.StartTime += Delay;

            StartCoroutine(ApplySpecialItemTime());
        }

        private IEnumerator ApplySpecialItemTime()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.isPickedUp = true;

            var timeTextDelay = 2.0f;
            var textMeshPro = textPrefab.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = "+Time added";

            yield return new WaitForSeconds(timeTextDelay);
            textMeshPro.text = "";

            Destroy(gameObject);
        }
    }
}
