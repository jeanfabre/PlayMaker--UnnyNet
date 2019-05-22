// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a score to a UnnyNet LeaderBoard")]
    public class UnnyNetSendMessageToChannel : UnnyNetBaseAction
    {
        [Tooltip("The channel Name")]
        public FsmString channelName;

        [Tooltip("The message")]
        public FsmString message;

        [ActionSection("Result")]
        [Tooltip("true if message succeeded")]
        [UIHint(UIHint.Variable)]
        public FsmBool success;

        public override void Reset()
        {
            base.Reset();
            channelName = null;
            message = null;
            success = null;
        }

        public override void OnEnter()
        {
            UnnyNet.UnnyNet.SendMessageToChannel(channelName.Value, message.Value, BaseCallback);
        }
    }
}