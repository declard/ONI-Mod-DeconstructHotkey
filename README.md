# Bind the currently selected building's deconstruction to a hotkey in Oxygen Not Included

A tiny quality-of-life mod for Oxygen Not Included (vanilla version) that binds building deconstruction to BuildingUtility2 action which can then be assigned a key like Q or whatever feels good for you.

To use the mod locally follow [this guide](https://github.com/Cairath/Oxygen-Not-Included-Modding/wiki/Introduction).

Alternatively:
- make sure that either you have ONI installed at the standard Steam location (`C:\Program Files (x86)\Steam\steamapps\common\OxygenNotIncluded`) or you update the csproj file accordingly
- install dotnet 4.7.1 SDK + probably something newer (the latest C# is used, I didn't check if it compiles with 4.7.1 SDK)
- use `dotnet build -c Release` to obtain the binaries
- copy DeconstructHotkey.dll + mod.yaml + mod_info.yaml files into `%USERPROFILE%\Documents\Klei\OxygenNotIncluded\mods\Local\DeconstructHotkey`
