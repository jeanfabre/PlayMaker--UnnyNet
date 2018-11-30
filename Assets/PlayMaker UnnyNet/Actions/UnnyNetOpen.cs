// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Opens UnnyNet")]
    public class UnnyNetOpen : FsmStateAction
    {
        public override void OnEnter()
        {
            UnnyNet.UnnyNet.OpenUnnyNet();
            Finish();
        }
    }
}