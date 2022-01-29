using System;
using UnityEngine;

namespace Assets.SharedKernel.Inputs
{
    public interface IPlayerInput
    {
        public float Vertical { get; }

        public float Horizontal { get; }

        public bool Attack { get; }
    }

    public class PlayerInput : IPlayerInput
    {
        private static Lazy<PlayerInput> _lazy = new Lazy<PlayerInput>(() => new PlayerInput());
        public static IPlayerInput Instance => _lazy.Value;

        private PlayerInput()
        {
        }

        public float Vertical => Input.GetAxis("Vertical");

        public float Horizontal => Input.GetAxis("Horizontal");

        public bool Attack => Input.GetAxis("Jump") > 0;
    }
}
