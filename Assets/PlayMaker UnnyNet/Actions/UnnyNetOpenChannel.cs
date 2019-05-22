// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Opens UnnyNet Channel")]
    public class UnnyNetOpenChannel : UnnyNetBaseAction
    {
        [Tooltip("The channel Name")]
        public FsmString channelName;

        public override void Reset()
        {
            base.Reset();
            channelName = null;
        }

        public override void OnEnter()
        {
            UnnyNet.UnnyNet.OpenChannel(channelName.Value, BaseCallback);
        }
    }
}