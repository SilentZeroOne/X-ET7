using ET.Client.Game;

namespace ET.Client
{
	[ComponentOf(typeof(FUIEntity))]
	public class GamePanel: Entity, IAwake
	{
		private FUI_GamePanel _fuiGamePanel;

		public FUI_GamePanel FUIGamePanel
		{
			get => _fuiGamePanel ??= (FUI_GamePanel)this.GetParent<FUIEntity>().GComponent;
		}
		
		public int CurrentControlerIndex { get; set; }
		public int CurrentLanguageIndex { get; set; }
	}
	
	[ChildOf]
	public class GamePanel_ContextData: Entity, IAwake
	{
		/// <summary>
		/// 测试数据
		/// </summary>
		public string Data { get; set; }
	}
}
