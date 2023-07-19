using UnityEngine;

namespace ET.Client
{
	[FriendOf(typeof(GamePanel))]
	public static class GamePanelSystem
	{
		public static void Awake(this GamePanel self)
		{
		}

		public static void RegisterUIEvent(this GamePanel self)
		{
		}

		public static void OnShow(this GamePanel self, Entity contextData = null)
		{
			self.FUIGamePanel.StartButton.AddListnerAsync(self.StartGame);
			self.FUIGamePanel.SettingButton.AddListner(self.SettingBtnOnClick);
			self.FUIGamePanel.SettingPanel.LanguageCombo.items = new string[] {"简体中文", "繁體中文", "English"};
			self.FUIGamePanel.SettingPanel.LanguageCombo.selectedIndex = 0;
			self.FUIGamePanel.SettingPanel.LanguageCombo.onChanged.Add(() =>
			{
				switch (self.FUIGamePanel.SettingPanel.LanguageCombo.selectedIndex)
				{
					case 0:
						self.SwitchLanguage(SystemLanguage.ChineseSimplified);
						break;
					
					case 1:
						self.SwitchLanguage(SystemLanguage.ChineseTraditional);
						break;
					
					case 2:
						self.SwitchLanguage(SystemLanguage.English);
						break;
				}
			});
		}

		public static void OnHide(this GamePanel self)
		{
		}

		public static void BeforeUnload(this GamePanel self)
		{
		}

		public static async ETTask StartGame(this GamePanel self)
		{
			await SceneChangeHelper.SceneChangeTo(self.DomainScene(), "Stage1");

			self.DomainScene().GetComponent<FUIComponent>().HidePanel(PanelId.GamePanel);
			self.DomainScene().GetComponent<FUIComponent>().ShowPanelAsync(PanelId.StagePanel).Coroutine();
		}

		public static void SettingBtnOnClick(this GamePanel self)
		{
			self.FUIGamePanel.setting.selectedIndex = self.CurrentControlerIndex == 1? 0 : 1;
			self.CurrentControlerIndex = self.FUIGamePanel.setting.selectedIndex;
		}

		public static void SwitchLanguage(this GamePanel self, SystemLanguage language)
		{
			self.ClientScene().GetComponent<LocalizeComponent>().SwitchLanguage(language);
		}
	}
}