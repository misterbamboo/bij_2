using System;
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
        private float AmountOfLove = 0.0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Lover"))
            {
                return;
            }

            var loverMeter = other.GetComponentInChildren<LoveMeter>();
            loverMeter.ModifyLove(AmountOfLove);
        }
    }
}
