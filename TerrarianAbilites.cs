using Terraria.ModLoader;

namespace TerrarianAbilites
{
    /*
	 * internal MenuBar MenuBar; UIState
	 * private UserInterface _menuBar; 
	 * public override void Load()
{
    MenuBar = new MenuBar();
    MenuBar.Activate();
    _menuBar = new UserInterface();
    _menuBar.SetState(MenuBar);
}
public override void UpdateUI(GameTime gameTime)
{
    _menuBar?.Update(gameTime);
}
    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
{
    int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
    if (mouseTextIndex != -1)
    {
        layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
            "YourMod: A Description",
            delegate
            {
                _menuBar.Draw(Main.spriteBatch, new GameTime());
                return true;
            },
            InterfaceScaleType.UI)
        );
    }
}
	 */
    public class TerrarianAbilites : Mod
	{
	}

}