/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ET.Client.Stage
{
	public partial class FUI_StagePanel: GComponent
	{
		public Controller GameEnd;
		public Transition winAnim;
		public Transition overAnim;
		public GLabel ScoreText;
		public GLabel StepCountText;
		public ET.Client.Common.FUI_ProgressBar2 TimeSlider;
		public ET.Client.Common.FUI_CommonBtn CloseBtn;
		public ET.Client.Common.FUI_CommonBtn RetryBtn;
		public ET.Client.Common.FUI_CommonBtn Close2Btn;
		public GLabel TargetText;
		public GLabel TargetValueText;
		public const string URL = "ui://3m1vqahlu0ri0";

		public static FUI_StagePanel CreateInstance()
		{
			return (FUI_StagePanel)UIPackage.CreateObject("Stage", "StagePanel");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			GameEnd = GetControllerAt(0);
			winAnim = GetTransitionAt(0);
			overAnim = GetTransitionAt(1);
			ScoreText = (GLabel)GetChildAt(3);
			StepCountText = (GLabel)GetChildAt(4);
			TimeSlider = (ET.Client.Common.FUI_ProgressBar2)GetChildAt(5);
			CloseBtn = (ET.Client.Common.FUI_CommonBtn)GetChildAt(6);
			RetryBtn = (ET.Client.Common.FUI_CommonBtn)GetChildAt(9);
			Close2Btn = (ET.Client.Common.FUI_CommonBtn)GetChildAt(10);
			TargetText = (GLabel)GetChildAt(12);
			TargetValueText = (GLabel)GetChildAt(13);
		}
	}
}
