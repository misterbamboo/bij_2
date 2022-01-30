using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.BowAndArrow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float Duration = 5.0f;

        [SerializeField]
        private float AmountOfLove = 0.0f;

        private void Awake()
        {
            StartCoroutine(DestroyTimer());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Lover"))
            {
                return;
            }

            GameManager.Instance.GameEvent( GameProgression.GameEvents.LoverHitByArrow);

            var loverMeter = other.GetComponentInChildren<LoveMeter>();
            loverMeter.ModifyLove(AmountOfLove);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(Duration);
            Destroy(gameObject);
        }
    }
}
