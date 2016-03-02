namespace qwik.helpers
{
    public class Menu
    {
        public static void RunMenuByName(string name)
        {
            var menu = Externals.GetMenu(Windows.AolWindow());
            var count = Externals.GetMenuItemCount(menu);
            for (var main = 0; main < count; ++main)
            {
                var submenu = Externals.GetSubMenu(menu, main);
                var subcount = Externals.GetMenuItemCount(submenu);
                for (var sub = 0; sub < subcount; ++sub)
                {
                    var menuText = Regexes.HotKey.Replace(Externals.GetMenuText(submenu, sub), string.Empty);
                    if (menuText.ToLower().Trim() != name.ToLower().Trim()) continue;
                    var subMenuId = Externals.GetMenuItemId(submenu, sub);
                    Externals.SendMessageLong(Windows.AolWindow(), Externals.WM_COMMAND, subMenuId.ToInt32(), 0);
                }
            }
        }
    }
}