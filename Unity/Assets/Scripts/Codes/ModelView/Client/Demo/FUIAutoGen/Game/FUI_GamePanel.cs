/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ET.Client.Game
{
	public partial class FUI_GamePanel: GComponent
	{
		public Controller setting;
		public ET.Client.Common.FUI_CommonBtn StartButton;
		public ET.Client.Common.FUI_CommonBtn SettingButton;
		public ET.Client.Game.FUI_SettingPanel SettingPanel;
		public const string URL = "ui://3kf0p8qmu0ri0";

		public static FUI_GamePanel CreateInstance()
		{
			return (FUI_GamePanel)UIPackage.CreateObject("Game", "GamePanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			setting = GetControllerAt(0);
			StartButton = (ET.Client.Common.FUI_CommonBtn)GetChildAt(8);
			SettingButton = (ET.Client.Common.FUI_CommonBtn)GetChildAt(9);
			SettingPanel = (ET.Client.Game.FUI_SettingPanel)GetChildAt(10);
		}
	}
}
