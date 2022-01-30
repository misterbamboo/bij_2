using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Player.Scripts.SpecialItems.Items
{
    public class SpeedyArrowItem : SpecialItem
    {
        [SerializeField] public GameObject textPrefab;
        public override int Rarity => 5;
        public override int RespawnDelay => 0;

        [SerializeField] private float BoosterTime;

        private Bow bowInstance;

        [SerializeField] private Collider collider;
        [SerializeField] private Renderer renderer;

        void Start()
        {
            textPrefab = GameObject.FindWithTag("SpeedyArrowItem");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player")
            {
                return;
            }

            BoosterTime += Delay;

            bowInstance = other.GetComponentInChildren<Bow>();
            bowInstance.UseSpeedyArrows = true;

            StartCoroutine(CheckSpecialItemTime());

            collider.enabled = false;
            renderer.enabled = false;
        }

        private IEnumerator CheckSpecialItemTime()
        {
            var timeTextDelay = 2.0f;
            var textMeshPro = textPrefab.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = "+Speedy arrows";
            yield return new WaitForSeconds(timeTextDelay);
            textMeshPro.text = "";
            yield return new WaitForSeconds(BoosterTime - timeTextDelay);

            bowInstance.UseSpeedyArrows = false;
            Destroy(gameObject);
        }
    }
}
