using HarmonyLib;
using STRINGS;
using System.Linq;
using System.Reflection;

namespace DeconstructHotkey;

public class Patches
{
    [HarmonyPatch(typeof(Deconstructable), "OnRefreshUserMenu")]
    public class Deconstructable_OnRefreshUserMenu_Patch
    {
        public static bool Prefix(object data, Deconstructable __instance)
        {
            OnRefreshUserMenu(__instance, data);
            return false;
        }

        private delegate void OnDeconstructDelegate(Deconstructable @this);

        private static OnDeconstructDelegate OnDeconstruct { get; } = BuildOnDeconstruct();

        private static OnDeconstructDelegate BuildOnDeconstruct() => (OnDeconstructDelegate)typeof(Deconstructable)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .First(m => m.Name == "OnDeconstruct" && m.GetParameters().Length == 0)
            .CreateDelegate(typeof(OnDeconstructDelegate));

        private static void OnRefreshUserMenu(Deconstructable @this, object data)
        {
            if (!@this.allowDeconstruction)
                return;

            var buttonInfo = @this.chore == null
                ? new KIconButtonMenu.ButtonInfo("action_deconstruct", (string)UI.USERMENUACTIONS.DECONSTRUCT.NAME,
                    on_click: () => OnDeconstruct(@this),
                    shortcutKey: Action.BuildingUtility2,
                    tooltipText: (string)UI.USERMENUACTIONS.DECONSTRUCT.TOOLTIP)
                : new KIconButtonMenu.ButtonInfo("action_deconstruct", (string)UI.USERMENUACTIONS.DECONSTRUCT.NAME_OFF,
                    on_click: () => OnDeconstruct(@this),
                    shortcutKey: Action.BuildingUtility2,
                    tooltipText: (string)UI.USERMENUACTIONS.DECONSTRUCT.TOOLTIP_OFF);

            Game.Instance.userMenu.AddButton(@this.gameObject, buttonInfo, 0.0f);
        }
    }
}
