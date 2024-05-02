using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BloodMoon.Serialization
{
    public abstract class ALibrary<T> : ScriptableObject
        where T : ALibrary<T>
    {
        static T mINSTANCE;
        static T INSTANCE
        {
            get
            {
                if (!mINSTANCE)
                {
                    mINSTANCE = Resources.Load<T>("Libraries/" + GetProperFileName());
                }
                return mINSTANCE;
            }
        }

        public static T GetInstance()
        {
            if (INSTANCE == null)
                throw new ApplicationException("Couldn't find Library of type " + typeof(T).Name +
                                               ". Please look in the Resources/Libraries directory.");
            return INSTANCE;
        }

        [InfoBox("This file MUST be placed in a Resources/Libraries directory")]
        [ShowInInspector]
        public T Instance => INSTANCE;
        static string GetProperFileName()
        {
            return "LIB_" + typeof(T).Name;
        }
    }
}
