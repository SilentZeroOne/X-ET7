/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ET.Client.Stage
{
	public partial class FUI_StagePanel: GComponent
	{
		public GLabel ScoreText;
		public GLabel StepCountText;
		public ET.Client.Common.FUI_ProgressBar2 TimeSlider;
		public const string URL = "ui://3m1vqahlu0ri0";

		public static FUI_StagePanel CreateInstance()
		{
			return (FUI_StagePanel)UIPackage.CreateObject("Stage", "StagePanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			ScoreText = (GLabel)GetChildAt(3);
			StepCountText = (GLabel)GetChildAt(4);
			TimeSlider = (ET.Client.Common.FUI_ProgressBar2)GetChildAt(5);
		}
	}
}
