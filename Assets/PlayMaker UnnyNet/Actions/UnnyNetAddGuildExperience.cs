// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Adds to the local Player Guild Experience")]
    public class UnnyNetAddGuildExperience : UnnyNetBaseAction
    {
        [Tooltip("The experience value")]
        public FsmInt experience;

        [Tooltip("The progress as a string, must be a valid int")]
        public FsmString experienceAsString;

        public override void Reset()
        {
            base.Reset();
            experience = 500;
        }

        public override void OnEnter()
        {
            int _experience;

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

            UnnyNet.UnnyNet.AddGuildExperience(_experience, BaseCallback);
        }
    }
}