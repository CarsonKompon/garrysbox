using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using CarsonK;

[Library]
public partial class SandboxHud : HudEntity<RootPanel>
{
	public SandboxHud()
	{
		if ( !Game.IsClient )
			return;

		RootPanel.StyleSheet.Load( "/Styles/sandbox.scss" );

		RootPanel.AddChild<Chat>();
		RootPanel.AddChild<VoiceList>();
		RootPanel.AddChild<VoiceSpeaker>();
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
		RootPanel.AddChild<Health>();
		RootPanel.AddChild<GmodInventoryBar>();
		RootPanel.AddChild<CurrentTool>();
		RootPanel.AddChild<GmodSpawnMenu>();
		RootPanel.AddChild<Crosshair>();

		RootPanel.StyleSheet.Load("/ui/gmod/gmod.scss");

		for(int z=0; z<RootPanel.ChildrenCount; z++)
		{
			var child = RootPanel.GetChild(z);
			switch(child.GetType().Name)
			{
				case "Health":
					child.Add.Label("HEALTH", "health-header");
					break;
				
				case "Chat":

					for(int i=0; i<child.ChildrenCount; i++)
					{
						var chatChild = child.GetChild(i);
						if(chatChild is TextEntry chatEntry)
						{
							var entryPanel = child.Add.Panel("textentry-container");
							entryPanel.Add.Label("Say :", "textentry-say");
							entryPanel.AddChild(chatEntry);
						}
					}
					break;
				
				case "SpawnMenu":
					var newSpawnMenu = RootPanel.AddChild<GmodSpawnMenu>();
					RootPanel.SetChildIndex(newSpawnMenu, RootPanel.GetChildIndex(child));
					child.Delete(true);
					break;
				
				case "InventoryBar":
					var newInventoryBar = RootPanel.AddChild<GmodInventoryBar>();
					RootPanel.SetChildIndex(newInventoryBar, RootPanel.GetChildIndex(child));
					child.Delete(true);
					break;

				default:
					if(child.GetType().Name.Contains("Scoreboard"))
					{
						child.Add.Label(Game.Server.GameTitle, "scoreboard-header");
					}
					else
					{
						Log.Info("Unknown type: " + child.GetType().Name);
					}
					break;
			}
		}


	}
}
