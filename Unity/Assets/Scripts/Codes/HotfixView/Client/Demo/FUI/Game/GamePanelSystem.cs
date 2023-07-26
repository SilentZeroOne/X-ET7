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
			self.FUIGamePanel.setting.SetSelectedIndex(0);
			
			self.FUIGamePanel.StartButton.AddListnerAsync(self.StartGame);
			self.FUIGamePanel.SettingButton.AddListner(self.SettingBtnOnClick);
			var settingPanel = self.FUIGamePanel.SettingPanel;
			
			settingPanel.LanguageCombo.items = new string[] {"简体中文", "繁體中文", "English"};
			settingPanel.LanguageCombo.selectedIndex = PlayerPrefs.GetInt("sweetgame.language", 0);
			settingPanel.LanguageCombo.onChanged.Add(() =>
			{
				self.SetLanguage(settingPanel.LanguageCombo.selectedIndex);
			});

			self.SetLanguage(settingPanel.LanguageCombo.selectedIndex);

			settingPanel.BackgroundSlider.onChanged.Add(() =>
			{
				self.SetBackgroundVolume(settingPanel.BackgroundSlider.value);
			});

			settingPanel.SFXSlider.onChanged.Add(() =>
			{
				self.SetSFXVolume(settingPanel.SFXSlider.value);
			});
			
			settingPanel.BackgroundSlider.value = PlayerPrefs.GetFloat("sweetgame.backgroundVolume", 100);
			self.SetBackgroundVolume(settingPanel.BackgroundSlider.value);
			
			settingPanel.SFXSlider.value = PlayerPrefs.GetFloat("sweetgame.sfxVolume", 100);
			self.SetSFXVolume(settingPanel.SFXSlider.value);
			
			foreach (var config in StageConfigCategory.Instance.GetAll())
			{
				var item = self.FUIGamePanel.StageSelectList.AddItemFromPool();
				item.asButton.title = config.Value.Name;
				item.asButton.AddListnerAsync(self.SwitchStage, config.Value.Name);
			}
		}

		public static void OnHide(this GamePanel self)
		{
			self.FUIGamePanel.StageSelectList.RemoveChildrenToPool();
		}

		public static void BeforeUnload(this GamePanel self)
		{
		}

		public static async ETTask StartGame(this GamePanel self)
		{
			// await SceneChangeHelper.SceneChangeTo(self.DomainScene(), "Stage1");
			//
			// self.DomainScene().GetComponent<FUIComponent>().HidePanel(PanelId.GamePanel);
			// self.DomainScene().GetComponent<FUIComponent>().ShowPanelAsync(PanelId.StagePanel).Coroutine();
			self.FUIGamePanel.setting.SetSelectedIndex(2);//关卡选择界面
			
			await ETTask.CompletedTask;
		}

		private static async ETTask SwitchStage(this GamePanel self, string name)
		{
			await SceneChangeHelper.SceneChangeTo(self.DomainScene(), name);
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

		public static void SetBackgroundVolume(this GamePanel self, double value)
		{
			self.FUIGamePanel.SettingPanel.BackgroundPect.text = Mathf.Floor((float) value) + "%";
			AudioComponent.Instance.SetBackgroundVolume((float) value / 100);
			PlayerPrefs.SetFloat("sweetgame.backgroundVolume", (float) value);
		}

		public static void SetSFXVolume(this GamePanel self,double value)
		{
			self.FUIGamePanel.SettingPanel.SFXPect.text = Mathf.Floor((float) value) + "%";
			AudioComponent.Instance.SetSFXVolume((float) value / 100);
			PlayerPrefs.SetFloat("sweetgame.sfxVolume", (float) value);
		}

		public static void SetLanguage(this GamePanel self,int index)
		{
			switch (index)
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
			self.CurrentLanguageIndex = index;
			PlayerPrefs.SetInt("sweetgame.language", index);
		}
	}
}