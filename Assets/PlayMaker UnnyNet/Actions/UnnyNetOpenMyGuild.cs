// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Opens UnnyNet guild of the local player. The Guilds window is opened if the player isn't in a guild")]
    public class UnnyNetOpenMyGuild : UnnyNetBaseAction
    {
        public override void OnEnter()
        {
            UnnyNet.UnnyNet.OpenMyGuild(BaseCallback);
        }
    }
}