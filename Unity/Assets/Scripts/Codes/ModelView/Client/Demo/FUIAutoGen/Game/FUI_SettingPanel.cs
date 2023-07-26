/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ET.Client.Game
{
	public partial class FUI_SettingPanel: GComponent
	{
		public Controller setting;
		public GLabel LanguageText;
		public ET.Client.Common.FUI_ComboBox1 LanguageCombo;
		public GLabel BackgroundText;
		public GLabel SFXText;
		public GSlider BackgroundSlider;
		public GSlider SFXSlider;
		public GLabel BackgroundPect;
		public GLabel SFXPect;
		public const string URL = "ui://3kf0p8qmu0riw";

		public static FUI_SettingPanel CreateInstance()
		{
			return (FUI_SettingPanel)UIPackage.CreateObject("Game", "SettingPanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			setting = GetControllerAt(0);
			LanguageText = (GLabel)GetChildAt(2);
			LanguageCombo = (ET.Client.Common.FUI_ComboBox1)GetChildAt(3);
			BackgroundText = (GLabel)GetChildAt(4);
			SFXText = (GLabel)GetChildAt(5);
			BackgroundSlider = (GSlider)GetChildAt(6);
			SFXSlider = (GSlider)GetChildAt(7);
			BackgroundPect = (GLabel)GetChildAt(8);
			SFXPect = (GLabel)GetChildAt(9);
		}
	}
}
