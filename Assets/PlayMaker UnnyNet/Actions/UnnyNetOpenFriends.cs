// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Opens UnnyNet Friends")]
    public class UnnyNetOpenFriends : UnnyNetBaseAction
    {
        public override void OnEnter()
        {
            UnnyNet.UnnyNet.OpenFriends(BaseCallback);
        }
    }
}