using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    public class UnnyNetBaseAction : FsmStateAction
    {
        [Tooltip("The error message if any")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("Event sent if there is an error callback, likely because the window is already opened")]
        public FsmEvent errorEvent;

        [Tooltip("Event sent if the window opened")]
        public FsmEvent successEvent;

        public override void Reset()
        {
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
        }

        protected void BaseCallback(UnnyNet.ResponseData response)
        {
            if (response == null || response.Success)
            {
                if (!errorMessage.IsNone)
                    errorMessage.Value = string.Empty;

                if (successEvent != null)
                    Fsm.Event(successEvent);
            }
            else
            {
                if (!errorMessage.IsNone)
                    errorMessage.Value = response.Error != null ? response.Error.Message : string.Empty;

                if (errorEvent != null)
                    Fsm.Event(errorEvent);
            }

            Finish();
        }
    }
}