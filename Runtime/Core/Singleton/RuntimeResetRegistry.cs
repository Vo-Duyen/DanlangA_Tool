using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanlangA.Tool
{
    internal static class RuntimeResetRegistry
    {
        private static readonly HashSet<Action> ResetActions = new HashSet<Action>();

        public static void Register(Action resetAction)
        {
            if (resetAction != null)
            {
                ResetActions.Add(resetAction);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetAll()
        {
            foreach (var action in ResetActions)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }
    }
}
