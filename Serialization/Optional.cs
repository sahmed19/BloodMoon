using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodMoon.Serialization
{
    [System.Serializable]
    public struct Optional<T>
    {
        [SerializeField] public bool Enabled;
        [SerializeField] T Value;

        public void Set(T value) => Value = value;

        public Optional(T initialValue)
        {
            Enabled = true;
            Value = initialValue;
        }

        public static implicit operator bool(Optional<T> optional)
        {
            return optional.Enabled;
        }

        public static implicit operator T(Optional<T> optional)
        {
            return optional.Value;
        }
    }
}
