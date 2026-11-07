using LogicRevampedMod;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Runtime.Remoting.Messaging;
using MelonLoader;
using MelonLoader.Utils;
using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using UnityEngine;
using UnityEngine.Playables;
using static Il2Cpp.GameplayEvents;
using static Il2CppSystem.Array;
using static MelonLoader.Modules.MelonModule;
using LogicRevampedMod;
using Il2CppInterop.Runtime.Runtime;

[assembly: MelonInfo(typeof(MainMod), "Logic Revamped", "1.0.0", "Wingidon")]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace LogicRevampedMod;
public class MainMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        //ClassInjector.RegisterTypeInIl2Cpp<w_Arbiter>();
    }



    /*
    Todo:
    - Puppeteer
    - Shaman
    - Werewolf
    - Witch

    - Baa
    - Lilis
    - Pooka


    Not doing:
    - Baker - I'm not dealing with her.

    - Bombardier - Nothing to redo.
    - Rambler - Nothing to redo.

    - Puppet - Nothing to redo.
    */




    public MelonPreferences_Category configCategory = null!;
    public override void OnLateInitializeMelon()
    {
        GameObject content = GameObject.Find("Game/Gameplay/Content");
        NightPhase nightPhase = content.GetComponent<NightPhase>();
        Statics.GetStartingRoles();
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        configCategory = MelonPreferences.CreateCategory("LogicRevampedSettings");
        configCategory.CreateEntry("BlindModeDreamer", false, "BlindModeDreamer", "VILLAGERS\nWhen enabled, the Dreamer is allowed to use not-in-Deck roles as the false roles in her info.");
        configCategory.CreateEntry("BlindModeDruid", false, "BlindModeDruid", "\nWhen enabled, a Lying Druid is allowed to mention not-in-Deck Disguised Outcasts.");
        configCategory.CreateEntry("BlindModeOracle", false, "BlindModeOracle", "\nWhen enabled, a Lying Oracle is allowed to mention not-in-Deck Minions.");
        configCategory.CreateEntry("BlindModeScout", false, "BlindModeScout", "\nWhen enabled, a Lying Scout is allowed to mention not-in-Deck Minions.");
        configCategory.CreateEntry("AlternateConfessions", false, "AlternateConfessions", "\nWhen enabled, the Confessor will say \"I am the Confessor\" when Truthful and something else when Lying, akin to Dupery's Priest." +
            "\nWith this setting enabled, the Confessor is no longer always Truthful.");
        configCategory.CreateEntry("ConfessorReminders", false, "ConfessorReminders", "\nWhen enabled, the Confessor will remind you that she is always Truthful in her info, unless the above setting is enabled.");
        configCategory.CreateEntry("ExpandedPoet", true, "ExpandedPoet", "\nWhen enabled, the Poet is allowed to pull info from Fortune Teller, Jester, Judge, Druid, Dreamer and Investigator.");
        configCategory.CreateEntry("ModdedPoet", true, "ModdedPoet", "\n[EXPERIMENTAL, NOT THOROUGHLY TESTED]\nWhen enabled, the Poet is allowed to pull info from modded roles from other installed mods.");



        configCategory.CreateEntry("DoppelOutcastDisguise", false, "DoppelOutcastDisguise", "\n\n\nOUTCASTS\nWhen enabled, the Doppelganger is allowed to Disguise as another Outcast.");
        configCategory.CreateEntry("DrunkOutcastDisguise", false, "DrunkOutcastDisguise", "\nWhen enabled, the Drunk is allowed to Disguise as another Outcast.");
        configCategory.CreateEntry("DrunkDamage", 2, "DrunkDamage", "\nThe amount of damage the Drunk deals to you when Executed.");
        configCategory.CreateEntry("LycanOutcastDisguise", false, "LycanOutcastDisguise", "\nWhen enabled, the Lycanthrope is allowed to Disguise as another Outcast.");
        configCategory.CreateEntry("LycanDoubleClaim", false, "LycanDoubleClaim", "\nWhen enabled, the Lycanthrope is allowed to claim an in-play role.");



        configCategory.CreateEntry("PoisonerExtendedRange", false, "PoisonerExtendedRange", "\n\n\nMINIONS\nWhen enabled, the Poisoner will poison 1 Good Villager closest to them (instead of just adjacent)");
        configCategory.CreateEntry("PuppeteerExtendedRange", false, "PuppeteerExtendedRange", "\nWhen enabled, the Puppeteer will create a Puppet out of a Good Villager closest to them (instead of just adjacent)");
        configCategory.CreateEntry("MinionUniquity", false, "MinionUniquity", "\nWhen enabled, the Minion will always Disguise as a not-in-play role.");
        configCategory.CreateEntry("MinionTruthChance", 0, "MinionTruthChance", "\nThe percent chance for the Minion to tell the Truth to blend in.");
        configCategory.CreateEntry("TwinionDuplicity", false, "TwinionDuplicity", "\nWhen enabled, the Twin Minion will always Disguise as an in-play role.");
        configCategory.CreateEntry("TwinionTruthChance", 0, "TwinionTruthChance", "\nThe percent chance for the Twin Minion to tell the Truth to blend in.");



        configCategory.CreateEntry("BaaHiddenOutcasts", 1, "BaaHiddenOutcasts", "\n\n\nDEMONS\nHow many Outcast roles should Baa hide from the Deck?\nIf set to 4 or higher, Baa hides every Outcast in the Deck.");
        configCategory.CreateEntry("BaaHiddenMinions", 0, "BaaHiddenMinions", "\nHow many Minion roles should Baa hide from the Deck?\nIf set to 4 or higher, Baa hides every Minion in the Deck.");
        configCategory.CreateEntry("BaaRevealDeath", true, "BaaRevealDeath", "\nWhether or not Baa should reveal the hidden roles on death.");
        configCategory.CreateEntry("BaaAddOutcastsMin", 0, "BaaAddOutcastsMin", "\nThe minimum number of Outcasts that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddOutcastsMax", 0, "BaaAddOutcastsMax", "\nThe maximum number of Outcasts that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddMinionsMin", 0, "BaaAddMinionsMin", "\nThe minimum number of Minions that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddMinionsMax", 0, "BaaAddMinionsMax", "\nThe maximum number of Minions that Baa can add to the Deck.");
        configCategory.CreateEntry("LilisDamage", 2, "LilisDamage", "\nHow much damage Lilis deals when attacking.");
        configCategory.CreateEntry("LilisEvilAttack", true, "LilisEvilAttack", "\nIf disabled, Lilis will never attack Evil characters, even if they are her last valid targets.");
        configCategory.CreateEntry("LilisSelfAttack", false, "LilisSelfAttack", "\nIf enabled, Lilis is allowed to attack herself if only Evil characters are Unrevealed.");
        configCategory.CreateEntry("LilisKillRevealed", false, "LilisKillRevealed", "\nIf enabled, Lilis is allowed to kill Revealed characters.");
        configCategory.CreateEntry("LilisRevealedDamageBonus", 0, "LilisRevealedDamageBonus", "\nHow much extra damage Lilis deals if she attacks a Revealed character.");
        configCategory.CreateEntry("PookaExtendedRange", false, "PookaExtendedRange", "\nWhen enabled, Pooka will Corrupt the Good Villagers closest to her (instead of just adjacent).");
        configCategory.CreateEntry("DemonOutcastDisguise", false, "DemonOutcastDisguise", "\nWhen enabled, Demons are allowed to Disguise themselves as Outcasts.");





        configCategory.CreateEntry("OverrideVillageComps", false, "OverrideVillageComps", "\n\n\nVILLAGE COMPOSITION\nWhen enabled, vanilla Demons will show up in new compositions." +
            "\nThe rest of the settings in this category will not work if this setting is disabled.");
        configCategory.CreateEntry("Baa_MinimumSize", 7, "Baa_MinimumSize", "\nThe lowest-sized village Baa is allowed to appear in.\nDefault: 7\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Baa_MaximumSize", 9, "Baa_MaximumSize", "\nThe highest-sized village Baa is allowed to appear in.\nDefault: 9\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Baa_DifficultyModifier", 0, "Baa_DifficultyModifier", "\nHow many more or less Villagers than normal should be in each Baa village.\nPositive numbers add Villagers, negative numbers subtract Villagers.");
        configCategory.CreateEntry("Baa_ExoticCompositions", false, "Baa_ExoticCompositions", "\nWhen enabled, Baa is allowed to appear in more \'exotic\' compositions containing unusually high or low numbers of Outcasts & Minions.");

        configCategory.CreateEntry("Lilis_MinimumSize", 8, "Lilis_MinimumSize", "\nThe lowest-sized village Lilis is allowed to appear in.\nDefault: 8\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Lilis_MaximumSize", 10, "Lilis_MaximumSize", "\nThe highest-sized village Lilis is allowed to appear in.\nDefault: 10\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Lilis_DifficultyModifier", 0, "Lilis_DifficultyModifier", "\nHow many more or less Villagers than normal should be in each Lilis village.");
        configCategory.CreateEntry("Lilis_ExoticCompositions", false, "Lilis_ExoticCompositions", "\nWhen enabled, Lilis is allowed to appear in more \'exotic\' compositions containing unusually high or low numbers of Outcasts & Minions.");

        configCategory.CreateEntry("Pooka_MinimumSize", 6, "Pooka_MinimumSize", "\nThe lowest-sized village Pooka is allowed to appear in.\nDefault: 6\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Pooka_MaximumSize", 10, "Pooka_MaximumSize", "\nThe highest-sized village Pooka is allowed to appear in.\nDefault: 10\nMinimum: 3\nMaximum: 20");
        configCategory.CreateEntry("Pooka_DifficultyModifier", 0, "Pooka_DifficultyModifier", "\nHow many more or less Villagers than normal should be in each Pooka village.");
        configCategory.CreateEntry("Pooka_ExoticCompositions", false, "Pooka_ExoticCompositions", "\nWhen enabled, Pooka is allowed to appear in more \'exotic\' compositions containing unusually high or low numbers of Outcasts & Minions.");

        // configCategory.CreateEntry("Village_ExtraVillagerSuspects", 0, "Village_ExtraVillagerSuspects", "\nHow many additional Villagers should be in the Deck for each village."); // Pointless
        configCategory.CreateEntry("Village_ExtraOutcastSuspects", 0, "Village_ExtraOutcastSuspects", "\nHow many additional Outcasts should be in the Deck for each village.");
        configCategory.CreateEntry("Village_ExtraMinionSuspects", 0, "Village_ExtraMinionSuspects", "\nHow many additional Minions should be in the Deck for each village.");
        configCategory.CreateEntry("Village_OnlyMinions", false, "Village_OnlyMinions", "\nWhen enabled, villages are allowed to have no Demons, only Minions!");



        configCategory.CreateEntry("DebugMode", false, "DebugMode", "\n\n\nMISC\nWhen enabled, certain roles will print things out to the console. Used for debugging.");
        configCategory.CreateEntry("InstantPick", false, "InstantPick", "\nWhen enabled, Villagers with Pick abilities will pick their targets instantly.");
        configCategory.CreateEntry("RoleFlavour", false, "RoleFlavour", "\nWhen enabled, roles that are usually silent will tell you something really interesting instead of staying silent.");
        configCategory.SetFilePath(Path.Combine(MelonEnvironment.UserDataDirectory, "LogicRevampedSettings.cfg"));
        configCategory.SaveToFile();















        AscensionsData advancedAscension = ProjectContext.Instance.gameData.advancedAscension;





        MelonLogger.Msg($"Handling scripts...");
        Il2CppSystem.Collections.Generic.List<string> displayedScripts = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> replaceScripts = new();
        w_zRoleBase configChecker = new();

        if (configChecker.CheckConfigOption_Bool("OverrideVillageComps"))
        {
            replaceScripts.Add("Baa_Difficult");
            replaceScripts.Add("Baa_2_Difficult");
            replaceScripts.Add("Pookah_1Minion");
            replaceScripts.Add("Pookah_2Minions");
            replaceScripts.Add("Pookah_Small");
            replaceScripts.Add("Lilis_1");
        }

        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            ScriptInfo script = scriptData.scriptInfo;
            if (!displayedScripts.Contains(scriptData.name))
            {
                MelonLogger.Msg($"Script: {scriptData.name}");
                displayedScripts.Add(scriptData.name);
                if (replaceScripts.Contains(scriptData.name))
                {
                    MelonLogger.Msg($"Replacing counts...");
                    Il2CppSystem.Collections.Generic.List<CharactersCount> newCounts = GetCharacterCountsOfDemon(scriptData.scriptInfo.startingDemons[0].characterName);
                    if (newCounts.Count == 0)
                    {
                        MelonLogger.Msg($"Invalid settings! Your chosen settings are preventing the composition overrides from having anything in them.");
                        replaceScripts.Clear();
                    }
                    else
                    {
                        script.characterCounts = newCounts;
                        script.mustInclude.Clear();
                    }
                }
                Il2CppSystem.Collections.Generic.List<string> characterCounts = new Il2CppSystem.Collections.Generic.List<string>();
                foreach (CharactersCount characterCount in script.characterCounts)
                {
                    string charCount = $"{characterCount.town}/{characterCount.outs}/{characterCount.minion}/{characterCount.demon}";
                    if (!characterCounts.Contains(charCount)) MelonLogger.Msg(charCount);
                    characterCounts.Add(charCount);
                }
            }
        }
    }



    int outcastRoleCount = 5;
    int minionRoleCount = 8;

    public Il2CppSystem.Collections.Generic.List<CharactersCount> GetCharacterCountsOfDemon(string demonName)
    {
        Il2CppSystem.Collections.Generic.List<CharactersCount> requestedCounts = new();
        Il2CppSystem.Collections.Generic.List<CharactersCount> returnList = new();

        Il2CppSystem.Collections.Generic.List<CharacterData> outcastRoles = new();
        Il2CppSystem.Collections.Generic.List<CharacterData> minionRoles = new();

        AscensionsData advancedAscension = ProjectContext.Instance.gameData.advancedAscension;
        foreach (CustomScriptData data in advancedAscension.possibleScriptsData)
        {
            if (data.name == "Baa_Difficult")
            {
                foreach (CharacterData character in data.scriptInfo.startingOutsiders) outcastRoles.Add(character);
                foreach (CharacterData character in data.scriptInfo.startingMinions) minionRoles.Add(character);
            }
            break;
        }
        outcastRoleCount = outcastRoles.Count;
        minionRoleCount = minionRoles.Count;



        w_zRoleBase configChecker = new();
        int difficultyMod = configChecker.CheckConfigOption_Int($"{demonName}_DifficultyModifier");

        // Each size gets 50 lots total.

        // Size: 3
        requestedCounts = addCharacterCount(setCharacterCount(1, 1, 1, 0), requestedCounts, 20, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(2, 0, 0, 1), requestedCounts, 30, difficultyMod);


        // Size: 4
        requestedCounts = addCharacterCount(setCharacterCount(2, 1, 1, 0), requestedCounts, 15, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(2, 1, 0, 1), requestedCounts, 15, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(2, 0, 1, 1), requestedCounts, 20, difficultyMod);


        // Size: 5
        requestedCounts = addCharacterCount(setCharacterCount(4, 0, 1, 0), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 1, 1, 0), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 0, 2, 0), requestedCounts, 8, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(3, 0, 1, 1), requestedCounts, 13, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 1, 0, 1), requestedCounts, 13, difficultyMod);


        // Size: 6
        requestedCounts = addCharacterCount(setCharacterCount(3, 1, 2, 0), requestedCounts, 10, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 2, 1, 0), requestedCounts, 10, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(4, 1, 0, 1), requestedCounts, 15, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 0, 1, 1), requestedCounts, 15, difficultyMod);


        // Size: 7
        requestedCounts = addCharacterCount(setCharacterCount(4, 1, 2, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 1, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 0, 3, 0), requestedCounts, 6, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(4, 0, 2, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 1, 1, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 1, 0, 1), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 0, 1), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 3, 0, 1), requestedCounts, 6, difficultyMod);


        // Size: 8
        requestedCounts = addCharacterCount(setCharacterCount(4, 0, 4, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 1, 3, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 2, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 3, 1, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(3, 4, 1, 0), requestedCounts, 3, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(3, 4, 0, 1), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 1, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 1, 2, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 0, 2, 1), requestedCounts, 6, difficultyMod);


        // Size: 9
        requestedCounts = addCharacterCount(setCharacterCount(5, 1, 3, 0), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 2, 2, 0), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 4, 1, 0), requestedCounts, 8, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(4, 4, 0, 1), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 3, 1, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 2, 1), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 2, 1, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 1, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 0, 3, 1), requestedCounts, 2, difficultyMod);


        // Size: 10
        requestedCounts = addCharacterCount(setCharacterCount(5, 4, 1, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 3, 2, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 2, 3, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 1, 4, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 5, 1, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 4, 2, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 3, 3, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 2, 4, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 0, 5, 0), requestedCounts, 2, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(5, 0, 4, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 1, 3, 1), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 2, 2, 1), requestedCounts, 10, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 3, 1, 1), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(4, 5, 0, 1), requestedCounts, 3, difficultyMod);


        // Size: 11
        requestedCounts = addCharacterCount(setCharacterCount(6, 0, 5, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 4, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 3, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 2, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 5, 1, 0), requestedCounts, 3, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(6, 0, 4, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 3, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 2, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 1, 1), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(5, 5, 0, 1), requestedCounts, 4, difficultyMod);


        // Size: 12
        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 5, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 4, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 3, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 4, 2, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 5, 1, 0), requestedCounts, 3, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 4, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 3, 1), requestedCounts, 10, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 2, 1), requestedCounts, 10, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 4, 1, 1), requestedCounts, 5, difficultyMod);


        // Size: 13
        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 6, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 5, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 4, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 4, 3, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 5, 2, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 6, 1, 0), requestedCounts, 2, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(6, 1, 5, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 2, 4, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 3, 3, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 4, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 5, 1, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(6, 6, 0, 1), requestedCounts, 3, difficultyMod);


        // Size: 14
        requestedCounts = addCharacterCount(setCharacterCount(7, 1, 6, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 2, 5, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 3, 4, 0), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 4, 3, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 5, 2, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 6, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(7, 1, 5, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 2, 4, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 3, 3, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 4, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 5, 1, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 6, 0, 1), requestedCounts, 3, difficultyMod);


        // Size: 15
        requestedCounts = addCharacterCount(setCharacterCount(7, 1, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 2, 6, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 3, 5, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 4, 4, 0), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 5, 3, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 6, 2, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(7, 7, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(8, 1, 5, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 2, 4, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 3, 3, 1), requestedCounts, 9, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 4, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 5, 1, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 6, 0, 1), requestedCounts, 3, difficultyMod);


        // Size: 16
        requestedCounts = addCharacterCount(setCharacterCount(8, 0, 8, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 1, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 2, 6, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 3, 5, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 4, 4, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 5, 3, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 6, 2, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 7, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(8, 0, 7, 1), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 1, 6, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 2, 5, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 3, 4, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 4, 3, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 5, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 6, 1, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 7, 0, 1), requestedCounts, 2, difficultyMod);


        // Size: 17
        requestedCounts = addCharacterCount(setCharacterCount(8, 1, 8, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 2, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 3, 6, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 4, 5, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 5, 4, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 6, 3, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 7, 2, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 8, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(8, 1, 7, 1), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 2, 6, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 3, 5, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 4, 4, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 5, 3, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 6, 2, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 7, 1, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(8, 8, 0, 1), requestedCounts, 2, difficultyMod);


        // Size: 18
        requestedCounts = addCharacterCount(setCharacterCount(9, 0, 9, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 1, 8, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 2, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 3, 6, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 4, 5, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 5, 4, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 6, 3, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 7, 2, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 8, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(9, 0, 8, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 1, 7, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 2, 6, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 3, 5, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 4, 4, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 5, 3, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 6, 2, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 7, 1, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 8, 0, 1), requestedCounts, 1, difficultyMod);


        // Size: 19
        requestedCounts = addCharacterCount(setCharacterCount(9, 1, 9, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 2, 8, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 3, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 4, 6, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 5, 5, 0), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 6, 4, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 7, 3, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 8, 2, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 9, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(9, 1, 8, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 2, 7, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 3, 6, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 4, 5, 1), requestedCounts, 7, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 5, 4, 1), requestedCounts, 5, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 6, 3, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 7, 2, 1), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 8, 1, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(9, 9, 0, 1), requestedCounts, 1, difficultyMod);


        // Size: 20
        requestedCounts = addCharacterCount(setCharacterCount(10, 0, 10, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 1, 9, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 2, 8, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 3, 7, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 4, 6, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 5, 5, 0), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 6, 4, 0), requestedCounts, 3, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 7, 3, 0), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 8, 2, 0), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 9, 1, 0), requestedCounts, 1, difficultyMod);

        requestedCounts = addCharacterCount(setCharacterCount(10, 0, 9, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 1, 8, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 2, 7, 1), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 3, 6, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 4, 5, 1), requestedCounts, 8, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 5, 4, 1), requestedCounts, 6, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 6, 3, 1), requestedCounts, 4, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 7, 2, 1), requestedCounts, 2, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 8, 1, 1), requestedCounts, 1, difficultyMod);
        requestedCounts = addCharacterCount(setCharacterCount(10, 9, 0, 1), requestedCounts, 1, difficultyMod);



        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<string> prevMentionedComps = new();
        Il2CppSystem.Collections.Generic.List<string> newComps = new();
        sharedScripts.DebugMessage($"Difficulty modifier: {difficultyMod}");
        foreach (CharactersCount count in requestedCounts)
        {
            string countToString = $"{count.town}/{count.outs}/{count.minion}/{count.demon}";

            if (count.outs != 0) count.dOuts = count.outs + configChecker.CheckConfigOption_Int("Village_ExtraOutcastSuspects");
            if (count.minion != 0) count.dMinion = count.minion + configChecker.CheckConfigOption_Int("Village_ExtraMinionSuspects");

            count.dOuts = System.Math.Min(count.dOuts, outcastRoleCount);
            count.dMinion = System.Math.Min(count.dMinion, minionRoleCount);

            //if (!prevMentionedComps.Contains(countToString))
            //{
                //count.town += configChecker.CheckConfigOption_Int($"{demonName}_DifficultyModifier");
                //count.allCharCount += configChecker.CheckConfigOption_Int($"{demonName}_DifficultyModifier");
            //}

            string countToStringTwo = $"{count.town}/{count.outs}/{count.minion}/{count.demon}";

            // if (!prevMentionedComps.Contains(countToString)) sharedScripts.DebugMessage($"Found {demonName} comp of {countToString}.");
            // count.dTown = count.town + configChecker.CheckConfigOption_Int("Village_ExtraVillagerSuspects");
        }



        foreach (CharactersCount count in requestedCounts)
        {
            returnList = AddCharacterCountIfValid(demonName, count, returnList);
        }

        Il2CppSystem.Collections.Generic.List<string> mentionedComps = new();
        foreach (CharactersCount count in returnList)
        {
            string countToString = $"{count.town}/{count.outs}/{count.minion}/{count.demon}";
            if (!mentionedComps.Contains(countToString))
            {
                mentionedComps.Add(countToString);
                sharedScripts.DebugMessage($"Found village composition ({demonName}): {countToString}");
            }
        }

        return returnList;
    }

    public Il2CppSystem.Collections.Generic.List<CharactersCount> AddCharacterCountIfValid(string demonName, CharactersCount characterCount, Il2CppSystem.Collections.Generic.List<CharactersCount> addList)
    {
        Il2CppSystem.Collections.Generic.List<CharactersCount> returnList = addList;
        if (CheckCountValid(demonName, characterCount)) returnList = addCharacterCount(characterCount, addList, 1);
        return returnList;
    }

    public bool CheckCountValid(string demonName, CharactersCount count)
    {
        w_zRoleBase configChecker = new();
        int minSize = configChecker.CheckConfigOption_Int($"{demonName}_MinimumSize");
        int maxSize = configChecker.CheckConfigOption_Int($"{demonName}_MaximumSize");
        int difficultyMod = configChecker.CheckConfigOption_Int($"{demonName}_DifficultyModifier");
        bool exoticCompsAllowed = configChecker.CheckConfigOption_Bool($"{demonName}_ExoticCompositions");
        bool onlyMinions = configChecker.CheckConfigOption_Bool($"Village_OnlyMinions");

        // Regular constraints.
        if (count.town + difficultyMod < 0) return false; // Avoid negative Villager counts
        if (count.allCharCount + difficultyMod < minSize) return false; // Avoid villages below the minimum size.
        if (count.allCharCount + difficultyMod > maxSize) return false; // Avoid villages above the maximum size.
        if (!onlyMinions && count.demon == 0) return false; // Avoid Minion-only villages if the setting is disabled.

        // Exotic constraints.
        int allCount = count.allCharCount;
        int villagerCount = count.town + difficultyMod; // For some reason, the checker hasn't been taking the difficulty mod into account. This should fix it.
        int outcastCount = count.outs;
        int minionCount = count.minion;
        int demonCount = count.demon;
        int goodCount = count.town + count.outs;
        int evilCount = count.minion + count.demon;
        if (outcastCount >= outcastRoleCount) return false; // If there are too many Outcasts compared to how many roles there actually are, disable.
        if (minionCount >= minionRoleCount) return false; // If there are too many Minions compared to how many roles there actually are, disable.
        if (!exoticCompsAllowed)
        {
            if (outcastCount >= villagerCount) return false; // If the Outcast count is greater than or equal to the Villager count, disable.
            if (evilCount >= villagerCount) return false; // If the Evil count is greater than or equal to the Villager count, disable.
            if (villagerCount < allCount * 0.4) return false; // If Villagers make up less than 40% of the village, disable.
            if (outcastCount > allCount * 0.33) return false; // If Outcasts make up more than 33% of the village, disable.
            if (minionCount > allCount * 0.4) return false; // If Minions make up more than 40% of the village, disable.
            if (evilCount >= allCount * 0.5) return false; // If Evils make up half or more of the village, disable.
            if (outcastCount - 1 > minionCount) return false; // If there are 2 or more Outcasts than Minions, disable.
            if (minionCount == 0 && allCount > 7) return false; // Solo Evil villages with 8 or more characters.
            int outcastMinionDiff = outcastCount - minionCount;
            if (outcastMinionDiff < 0) outcastMinionDiff *= -1;
            if (outcastMinionDiff > 2) return false; // If the difference between the two categories is too grand, disable.
        }

        if (allCount > 50) return false; // That's ridiculous.

        return true;
    }


    public CharactersCount setCharacterCount(int Villagers, int Outcasts, int Minions, int Demons)
    {
        CharactersCount myCharacterCount = new CharactersCount(Villagers + Outcasts + Minions + Demons, Villagers, Demons, Outcasts, Minions);
        myCharacterCount.dOuts = Outcasts + 1;
        return myCharacterCount;
    }


    public CharactersCount setCharacterCountByVillageSize(int size, int Outcasts, int Minions, int Demons)
    {
        int villagers = (size - Outcasts - Minions - Demons);
        CharactersCount myCharacterCount = new CharactersCount(size, villagers, Demons, Outcasts, Minions);
        myCharacterCount.dOuts = Outcasts + 1;
        return myCharacterCount;
    }
    public Il2CppSystem.Collections.Generic.List<CharactersCount> addCharacterCount(CharactersCount characterCount, Il2CppSystem.Collections.Generic.List<CharactersCount> addList, int weight)
    {
        Il2CppSystem.Collections.Generic.List<CharactersCount> returnList = addList;
        for (int i = 0; i < weight; i++)
        {
            returnList.Add(characterCount);
        }
        return returnList;
    }
    public Il2CppSystem.Collections.Generic.List<CharactersCount> addCharacterCount(CharactersCount characterCount, Il2CppSystem.Collections.Generic.List<CharactersCount> addList, int weight, int villagerMod)
    {
        characterCount.town += villagerMod;
        characterCount.dTown += villagerMod;
        Il2CppSystem.Collections.Generic.List<CharactersCount> returnList = addList;
        for (int i = 0; i < weight; i++)
        {
            returnList.Add(characterCount);
        }
        return returnList;
    }

    public void addCharacterDataToList(string ID, List<CharacterData> Characters)
    {
        foreach (CharacterData targetChar in Gameplay.Instance.GetAllAscensionCharacters())
        {
            if (targetChar.characterId == ID)
            {
                Characters.Append(targetChar);
            }
        }
    }
    public void addRole(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        if (list.Contains(data))
        {
            return;
        }
        list.Add(data);
    }
    public void addRoleEvenIfDupe(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        list.Add(data);
    }
    public void addRoleIfNotJinxed(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data, Il2CppSystem.Collections.Generic.List<CharacterData> jinxList, Il2CppSystem.Collections.Generic.List<CharacterData> jinxCheckList)
    {
        if (list.Contains(data))
        {
            return;
        }
        bool jinxed = false;
        foreach (CharacterData character in jinxList)
        {
            foreach (CharacterData character2 in jinxCheckList)
            {
                if (character2 == character)
                {
                    jinxed = true;
                }
            }
        }
        if (jinxed) return;
        list.Add(data);
    }
    public CharacterData[] allDatas = System.Array.Empty<CharacterData>();
    public override void OnUpdate()
    {
        if (allDatas.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                allDatas = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    allDatas[i] = loadedCharList[i]!.Cast<CharacterData>();
                }
            }
        }
        if (Statics.charactersArray.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                Statics.charactersArray = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    CharacterData data = loadedCharList[i]!.Cast<CharacterData>();
                    Statics.CheckAddRole(data);
                    Statics.charactersArray[i] = data;
                }
            }
            if (Statics.charactersArray.Length > 0)
            {
                this.OnFirstUpdate();
            }
        }
    }
    public CharacterData[] InsertAfterAct(string previous, CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} after {previous}");
        CharacterData[] actList = Characters.Instance.startGameActOrder;

        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        bool inserted = false;
        for (int i = 0; i < actSize; i++)
        {
            if (inserted)
            {
                newActList[i + 1] = actList[i];
            }
            else
            {
                if (actList[i] != null)
                {
                    newActList[i] = actList[i];
                    if (actList[i].name == previous)
                    {
                        newActList[i + 1] = data;
                        inserted = true;
                    }
                }
            }
        }
        if (!inserted)
        {
            LoggerInstance.Msg("");
        }
        return newActList;
    }
    public CharacterData[] InsertAtStartOfActOrder(CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} to start of act order");
        CharacterData[] actList = Characters.Instance.startGameActOrder;
        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        for (int i = 0; i < actSize; i++)
        {
            newActList[i + 1] = actList[i];
        }
        newActList[0] = data;
        return newActList;
    }
    public CharacterData[] InsertAtEndOfActOrder(CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} to end of act order");
        CharacterData[] actList = Characters.Instance.startGameActOrder;
        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        for (int i = 0; i < actSize; i++)
        {
            newActList[i] = actList[i];
        }
        newActList[actSize] = data;
        return newActList;
    }
    public CharacterData[] insertBeforeAct(string next, CharacterData data)
    {
        MelonLogger.Msg($"insertBeforeAct called adding {data.name.ToString()} before {next}");
        int actSize = Characters.Instance.startGameActOrder.Length;
        Il2CppSystem.Collections.Generic.List<CharacterData> newActList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        bool added = false;
        foreach (CharacterData character in Characters.Instance.startGameActOrder)
        {
            MelonLogger.Msg($"Attempting to add {character.name.ToString()} to act order");
            if (character.name.ToString() == next) MelonLogger.Msg($"Found target {character.name.ToString()}");
            if (character.name.ToString() == next && added == false)
            {
                MelonLogger.Msg($"Adding target {data.name.ToString()} to newActList");
                newActList.Add(data);
                MelonLogger.Msg($"Added {data.name.ToString()} to newActList");
            }
            MelonLogger.Msg($"Adding {character.name.ToString()} to newActList");
            newActList.Add(character);
        }
        CharacterData[] newActArray = new CharacterData[actSize + 1];
        int counter = 0;
        MelonLogger.Msg($"Beginning loop");
        foreach (CharacterData character in newActList)
        {
            Debug.Log(string.Format("Adding {0} to act order at array position {1}", character.name.ToString(), counter));
            newActArray[counter] = character;
            counter += 1;
        }
        return newActArray;
    }
    public static Il2CppSystem.Collections.Generic.List<CharacterData> JinxCharacter(Il2CppSystem.Collections.Generic.List<CharacterData> inputList, string ID)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> outputList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in inputList)
        {
            if (character.characterId != ID)
            {
                outputList.Add(character);
            }
        }
        return outputList;
    }
    public void OnFirstUpdate()
    {
        PatchVanillaCharacterDescriptions();
        wx_SavedScripts sharedScripts = new();
        Transform chars = GameObject.Find("Game/Gameplay/Content/Canvas/Panel/Characters").transform;
        if (chars)
        {
            sharedScripts.DebugMessage("Found chars transform");
        }
        else
        {
            sharedScripts.DebugMessage("Didn't find chars transform, expect an error");
        }
        for (int i = 12; i < 51; i++)
        {
            sharedScripts.DebugMessage($"Creating circle of size {i}");
            Statics.checkCreateCircle(chars, i);
        }
        for (int j = 2; j < 5; j++)
        {
            sharedScripts.DebugMessage($"Creating circle of size {j}");
            Statics.checkCreateCircle(chars, j);
        }
    }

    public static class HiddenRoleStatus
    {
        public static ECharacterStatus hiddenRole = (ECharacterStatus)999;
    }



    public static CharacterData newCharacter(string name, EAlignment alignment, ECharacterType type, bool bluffable, bool usuallyDisguised, string flavour, string placeholderArtID)
    {



        Il2CppSystem.Collections.Generic.List<string> refIDs = new Il2CppSystem.Collections.Generic.List<string>();
        refIDs = GetRolePlaceholderArt(type, alignment);
        MelonLogger.Msg($"refIDs[0] = {refIDs[0]}");
        MelonLogger.Msg($"refIDs[1] = {refIDs[1]}");
        CharacterData backgroundRef = ProjectContext.Instance.gameData.GetCharacterDataOfId(refIDs[0]);
        CharacterData artRef = ProjectContext.Instance.gameData.GetCharacterDataOfId(placeholderArtID);
        if (artRef == null) artRef = ProjectContext.Instance.gameData.GetCharacterDataOfId(refIDs[1]);
        if (backgroundRef == null)
        {
            MelonLogger.Msg("backgroundRef is null! Resetting to Bishop...");
            backgroundRef = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        }
        if (artRef == null)
        {
            MelonLogger.Msg("artRef is null! Resetting to Bishop...");
            artRef = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        }
        MelonLogger.Msg($"backgroundRef = {backgroundRef.characterName}");
        MelonLogger.Msg($"artRef = {artRef.characterName}");




        CharacterData newCharacter = new CharacterData();
        //CharacterData bishopData = new CharacterData();
        //bishopData = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        //newCharacter.art = bishopData.art;
        //newCharacter.backgroundArt = bishopData.backgroundArt;
        //newCharacter.roguelikeInfo = bishopData.roguelikeInfo;

        MelonLogger.Msg("");
        MelonLogger.Msg($"Creating role {name} of type {type} and alignment {alignment}.");
        MelonLogger.Msg($"Name: {name}");
        newCharacter.name = name;
        newCharacter.characterName = name;
        MelonLogger.Msg($"Setting base desc...");
        newCharacter.description = "";
        MelonLogger.Msg($"Flavour: {flavour}");
        newCharacter.flavorText = flavour;
        newCharacter.hints = "";
        newCharacter.ifLies = "";
        newCharacter.picking = false;
        MelonLogger.Msg($"Alignment: {alignment.ToString()}");
        newCharacter.startingAlignment = alignment;
        MelonLogger.Msg($"Type: {type.ToString()}");
        newCharacter.type = type;
        MelonLogger.Msg($"Bluffable?: {bluffable.ToString()}");
        newCharacter.bluffable = bluffable;
        newCharacter.characterId = $"{name}_WING";
        newCharacter.artBgColor = getColour(type, alignment, "artBgColor");
        newCharacter.cardBgColor = getColour(type, alignment, "cardBgColor");
        newCharacter.cardBorderColor = getColour(type, alignment, "cardBorderColor");
        newCharacter.color = getColour(type, alignment, "color");
        MelonLogger.Msg($"Finished getting colours.");
        MelonLogger.Msg($"Usually Disguised?: {usuallyDisguised.ToString()}");
        newCharacter.usuallyDisguised = usuallyDisguised;
        newCharacter.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        newCharacter.additionalFlavorTexts[0] = flavour;

        newCharacter.bundledCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        newCharacter.additionalPossibleCharacters = new AddedCharacterTypes();

        newCharacter.art_cute = artRef.art_cute;
        newCharacter.backgroundArt = backgroundRef.backgroundArt;

        newCharacter.localization_key = $"WINGMOD_{name}";

        return newCharacter;
    }

    public static CharacterCount NewPossibleCharacterCount(ECharacterType type, int amount)
    {
        CharacterCount returnVal = new CharacterCount();
        returnVal.type = type;
        returnVal.count = amount;
        return returnVal;
    }

    public static Il2CppSystem.Collections.Generic.List<string> GetRolePlaceholderArt(ECharacterType type, EAlignment alignment) // First item of the list is the background, second is the art.
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new Il2CppSystem.Collections.Generic.List<string>();
        if (alignment == EAlignment.Good)
        {
            returnList.Add("Bishop_58855542");
        }
        else
        {
            returnList.Add("Minion_71804875");
        }
        if (type == ECharacterType.Villager)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Knight_47970624"); // Good Villager: Knight
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Gambler_42592744"); // Evil Villager: Slayer
            }
        }
        if (type == ECharacterType.Outcast)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Wretch_80988916"); // Good Outcast: Wretch
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Bombardier_79093372"); // Evil Outcast: Bombardier
            }
        }
        if (type == ECharacterType.Minion)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Witch_25286521"); // Good Minion: Witch
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Poisoner_64796285"); // Evil Minion: Poisoner
            }
        }
        if (type == ECharacterType.Demon)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Confessor_18741708"); // Good Demon: Confessor
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Lillith_90453844"); // Evil Demon: Lilis
            }
        }
        return returnList;
    }

    public static Color getColour(ECharacterType type, EAlignment alignment, string field)
    {
        // Type = character type
        // Alignment = character alignment
        // Field = "color" for text colour, "cardBgColor" for card background colour, "cardBorderColor" for the border colour and "artBgColor" for the art background colour.
        // In summary, field = "color", "cardBgColor", "cardBorderColor" or "artBgColor".
        Color returnColour = new Color(0, 0, 0);
        if (field == "artBgColor")
        {
            return getColour(type, alignment, "cardBorderColor");
        }
        if (type == ECharacterType.Villager)
        {
            if (alignment == EAlignment.Good)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1f, 0.9333f, 0.7294f);
                    case "cardBgColor": return new Color(0.2588f, 0.1529f, 0.3411f);
                    case "cardBorderColor": return new Color(0.7137f, 0.3372f, 0.8666f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.9098f, 0.7764f, 1f); // E8C6FF
                    case "cardBgColor": return new Color(0.1647f, 0.1058f, 0.2f); // 2A1B33
                    case "cardBorderColor": return new Color(0.6078f, 0.1843f, 0.6823f); // 9B2FAE
                }
            }
        }
        if (type == ECharacterType.Outcast)
        {
            if (alignment == EAlignment.Good)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.9647f, 1, 0.447f);
                    case "cardBgColor": return new Color(0.1019f, 0.0666f, 0.0392f);
                    case "cardBorderColor": return new Color(0.7843f, 0.6470f, 0);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1, 0.6666f, 0.9568f); // E8C6FF
                    case "cardBgColor": return new Color(0.2509f, 0, 0.2156f); // 2A1B33
                    case "cardBorderColor": return new Color(1, 0, 0.8666f); // FF00DD
                }
            }
        }
        if (type == ECharacterType.Minion)
        {
            if (alignment == EAlignment.Evil)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.8509f, 0.4549f, 0);
                    case "cardBgColor": return new Color(0.094f, 0.0431f, 0.04313f);
                    case "cardBorderColor": return new Color(0.8196f, 0, 0.0235f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.7882f, 1, 0.9490f); // E8C6FF
                    case "cardBgColor": return new Color(0.0588f, 0.1647f, 0.1647f); // 2A1B33
                    case "cardBorderColor": return new Color(0.2f, 0.8196f, 0.7764f); // 33D1C6
                }
            }
        }
        if (type == ECharacterType.Demon)
        {
            if (alignment == EAlignment.Evil)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1, 0.3803f, 0.3803f);
                    case "cardBgColor": return new Color(0.0941f, 0.0431f, 0.0431f);
                    case "cardBorderColor": return new Color(0.8196f, 0, 0.0235f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1f, 0.9607f, 0.8784f); // E8C6FF
                    case "cardBgColor": return new Color(0.1019f, 0.0588f, 0.1803f); // 2A1B33
                    case "cardBorderColor": return new Color(0.4784f, 0.3607f, 1f); // 7A5CFF
                }
            }
        }
        return returnColour;
    }



    public void PatchVanillaCharacterDescriptions()
    {
        GameObject content = GameObject.Find("Game/Gameplay/Content");
        NightPhase nightPhase = content.GetComponent<NightPhase>();
        bool instantPick = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("InstantPick").Value;
        bool reallyInteresting = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("RoleFlavour").Value;
        w_zRoleBase configChecker = new();
        for (int i = 0; i < allDatas.Count(); i++)
        {
            MelonLogger.Msg($"Role Patcher: Found {allDatas[i].name.ToString()}");
            if (allDatas[i].characterId == "Alchemist_94446803")
            {
                allDatas[i].role = new w_vAlchemistRevamped();
                allDatas[i].hints = "";
                allDatas[i].description = "<b>Game Start:</b>\nVillagers within [Range 2] of me are Cured of Corruption.\n\nLearn how many characters within [Range 2] of me were or still are Corrupted.\n\nI can't be Corrupted.";
            }

            if (allDatas[i].characterId == "Architect_39883285")
            {
                allDatas[i].role = new w_vArchitectRevamped();
                allDatas[i].description = "Learn whether the left or right half of the circle contains more Evil characters, or that both have the same amount.";
                allDatas[i].hints = "The characters in the exact middle of the circle count as being on both sides of it.";
            }

            if (allDatas[i].characterId == "Athlete_95133291")
            {
                allDatas[i].role = new w_vBardRevamp();
                allDatas[i].ifLies = "";
            }

            if (allDatas[i].characterId == "Baker_22847064")
            {
                allDatas[i].role = new w_vBakerRevamp();
                allDatas[i].description = "<b>Reveal:</b>\n" +
                    "1 random Unrevealed Good Villager becomes a <color=#B656DD>Baker</color>.\n" +
                    "Learn what my previous Villager Role was, or that I am the start of my chain.";
                allDatas[i].ifLies = "I tell you that I was a random incorrect Villager Role.\nI never say that I am the start of my chain.";
                allDatas[i].hints = "Keep in mind that my ability can chain. Each new <color=#B656DD>Baker</color> can turn another Villager into a <color=#B656DD>Baker</color>, and so on, so forth.";
            }

            if (allDatas[i].characterId == "Bishop_58855542")
            {
                allDatas[i].role = new w_vBishopRevamp();
                allDatas[i].description = "Learn up to 3 characters.\nAmong them are 1 Villager, 1 Outcast and 1 Minion or Demon, if possible.";
                allDatas[i].ifLies = "";
                allDatas[i].hints = "I specify which Evil Type I Learn in my info.";
            }

            if (allDatas[i].characterId == "Confessor_18741708")
            {
                if (!configChecker.CheckConfigOption_Bool("AlternateConfessions"))
                {
                    allDatas[i].role = new w_vConfessorRevamp();
                    allDatas[i].description = "If Evil or Corrupted, I Declare that \"I am dizzy\"\nOtherwise, I Declare that \"I am Good\"\n\nI am always Truthful, even if Corrupted or Evil.";
                }
                else
                {
                    allDatas[i].role = new w_vConfessorAlternate();
                    allDatas[i].description = "Learn that \"I am the Confessor\".";
                    allDatas[i].ifLies = "I tell you something else instead.";
                    allDatas[i].hints = "If a Truthful character Disguises as me, they will still say \"I am the Confessor\" regardless of their True Role.";
                }
                allDatas[i].hints = "";
            }

            if (allDatas[i].characterId == "Dreamer_32014895")
            {
                allDatas[i].role = new w_vDreamerRevamp();
                allDatas[i].description = "<b>Pick 2 characters:</b>\nLearn a Role that's among them and a Role that isn't.\n\nI am a Sleepwalker.";
            }

            if (allDatas[i].characterId == "Druid_89845092")
            {
                allDatas[i].role = new w_vDruidRevamp();
                allDatas[i].description = "<b>Pick 3 characters:</b>\nLearn 1 random Outcast Role among them, if any.";
                allDatas[i].hints = "The <color=#C8A500>Wretch</color> Registers as a Minion to me, so I cannot pick up on his presence.";
            }

            if (allDatas[i].characterId == "Empress_13782227")
            {
                allDatas[i].role = new w_vEmpressRevamp();
                allDatas[i].ifLies = "";
                allDatas[i].description = "Learn 3 characters.\nExactly one of them is Evil.";
            }

            if (allDatas[i].characterId == "Enlightened_62576217")
            {
                allDatas[i].role = new w_vEnlightenedRevamp();
            }

            if (allDatas[i].characterId == "Fortune Teller_74565681")
            {
                allDatas[i].role = new w_vFortuneTellerRevamp();
                allDatas[i].description = "<b>Pick 2 characters:</b>\nLearn if either of them are Evil.";
            }

            if (allDatas[i].characterId == "Archivist_34476114")
            {
                allDatas[i].role = new w_vGemcrafterRevamp();
            }

            if (allDatas[i].characterId == "Hunter_93427887")
            {
                allDatas[i].role = new w_vHunterRevamp();
                allDatas[i].description = "Learn the distance from me to my closest Evil character.";
                allDatas[i].ifLies = "";
            }

            if (allDatas[i].characterId == "Investigator_34015277")
            {
                allDatas[i].role = new w_vInvestigatorRevamp();
            }

            if (allDatas[i].characterId == "Jester_41367606")
            {
                allDatas[i].role = new w_vJesterRevamp();
            }

            if (allDatas[i].characterId == "Judge_87202475")
            {
                allDatas[i].role = new w_vJudgeRevamp();
            }

            if (allDatas[i].characterId == "Knight_47970624")
            {
                allDatas[i].role = new w_vKnightRevamp();
                allDatas[i].hints = "";
                if (reallyInteresting) allDatas[i].description = "I can't Die.\n\nI tell you something really interesting.";
            }

            if (allDatas[i].characterId == "Knitter_32352172")
            {
                allDatas[i].role = new w_vKnitterRevamp();
                allDatas[i].description = "Learn how many pairs of Evil there are.";
            }

            if (allDatas[i].characterId == "Lover_91302708")
            {
                allDatas[i].role = new w_vLoverRevamp();
            }

            if (allDatas[i].characterId == "Lookout_41018246")
            {
                allDatas[i].role = new w_vMediumRevamp();
                allDatas[i].ifLies = "I point to a character whose claim doesn't match what they Register as, and attempt to back up their claim.";
                allDatas[i].description = "Learn a Good character and their Role.";
            }

            if (allDatas[i].characterId == "Oracle_07039445")
            {
                allDatas[i].role = new w_vOracleRevamp();
                allDatas[i].description = "Learn that exactly 1 of 2 characters is a particular Minion role.";
                allDatas[i].ifLies = "";
            }

            if (allDatas[i].characterId == "Gossip_85354100")
            {
                allDatas[i].role = new w_vPoetRevamp();
            }

            if (allDatas[i].characterId == "Scout_88081716")
            {
                allDatas[i].role = new w_vScoutRevamp();
                allDatas[i].description = "Learn an in-play Evil role and the distance from them to their nearest other Evil character.";
                allDatas[i].hints = "If there are multiple of the same Evil role in-play, I Learn the lowest distance.";
            }

            if (allDatas[i].characterId == "Gambler_42592744")
            {
                allDatas[i].role = new w_vSlayerRevamp();
                allDatas[i].ifLies = "I always fail to kill my target.";
            }

            if (allDatas[i].characterId == "Witness_25155076")
            {
                allDatas[i].role = new w_vWitnessRevamp();
            }


            if (instantPick)
            {
                if (allDatas[i].characterName == "Fortune Teller")
                {
                    allDatas[i].description = "Learn 2 characters and whether or not either are Evil.";
                    allDatas[i].role = new w_vFortuneTellerLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Jester")
                {
                    allDatas[i].description = "Learn 3 characters and how many of them are Evil.";
                    allDatas[i].role = new w_vJesterLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Judge")
                {
                    allDatas[i].description = "Learn whether or not a particular character is Lying.";
                    allDatas[i].role = new w_vJudgeLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Dreamer")
                {
                    allDatas[i].description = "Learn 2 characters & 2 roles:\nOne role is among them, one isn't.\n\nMy Ability activates again after every 4th Reveal.";
                    allDatas[i].hints = $"I act <i>before</i> any Night abilities.\n\n{allDatas[i].hints}";
                    allDatas[i].role = new w_vDreamerLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Druid")
                {
                    allDatas[i].description = "Learn 3 characters and a random Outcast role among them, if any.";
                    allDatas[i].role = new w_vDruidLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Slayer")
                {
                    allDatas[i].description = "Learn a character. If they're Evil, I Kill them.";
                    allDatas[i].hints = "I will never Learn & Execute the Wretch.";
                    allDatas[i].ifLies = "I will always fail to kill my target.";
                    allDatas[i].role = new w_vSlayerLearn();
                    allDatas[i].picking = false;
                }
                if (allDatas[i].characterName == "Investigator")
                {
                    allDatas[i].description = "Learn 2 characters and what the most Suspicious type among them is.";
                    allDatas[i].role = new w_vInvestigatorLearn();
                    allDatas[i].picking = false;
                }
            }





            if (allDatas[i].characterId == "Bombardier_79093372")
            {
                allDatas[i].role = new w_oBombardierRevamp();
                if (reallyInteresting) allDatas[i].description = "Lose if you Execute me.\n\nI tell you something really interesting.";
            }

            if (allDatas[i].characterId == "Doppleganger_52694042")
            {
                bool doppelOutsDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("DoppelOutcastDisguise").Value;
                allDatas[i].role = new w_oDoppelgangerRevamped();
                allDatas[i].description = "I Disguise as an in-play Good";
                if (!doppelOutsDisguise) allDatas[i].description += " Villager";
                allDatas[i].description += " Role.";
                allDatas[i].hints = "";
            }

            if (allDatas[i].characterId == "Drunk_15369527")
            {
                bool drunkOutsDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("DrunkOutcastDisguise").Value;
                allDatas[i].role = new w_oDrunkRevamped();
                allDatas[i].description = "I Lie and Disguise as a not-in-play Good";
                if (!drunkOutsDisguise) allDatas[i].description += " Villager";
                allDatas[i].description += " Role.\nI am Corrupted and cannot be Cured.";
                allDatas[i].hints = "";

                int drunkDamage = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<int>("DrunkDamage").Value;
                if (drunkDamage != 5) allDatas[i].hints = $"If Executed, I deal {drunkDamage} Damage instead of 5.";
            }

            if (allDatas[i].characterId == "Lycanthrope_16077432")
            {
                bool lycanOutcastDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanOutcastDisguise").Value;
                bool lycanDoubleClaim = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanDoubleClaim").Value;
                allDatas[i].role = new w_oLycanthropeRevamped();
                allDatas[i].description = "<b>At Night:</b>\nIf Unrevealed, I become an Evil <link=\"LycanthropeWerewolfLink\"><color=#D97400>Werewolf</color></link>." +
                    "\n\nI Disguise";
                if (!lycanDoubleClaim || !lycanOutcastDisguise)
                {
                    allDatas[i].description += " as a random";
                    if (!lycanDoubleClaim) allDatas[i].description += " not-in-play";
                    allDatas[i].description += " Good";
                    if (!lycanOutcastDisguise) allDatas[i].description += " Villager";
                    allDatas[i].description += " Role";
                }
                allDatas[i].description += ".";
                allDatas[i].bundledCharacters.Add(ProjectContext.Instance.gameData.GetCharacterDataOfId("Werewolf_78350415"));
            }
            if (allDatas[i].characterId == "Werewolf_78350415")
            {
                bool lycanOutcastDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanOutcastDisguise").Value;
                bool lycanDoubleClaim = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanDoubleClaim").Value;
                // allDatas[i].role = new w_mWerewolfRevamped();
                allDatas[i].description = "<b>At Night:</b>\nDeal 1 damage to you." +
                    "\n\nI Lie and Disguise";
                if (!lycanDoubleClaim || !lycanOutcastDisguise)
                {
                    allDatas[i].description += " as a random";
                    if (!lycanDoubleClaim) allDatas[i].description += " not-in-play";
                    allDatas[i].description += " Good";
                    if (!lycanOutcastDisguise) allDatas[i].description += " Villager";
                    allDatas[i].description += " Role";
                }
                allDatas[i].description += ".";
                allDatas[i].hints = "I am never in-play naturally.\nI can only be in-play if the <color=#C8A500>Lycanthrope</color> transforms into me.";
                allDatas[i].bundledCharacters.Add(ProjectContext.Instance.gameData.GetCharacterDataOfId("Lycanthrope_16077432"));
            }

            if (allDatas[i].characterId == "Plague Doctor_49312486")
            {
                allDatas[i].role = new w_oPlagueDoctorRevamp();
                allDatas[i].description = "<b>Game Start:</b>\nOne Good Villager is Corrupted.\n\n<b>Pick 1 character:</b>\nIf they're Corrupted, Learn an Evil character.";
                allDatas[i].hints = "";
                allDatas[i].ifLies = "I tell you that a Corrupted character is Pure, or that a Pure character is Corrupted and that a Good character is Evil.";
            }

            if (allDatas[i].characterId == "Rambler_13041651")
            {
                allDatas[i].role = new w_oRamblerRevamped();
            }
            
            if (allDatas[i].characterId == "Rambler_57930131")
            {
                allDatas[i].role = new w_oRamblerRevamped();
            }

            if (allDatas[i].characterId == "Wretch_80988916")
            {
                allDatas[i].role = new w_oWretchRevamped();
                allDatas[i].description = "I Register as a random Evil Minion Role.";
                if (reallyInteresting) allDatas[i].description += "\n\nI tell you something really interesting.";
                allDatas[i].hints = "I cannot be Disguised as.\n\nWhen a character checks me, they will..." +
                    "\n...see my Alignment as Evil." +
                    "\n...see my Type as Minion." +
                    "\n...see my Role as a random Minion role." +
                    "\n\nI only fool characters who check Alignment, Type and Role. Everything else, including but not limited to my position, Truthfulness, Honesty and Purity, Registers correctly.";
            }





            if (allDatas[i].characterId == "Minion_71804875")
            {
                allDatas[i].role = new w_mMinionRevamped();
                int truthChance = configChecker.CheckConfigOption_Int("MinionTruthChance");
                bool uniquity = configChecker.CheckConfigOption_Bool("MinionUniquity");
                allDatas[i].description = "I";
                if (truthChance == 0) allDatas[i].description += " Lie and";
                allDatas[i].description += " Disguise";
                if (uniquity) allDatas[i].description += " as a not-in-play Good Role";
                allDatas[i].description += ".";
                if (truthChance != 0 && truthChance != 100) allDatas[i].description += $"\nI Lie {100 - truthChance}% of the time.\nI tell the Truth {truthChance}% of the time.";
            }
            if (allDatas[i].characterId == "Twin Minion_15695218")
            {
                allDatas[i].role = new w_mTwinMinion_Revamped();
                int truthChance = configChecker.CheckConfigOption_Int("TwinionTruthChance");
                bool duplicity = configChecker.CheckConfigOption_Bool("TwinionDuplicity");
                if (truthChance == 0 & !duplicity) allDatas[i].description = "I Disguise and Lie.";
                else
                {
                    allDatas[i].description = "I";
                    if (truthChance == 0) allDatas[i].description += " Lie and";
                    allDatas[i].description += " Disguise";
                    if (duplicity) allDatas[i].description += " as an in-play Good Role";
                    allDatas[i].description += ".";
                    if (truthChance != 0 && truthChance != 100) allDatas[i].description += $"\nI Lie {100 - truthChance}% of the time.\nI tell the Truth {truthChance}% of the time.";
                }
            }

            if (allDatas[i].characterId == "Baron_04539999")
            {
                allDatas[i].role = new w_mChancellorRevamped();
                allDatas[i].hints = "";
            }

            if (allDatas[i].characterId == "Puppet_15989619")
            {
                // allDatas[i].role doesn't need to change, Puppet's fine as-is.
                allDatas[i].description = "I Disguise as the Good Villager Role I used to be.";
                allDatas[i].hints = "I can only be in-play if created by the <color=#D97400>Puppeteer</color>." +
                    "\n\nThe Role I used to be is considered not-in-play to anyone that cares about it." +
                    "\n\nIf Disguised as the <color=#B656DD>Baker</color>, I say 'I was the Baker' and do not turn anyone else into a new <color=#B656DD>Baker</color>.";
                // allDatas[i].bundledCharacters.Add(ProjectContext.Instance.gameData.GetCharacterDataOfId("Mezepheles_09511163"));
            }

            if (allDatas[i].characterId == "Poisoner_64796285")
            {
                bool poisExtRange = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("PoisonerExtendedRange").Value;
                allDatas[i].role = new w_mPoisonerRevamped();
                allDatas[i].description = "<b>Game Start:</b>\nOne Good ";
                if (poisExtRange) allDatas[i].description += "Pure Villager closest";
                else allDatas[i].description += "Villager adjacent";
                allDatas[i].description += " to me is Corrupted, if possible.\n\nI Lie and Disguise.";
                if (poisExtRange) allDatas[i].hints = "My ability activates <i>after</i> <color=#FF6161>Pooka</color>'s.";
            }

            if (allDatas[i].characterId == "Mezepheles_09511163") // ABANDON HOPE ALL YE WHO ENTER, ATTEMPTING TO EDIT THE PUPPETEER FOR SOME FUCKING REASON CAUSES THE ENTIRE VILLAGE TO BREAK WHENEVER A PUPPETEER GENERATES
            // THEIR POWER LEVEL IS BEYOND OUR UNDERSTANDING, THEY SHATTER REALITY AT THE SEAMS!
            {
                allDatas[i].role = new w_mPuppeteerRevamped();
                allDatas[i].description = "<b>Game Start:</b>\n" +
                    "1 Good Villager adjacent to me becomes the <link=PuppeteerPuppetLink><color=#D97400>Puppet</color></link>, if possible." +
                    "\n\nI Lie and Disguise.";
            }
            if (allDatas[i].characterId == "Shaman_26945607")
            {
                allDatas[i].role = new w_mShamanRevamped();
                allDatas[i].description = "<b>Game Start:</b>" +
                    "\n1 Good Villager becomes a clone of another in-play Good Villager Role." +
                    "\n\nI Lie and Disguise.";
            }

            if (allDatas[i].characterId == "Witch_25286521")
            {
                allDatas[i].description = "The last Unrevealed character is Cursed while I am Alive." +
                    "\n\nI Lie and Disguise.";
                allDatas[i].hints = "";
            }




            if (allDatas[i].characterId == "Imp_58992273")
            {
                int hiddenOutcasts = configChecker.CheckConfigOption_Int("BaaHiddenOutcasts");
                int hiddenMinions = configChecker.CheckConfigOption_Int("BaaHiddenMinions");
                int addOutcastsMin = configChecker.CheckConfigOption_Int("BaaAddOutcastsMin");
                int addOutcastsMax = configChecker.CheckConfigOption_Int("BaaAddOutcastsMax");
                int addMinionsMin = configChecker.CheckConfigOption_Int("BaaAddMinionsMin");
                int addMinionsMax = configChecker.CheckConfigOption_Int("BaaAddMinionsMax");
                bool revealDeath = configChecker.CheckConfigOption_Bool("BaaRevealDeath");
                bool hideAllOutcasts = hiddenOutcasts >= 4;
                bool hideAllMinions = hiddenMinions >= 4;

                allDatas[i].role = new w_dBaaRevamped();
                allDatas[i].description = "I Lie and Disguise";
                if (hiddenOutcasts != 0 || hiddenMinions != 0 || addOutcastsMax != 0 || addMinionsMax != 0)
                {
                    allDatas[i].description = "<b>Game Start</b>:\n";
                    if (addOutcastsMax != 0)
                    {
                        if (addOutcastsMin != addOutcastsMax) allDatas[i].description += $"{addOutcastsMin}-{addOutcastsMax} fake Outcasts";
                        else
                        {
                            allDatas[i].description += $"{addOutcastsMax} fake Outcast";
                            if (addOutcastsMax != 1) allDatas[i].description += "s";
                        }
                        if (addMinionsMax != 0) allDatas[i].description += " and ";
                    }
                    if (addMinionsMax != 0)
                    {
                        if (addMinionsMin != addMinionsMax) allDatas[i].description += $"{addMinionsMin}-{addMinionsMax} fake Minions";
                        else
                        {
                            allDatas[i].description += $"{addMinionsMax} fake Minion";
                            if (addMinionsMax != 1) allDatas[i].description += "s";
                        }
                    }
                    if (addOutcastsMax + addMinionsMax != 0)
                    {
                        if (addOutcastsMax + addMinionsMax == 1) allDatas[i].description += " is";
                        else allDatas[i].description += " are";
                        allDatas[i].description += " added to the Deck.\n";
                    }



                    if (hiddenOutcasts != 0 || hiddenMinions != 0) allDatas[i].description += "Hide ";



                    if (hideAllOutcasts)
                    {
                        allDatas[i].description += "all Outcast roles";
                        if (hiddenMinions != 0) allDatas[i].description += " and ";
                    }
                    else if (hiddenOutcasts != 0)
                    {
                        allDatas[i].description += $"{hiddenOutcasts} Outcast role";
                        if (hiddenOutcasts != 1) allDatas[i].description += "s";
                        if (hiddenMinions != 0) allDatas[i].description += " and ";
                    }

                    if (hiddenMinions != 0)
                    {
                        if (hideAllMinions) allDatas[i].description += "all Minion roles";
                        else
                        {
                            allDatas[i].description += $"{hiddenMinions} Minion role";
                            if (hiddenMinions != 1) allDatas[i].description += "s";
                        }
                    }

                    if (hiddenOutcasts != 0 || hiddenMinions != 0)
                    {
                        allDatas[i].description += " from the Deck.\n";
                        if (revealDeath)
                        {
                            allDatas[i].description += "\n<b>On Death:</b>\nReveal all roles I hid from the Deck.\n";
                        }
                    }


                }
                allDatas[i].description += "\nI Lie and Disguise.";
                allDatas[i].hints = "";
                if (hiddenOutcasts != 0 && !hideAllOutcasts) allDatas[i].hints = "I prioritise hiding Outcast roles that usually Disguise.";
            }
            if (allDatas[i].characterId == "Lillith_90453844")
            {
                int damage = configChecker.CheckConfigOption_Int("LilisDamage");
                int revealedDamage = configChecker.CheckConfigOption_Int("LilisDamage") + configChecker.CheckConfigOption_Int("LilisRevealedDamageBonus");
                bool evilAttack = configChecker.CheckConfigOption_Bool("LilisEvilAttack");
                bool selfAttack = configChecker.CheckConfigOption_Bool("LilisSelfAttack");
                bool killRevealed = configChecker.CheckConfigOption_Bool("LilisKillRevealed");

                allDatas[i].role = new w_dLilisRevamped();
                allDatas[i].description = "<b>At Night:</b>\nKill a random";
                if (!killRevealed) allDatas[i].description += " unrevealed";
                if (!evilAttack) allDatas[i].description += " Good";
                allDatas[i].description += " character.";
                if (!killRevealed || damage == revealedDamage)
                {
                    if (damage != 0) allDatas[i].description += $"\nDeal {damage} damage to you.";
                    if (killRevealed) allDatas[i].description += $"\nIf they were Revealed, Unmask them.";
                }
                else
                {
                    if (damage != 0) allDatas[i].description += $"\nIf they are Unrevealed, deal {damage} damage to you.";
                    if (revealedDamage != 0) allDatas[i].description += $"\nIf they are Revealed, Unmask them and deal {revealedDamage} damage to you.";
                }
                allDatas[i].description += "\n\nI Lie and Disguise.";
                allDatas[i].hints = "";
                if (evilAttack) allDatas[i].hints = "I prioritise killing Good characters, but my only valid targets are Evil, I can attack them too.";
                if (!selfAttack && evilAttack) allDatas[i].hints += "\n\nI will never attack myself, even if I am my only valid target.";
            }
            if (allDatas[i].characterId == "Pooka_13445289")
            {
                allDatas[i].role = new w_dPookaRevamped();
                allDatas[i].description = "<b>Game Start:</b>\n";
                if (configChecker.CheckConfigOption_Bool("PookaExtendedRange")) allDatas[i].description += "The closest Good Villager on each side of me is Corrupted.";
                else allDatas[i].description += "Good Villagers adjacent to me are Corrupted.";
                allDatas[i].description += "\n\nI Lie and Disguise.";
            }
        }
    }
























    /*
    [HarmonyPatch(typeof(Gossip), nameof(Gossip.Act))]
    private static class GetPoetTrueInfo
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Gossip_85354100")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Gossip_85354100")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Role> infoRoles = new Il2CppSystem.Collections.Generic.List<Role>();
            infoRoles.Add(new Empath());
            infoRoles.Add(new Scout());
            infoRoles.Add(new Investigator());
            infoRoles.Add(new BountyHunter());
            infoRoles.Add(new Lookout());
            infoRoles.Add(new Knitter());
            infoRoles.Add(new Tracker());
            infoRoles.Add(new Shugenja());
            infoRoles.Add(new Noble());
            infoRoles.Add(new Bishop());
            infoRoles.Add(new Archivist());
            infoRoles.Add(new Acrobat2());
            infoRoles.Add(new w_Arithmetician());
            infoRoles.Add(new w_Chiromancer());
            infoRoles.Add(new w_Clairvoyant());
            infoRoles.Add(new w_Detective());
            infoRoles.Add(new w_Introvert());
            infoRoles.Add(new w_Jewelsmith());
            infoRoles.Add(new w_Lamb());
            infoRoles.Add(new w_Prince());
            infoRoles.Add(new w_Ranger());
            infoRoles.Add(new w_Sentinel());
            infoRoles.Add(new w_Sheriff());
            infoRoles.Add(new w_Spy());
            ActedInfo myInfo = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)].GetInfo(charRef);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }


    [HarmonyPatch(typeof(Gossip), nameof(Gossip.BluffAct))]
    private static class GetPoetFalseInfo
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Gossip_85354100")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Gossip_85354100")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Role> infoRoles = new Il2CppSystem.Collections.Generic.List<Role>();
            infoRoles.Add(new Empath());
            infoRoles.Add(new Scout());
            infoRoles.Add(new Investigator());
            infoRoles.Add(new BountyHunter());
            infoRoles.Add(new Lookout());
            infoRoles.Add(new Knitter());
            infoRoles.Add(new Tracker());
            infoRoles.Add(new Shugenja());
            infoRoles.Add(new Noble());
            infoRoles.Add(new Bishop());
            infoRoles.Add(new Archivist());
            infoRoles.Add(new Acrobat2());
            infoRoles.Add(new w_Arithmetician());
            infoRoles.Add(new w_Chiromancer());
            infoRoles.Add(new w_Clairvoyant());
            infoRoles.Add(new w_Detective());
            infoRoles.Add(new w_Introvert());
            infoRoles.Add(new w_Jewelsmith());
            infoRoles.Add(new w_Lamb());
            infoRoles.Add(new w_Prince());
            infoRoles.Add(new w_Ranger());
            infoRoles.Add(new w_Sentinel());
            infoRoles.Add(new w_Sheriff());
            infoRoles.Add(new w_Spy());
            ActedInfo myInfo = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)].GetBluffInfo(charRef);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }
    */


    /* Was causing crashes.
    [HarmonyPatch(typeof(Investigator), nameof(Investigator.BluffAct))]
    private static class GetOracleFalseInfo // Practically identical, save for the fact that it can't see Good Minions. Should fix problems with Good Swarm.
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Oracle_07039445")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Oracle_07039445")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Character> possibleInfoTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> infoTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<CharacterData> deckMinions = Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion);
            CharacterData chosenMinion = new CharacterData();
            if (deckMinions.Count == 0)
            {
                foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
                {
                    if (character.type == ECharacterType.Minion)
                    {
                        deckMinions.Add(character);
                    }
                }
            }
            chosenMinion = deckMinions[UnityEngine.Random.RandomRangeInt(0, deckMinions.Count)];
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Good && character.GetCharacterType() != ECharacterType.Minion)
                {
                    possibleInfoTargets.Add(character);
                }
            }
            string actInfo = "";
            if (possibleInfoTargets.Count < 2)
            {
                actInfo = "This village confuses me.";
            }
            infoTargets.Add(possibleInfoTargets[UnityEngine.Random.RandomRangeInt(0, possibleInfoTargets.Count)]);
            possibleInfoTargets.Remove(infoTargets[0]);
            infoTargets.Add(possibleInfoTargets[UnityEngine.Random.RandomRangeInt(0, possibleInfoTargets.Count)]);

            if (infoTargets[0].id < infoTargets[1].id)
            {
                actInfo = string.Format("#{0} or #{1} is a {2}", infoTargets[0].id, infoTargets[1].id, chosenMinion.name.ToString());
            }
            else
            {
                actInfo = string.Format("#{0} or #{1} is a {2}", infoTargets[1].id, infoTargets[0].id, chosenMinion.name.ToString());
            }
            ActedInfo myInfo = new ActedInfo(actInfo, infoTargets);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }
    */




    public static class Statics
    {
        public static Dictionary<string, CharacterData> roles = new Dictionary<string, CharacterData>();
        public static CharacterData[] charactersArray = Il2CppSystem.Array.Empty<CharacterData>();

        public static void checkCreateCircle(Transform parent, int size)
        {
            string name = "Circle_" + size;
            Transform t = parent.FindChild(name);
            if (t != null)
            {
                MelonLogger.Msg("Object Already exists!: " + name);
                return;
            }
            CreateCircle(size);
        }
        /*
        public static GameObject createCircle(int size) // I'm just gonna wait for WWW to figure this out
        {
            GameObject circle = new GameObject();
            circle.name = "Circle_" + size;
            circle.transform.SetParent(Characters.Instance.gameObject.transform);
            RectTransform rect = circle.AddComponent<RectTransform>();
            CharactersPool circPool = circle.AddComponent<CharactersPool>();
            GameObject circ6 = Characters.Instance.gameObject.transform.Find("Circle_6").gameObject;
            CharactersPool circ6Pool = circ6.GetComponent<CharactersPool>();
            circPool.characterPrefab = circ6Pool.characterPrefab;
            circPool.characters = new Character[0];
            circPool.cardPlaceHolders = new CardPlaceholder[size];
            for (int i = 0; i < size; i++)
            {
                GameObject cardHolder = new GameObject();
                cardHolder.transform.SetParent(circle.transform);
                string name = "CardPlaceholder";
                if (i > 0)
                {
                    name += " (" + i + ")";
                }
                cardHolder.name = name;
                RectTransform cardRect = cardHolder.AddComponent<RectTransform>();
                cardRect.anchoredPosition3D = new Vector3(0f, 0f, 0f);
                CardPlaceholder placeholder = cardHolder.AddComponent<CardPlaceholder>();
                int angle = i * 360 / size;
                if (angle <= 30)
                {
                    placeholder.actedSide = EActedSide.Down;
                }
                else if (angle <= 149)
                {
                    placeholder.actedSide = EActedSide.Left;
                }
                else if (angle <= 210)
                {
                    placeholder.actedSide = EActedSide.Up;
                }
                else if (angle <= 329)
                {
                    placeholder.actedSide = EActedSide.Right;
                }
                else
                {
                    placeholder.actedSide = EActedSide.Down;
                }
                circPool.cardPlaceHolders[i] = placeholder;
            }
            circle.transform.position = new UnityEngine.Vector3(0f, 1f, 85.9444f);
            circle.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            circle.SetActive(false);
            addToCharsPool(circPool);
            return circle;
        }
        */
        public static void addToCharsPool(CharactersPool pool)
        {
            CharactersPool[] pools = Characters.Instance.characterPool;
            CharactersPool[] newPools = new CharactersPool[pools.Length + 1];
            for (int i = 0; i < pools.Length; i++)
            {
                newPools[i] = pools[i];
            }
            newPools[pools.Length] = pool;
            Characters.Instance.characterPool = newPools;
        }

        public static void GetStartingRoles()
        {
            AscensionsData allCharactersAscension = ProjectContext.Instance.gameData.allCharactersAscension;
            foreach (CharacterData data in allCharactersAscension.startingTownsfolks)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingOutsiders)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingMinions)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingDemons)
            {
                CheckAddRole(data);
            }
        }
        public static void CheckAddRole(CharacterData data)
        {
            string name = data.name;
            if (!roles.ContainsKey(name))
            {
                roles.Add(name, data);
            }
        }

    }


    public static GameObject CreateCircle(int size)
    {
        GameObject circle = new GameObject();
        circle.name = "Circle_" + size;
        circle.transform.SetParent(Characters.Instance.gameObject.transform);
        RectTransform rt = circle.AddComponent<RectTransform>();
        CharactersPool cp = circle.AddComponent<CharactersPool>();
        GameObject gameObject = Characters.Instance.gameObject.transform.Find("Circle_6").gameObject;
        CharactersPool component = gameObject.GetComponent<CharactersPool>();
        cp.characterPrefab = component.characterPrefab;
        cp.characters = System.Array.Empty<Character>();
        cp.cardPlaceHolders = new CardPlaceholder[size];
        for (int i = 0; i < size; i++)
        {
            GameObject card = new GameObject();
            card.transform.SetParent(circle.transform);
            string text = "CardPlaceholder";
            if (i > 0)
            {
                text = text + " (" + i + ")";
            }
            card.name = text;
            RectTransform card_rt = card.AddComponent<RectTransform>();
            card_rt.anchoredPosition3D = new Vector3(0f, 0f, 0f);
            CardPlaceholder cardPlaceholder = card.AddComponent<CardPlaceholder>();
            int num = i * 360 / size;
            if (num <= 30)
            {
                cardPlaceholder.actedSide = EActedSide.Down;
            }
            else if (num <= 149)
            {
                cardPlaceholder.actedSide = EActedSide.Left;
            }
            else if (num <= 210)
            {
                cardPlaceholder.actedSide = EActedSide.Up;
            }
            else if (num <= 329)
            {
                cardPlaceholder.actedSide = EActedSide.Right;
            }
            else
            {
                cardPlaceholder.actedSide = EActedSide.Down;
            }
            cp.cardPlaceHolders[i] = cardPlaceholder;
        }
        circle.transform.position = new Vector3(0f, 1f, 85.9444f);
        circle.transform.localScale = new Vector3(1f, 1f, 1f);
        circle.SetActive(false);
        addToCharsPool(cp);
        return circle;
    }
    public static void addToCharsPool(CharactersPool pool)
    {
        CharactersPool[] oldpool = Characters.Instance.characterPool;
        CharactersPool[] newPool = new CharactersPool[oldpool.Length + 1];
        for (int i = 0; i < oldpool.Length; i++)
        {
            newPool[i] = oldpool[i];
        }
        newPool[oldpool.Length] = pool;
        Characters.Instance.characterPool = newPool;
    }
    /*
    public MelonPreferences_Category randomInfoConfigCategory = null!;
    public void CreateRandomInfoConfigFile()
    {

        randomInfoConfigCategory = MelonPreferences.CreateCategory("WingModSettings_RandomInfo");
        randomInfoConfigCategory.CreateEntry("", 2, description: "");
        randomInfoConfigCategory.SetFilePath(Path.Combine(MelonEnvironment.UserDataDirectory, "WingModSettings_RandomInfo.cfg"));
        randomInfoConfigCategory.SaveToFile();
    }
    */
}