using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace DeconstructHotkey;

public class Patches
{
    [HarmonyPatch(typeof(Deconstructable), "OnRefreshUserMenu")]
    public class Deconstructable_OnRefreshUserMenu_Patch
    {
        private static Action CustomAction { get; } = Action.BuildingUtility2;

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> oldBody)
        {
            var newBody = oldBody.ToList();

            // in the calls to KIconButtonMenu.ButtonInfo.ctor replace the default shortcutKey param value with the custom one
            var buttonInfoShortcuts = newBody.Where(i => i.opcode == OpCodes.Ldc_I4 && i.operand is int value && value == (int)Action.NumActions);

            foreach (var instruction in buttonInfoShortcuts)
            {
                instruction.operand = (int)CustomAction;
            }

            Debug.Log($"DeconstructHotkey: shortcut replaced to {CustomAction}");

            return newBody;
        }
    }
}
