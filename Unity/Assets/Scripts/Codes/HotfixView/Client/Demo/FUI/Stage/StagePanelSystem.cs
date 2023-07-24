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
			self.FUIStagePanel.RetryBtn.AddListner(self.OnRetryBtnClick);
			self.FUIStagePanel.CloseBtn.AddListnerAsync(self.OnBackBtnClick);
			self.FUIStagePanel.Close2Btn.AddListnerAsync(self.OnBackBtnClick);
		}

		public static void OnShow(this StagePanel self, Entity contextData = null)
		{
			var config = StageConfigCategory.Instance.GetByName(self.DomainScene().CurrentScene().Name);
			self.FUIStagePanel.GameEnd.SetSelectedIndex(0);
			self.SetTurn(config.Turn);
			self.FUIStagePanel.TimeSlider.max = config.HoldTime;
			self.SetTimeSlider(config.HoldTime);
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

		public static void SetController(this StagePanel self, GameStatus gameStatus)
		{
			if (gameStatus == GameStatus.Win && self.FUIStagePanel.GameEnd.selectedIndex!=2)
			{
				self.FUIStagePanel.GameEnd.SetSelectedIndex(2);
				self.FUIStagePanel.winAnim.Play();
			}
			else if (gameStatus == GameStatus.Over&& self.FUIStagePanel.GameEnd.selectedIndex!=1)
			{
				self.FUIStagePanel.GameEnd.SetSelectedIndex(1);
				self.FUIStagePanel.overAnim.Play();
			}
		}

		public static void OnRetryBtnClick(this StagePanel self)
		{
			var currentScene = self.DomainScene().CurrentScene();
			var sweetStage = currentScene.GetComponent<SweetStageComponent>();
			sweetStage.Restart(currentScene.Name);
			self.SetScore(0);
			
			self.FUIStagePanel.GameEnd.SetSelectedIndex(0);
		}

		public static async ETTask OnBackBtnClick(this StagePanel self)
		{
			await SceneChangeHelper.SceneChangeTo(self.DomainScene(), "Bootstrap");
			self.DomainScene().GetComponent<FUIComponent>().ShowPanelAsync(PanelId.GamePanel).Coroutine();
			self.DomainScene().GetComponent<FUIComponent>().HidePanel(PanelId.StagePanel);
		}
	}
}