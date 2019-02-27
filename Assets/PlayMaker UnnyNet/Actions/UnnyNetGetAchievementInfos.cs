// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Get Completed Achievement Infos. Requires PlayMakerUnnyNetProxy component to be in the scene, listen to 'UNNYNET / ACHIEVEMENT / ON COMPLETED' before using this action")]
    public class UnnyNetGetAchievementInfos : FsmStateAction
    {
        [Tooltip("The Achievement Id")]
        [UIHint(UIHint.Variable)]
        public FsmInt achievementId;

        public override void Reset()
        {
            achievementId = null;
        }

        public override void OnEnter()
        {
            if (!achievementId.IsNone)
            {
                achievementId.Value = PlayMakerUnnyNetProxy.AchievementId;
            }

            Finish();
        }
    }
}