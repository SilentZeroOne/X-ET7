using DG.Tweening;
using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(StagePanel))]
	public static class StagePanelSystem
	{
		public static void Awake(this StagePanel self)
		{
		}

		public static void RegisterUIEvent(this StagePanel self)
		{
		}

		public static void OnShow(this StagePanel self, Entity contextData = null)
		{
			self.SetTurn(StageConfigCategory.Instance.GetByName(self.DomainScene().CurrentScene().Name).Turn);
			self.SetScore(0);
		}

		public static void OnHide(this StagePanel self)
		{
		}

		public static void BeforeUnload(this StagePanel self)
		{
		}

		public static void SetScore(this StagePanel self,long score)
		{
			long startValue = long.Parse(self.FUIStagePanel.ScoreText.text);
			DOTween.To(() => startValue, x => { self.FUIStagePanel.ScoreText.text = Mathf.Floor(x).ToString(); }, score, 0.3f).SetEase(Ease.Linear).SetUpdate(true);
			//self.FUIStagePanel.ScoreText.text = score.ToString();
		}

		public static void SetTurn(this StagePanel self, int currentTurn)
		{
			self.FUIStagePanel.StepCountText.text = currentTurn.ToString();
		}

		public static void SetTimeSlider(this StagePanel self, int time)
		{
			self.FUIStagePanel.TimeSlider.value = time;
		}
	}
}