// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Sets to the return rejection message when a new guild request is made, leave to empty or none to allow for creation of this new guild")]
    public class UnnyNetSetNewGuildRejectionMessage : FsmStateAction
    {
        [Tooltip("The error message is any, leave to none for no error")]
        [UIHint(UIHint.Variable)]
        public FsmString rejectionMessage;

        public override void Reset()
        {
            rejectionMessage = null;
        }

        public override void OnEnter()
        {
            PlayMakerUnnyNetProxy.GuildRequestRejectionMessage = rejectionMessage.Value;
        }

    }
}