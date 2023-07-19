using ET.Client.Stage;

namespace ET.Client
{
	[ComponentOf(typeof(FUIEntity))]
	public class StagePanel: Entity, IAwake
	{
		private FUI_StagePanel _fuiStagePanel;

		public FUI_StagePanel FUIStagePanel
		{
			get => _fuiStagePanel ??= (FUI_StagePanel)this.GetParent<FUIEntity>().GComponent;
		}
	}
}
