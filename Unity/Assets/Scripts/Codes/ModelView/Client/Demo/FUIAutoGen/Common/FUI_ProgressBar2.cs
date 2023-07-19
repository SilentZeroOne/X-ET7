/** This is an automatically generated class by FUICodeSpawner. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ET.Client.Common
{
	public partial class FUI_ProgressBar2: GProgressBar
	{
		public GTextField title;
		public const string URL = "ui://f2boiu4iu0rix";

		public static FUI_ProgressBar2 CreateInstance()
		{
			return (FUI_ProgressBar2)UIPackage.CreateObject("Common", "ProgressBar2");
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			title = (GTextField)GetChildAt(2);
		}
	}
}
