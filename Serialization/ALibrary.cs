using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

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
                    mINSTANCE = Resources.LoadAll<T>("")?.First();
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

        [ShowInInspector]
        public T Instance => INSTANCE;
    }
}
