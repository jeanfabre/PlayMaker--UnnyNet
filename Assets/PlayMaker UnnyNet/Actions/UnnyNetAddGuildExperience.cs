// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Adds to the local Player Guild Experience")]
    public class UnnyNetAddGuildExperience : FsmStateAction
    {
        [Tooltip("The experience value")]
        public FsmInt experience;

        [Tooltip("The progress as a string, must be a valid int")]
        public FsmString experienceAsString;

        [Tooltip("The error message is any")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("Event sent if there is an error callback, likely because the window is already opened")]
        public FsmEvent errorEvent;

        [Tooltip("Event sent if tthe window opened")]
        public FsmEvent successEvent;


        int _experience;

        public override void Reset()
        {
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
            experience = 500;
        }

        public override void OnEnter()
        {
            if (!string.IsNullOrEmpty(experienceAsString.Value))
            {
                if (!int.TryParse(experienceAsString.Value, out _experience))
                {
                    LogError("Experience as string failed to parse as a int");
                }
            }
            else
            {
                _experience = experience.Value;
            }

            UnnyNet.UnnyNet.AddGuildExperience(_experience, AddGuildCallback);
        }

        void AddGuildCallback(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                errorMessage.Value = string.Empty;

                Fsm.Event(successEvent);
            }
            else
            {
                errorMessage.Value = error;
                Fsm.Event(errorEvent);
            }

            Finish();
        }
    }
}