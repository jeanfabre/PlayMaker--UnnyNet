// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a score to a UnnyNet LeaderBoard")]
    [Title("Send Message")]
    public class UnnyNetSendMessageToChannel : FsmStateAction
    {
        [Tooltip("The channel Name")]
        public FsmString channelName;

        [Tooltip("The message")]
        public FsmString message;

        [ActionSection("Result")]
        [Tooltip("true if message succeeded")]
        [UIHint(UIHint.Variable)]
        public FsmBool success;

        [Tooltip("The error message if message failed")]
        [UIHint(UIHint.Variable)]
        public FsmString errorMessage;

        [Tooltip("event sent if message failed")]
        public FsmEvent successEvent;

        [Tooltip("event sent if message succeeded")]
        public FsmEvent errorEvent;

        bool _success;

        public override void Reset()
        {
            channelName = null;
            message = null;
            success = null;
            errorMessage = null;
            errorEvent = null;
            successEvent = null;
        }

        public override void OnEnter()
        {
            UnnyNet.UnnyNet.SendMessageToChannel(channelName.Value, message.Value, HandleSendMessage);
        }

        void HandleSendMessage(string error)
        {
            if (!errorMessage.IsNone)
            {
                errorMessage.Value = error;
            }

            _success = string.IsNullOrEmpty(error);

            if (!success.IsNone)
            {
                success.Value = _success;
            }

            if (_success)
            {
                if (successEvent!=null)
                {
                    Fsm.Event(successEvent);
                }
            }else{
                if (errorEvent != null)
                {
                    Fsm.Event(errorEvent);
                }
            }

            Finish();
        }
    }
}