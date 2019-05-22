// (c) Copyright HutongGames, LLC 2010-2019. All rights reserved.
// Author jean@hutonggames.com
// This code is licensed under the MIT Open source License

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("UnnyNet")]
    [Tooltip("Report a progress to a UnnyNet achievement Id")]
    public class UnnyNetReportAchievements : UnnyNetBaseAction
    {
        [Tooltip("The achievement Id")]
        public FsmInt achievementId;

        [Tooltip("The progress")]
        public FsmInt progress;

        [Tooltip("The progress as a string, must be a valid int")]
        public FsmString progressAsString;

        [ActionSection("Result")]
        [Tooltip("true if report succeeded")]
        [UIHint(UIHint.Variable)]
        public FsmBool success;

        public override void Reset()
        {
            base.Reset();
            achievementId = null;
            progress = null;
            progressAsString = new FsmString() { UseVariable = true };
            success = null;
        }

        public override void OnEnter()
        {
            int _progress;

            if (!string.IsNullOrEmpty(progressAsString.Value))
            {
                if (!int.TryParse(progressAsString.Value, out _progress))
                {
                    LogError("Progress as string failed to parse as a int");
                }
            }else{
                _progress = progress.Value;
            }

            UnnyNet.UnnyNet.ReportAchievements(achievementId.Value, _progress, BaseCallback);
        }
    }
}