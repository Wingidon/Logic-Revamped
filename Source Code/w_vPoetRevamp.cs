using LogicRevampedMod;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vPoetRevamp : w_zRoleBase
{
    Il2CppSystem.Collections.Generic.List<Role> GetInfoRoles()
    {
        Il2CppSystem.Collections.Generic.List<Role> infoRoles = new();
        infoRoles.Add(new w_vLoverRevamp());
        infoRoles.Add(new w_vScoutRevamp());
        infoRoles.Add(new w_vOracleRevamp());
        infoRoles.Add(new w_vMediumRevamp());
        infoRoles.Add(new w_vKnitterRevamp());
        infoRoles.Add(new w_vHunterRevamp());
        infoRoles.Add(new w_vEnlightenedRevamp());
        infoRoles.Add(new w_vBishopRevamp());
        infoRoles.Add(new w_vGemcrafterRevamp());
        infoRoles.Add(new w_vBardRevamp());


        bool allowPickRoles = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("ExpandedPoet").Value;
        if (allowPickRoles)
        {
            infoRoles.Add(new w_vDreamerLearn());
            infoRoles.Add(new w_vDruidLearn());
            infoRoles.Add(new w_vFortuneTellerLearn());
            infoRoles.Add(new w_vInvestigatorLearn());
            infoRoles.Add(new w_vJesterLearn());
            infoRoles.Add(new w_vJudgeLearn());
        }
        else
        {
            infoRoles.Add(new w_vEmpressRevamp()); // Empress + Jester in the same pool is kinda pointless
        }

        bool allowModded = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("ModdedPoet").Value;
        if (allowModded)
        {
            infoRoles = AddModdedRoles(infoRoles);
        }
        return infoRoles;
    }
    public override string Description
    {
        get
        {
            return "Poet";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Role> infoRoles = GetInfoRoles();
        Role chosenRole = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)];
        new wx_SavedScripts().DebugMessage($"Poet chose {chosenRole.ToString()}");
        OnActed(ETriggerPhase.Day, charRef, chosenRole.GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Role> infoRoles = GetInfoRoles();
        Role chosenRole = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)];
        new wx_SavedScripts().DebugMessage($"Lying Poet chose {chosenRole.ToString()}");
        OnActed(ETriggerPhase.Day, charRef, chosenRole.GetBluffInfo(charRef));
    }
    public w_vPoetRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vPoetRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vPoetRevamp(System.IntPtr ptr) : base(ptr)
    {
    }




    Il2CppSystem.Collections.Generic.List<Role> AddModdedRoles(Il2CppSystem.Collections.Generic.List<Role> prevList)
    {
        Il2CppSystem.Collections.Generic.List<Role> returnList = prevList;
        Il2CppSystem.Collections.Generic.List<string> validIDs = new();
        // Wingidon's Expansion Pack: Arithmetician, Chiromancer, Clairvoyant, Detective, Introvert, Jewelsmith, Lamb, Paperboy, Prince, Ranger, Sentinel, Sleuth, Spy, Bartender, Cartographer, Cartomancer, Citizen, Hound Tamer, Puzzlemaster, Visionary
        validIDs.Add("Arithmetician_WING");
        validIDs.Add("Chiromancer_WING");
        validIDs.Add("Clairvoyant_WING");
        validIDs.Add("Detective_WING");
        validIDs.Add("Introvert_WING");
        validIDs.Add("Lamb_WING");
        validIDs.Add("Paperboy_WING");
        validIDs.Add("Prince_WING");
        validIDs.Add("Ranger_WING");
        validIDs.Add("Sentinel_WING");
        validIDs.Add("Sleuth_WING");
        validIDs.Add("Spy_WING");
        validIDs.Add("Bartender_WING");
        validIDs.Add("Cartographer_WING");
        validIDs.Add("Cartomancer_WING");
        validIDs.Add("Underling_V_WING");
        validIDs.Add("Hound Tamer_WING ");
        validIDs.Add("Puzzlemaster_WING");
        validIDs.Add("Visionary_WING");

        // The Salem Trials: Detective, Psychic, Sheriff, Sibyl
        validIDs.Add("Detective_TST");
        validIDs.Add("Psychic_TST");
        validIDs.Add("Sheriff_TST");
        validIDs.Add("Sibyl_TST");

        // Riddles: Cowboy, Crewmate, Developer, Engineer, Governor, Officer, Pioneer, Surveyor, Therapist, Tracker, Director, Mathematician, Obsessor, Psychic, Riddler, Scanner, Weaver
        validIDs.Add("Cowboy_scm");
        validIDs.Add("Crewmate_scm");
        validIDs.Add("Developer_scm");
        validIDs.Add("Engineer_scm");
        validIDs.Add("Governor_scm");
        validIDs.Add("Officer_scm");
        validIDs.Add("Pioneer_scm");
        validIDs.Add("Surveyor_scm");
        validIDs.Add("Therapist_scm");
        validIDs.Add("Tracker_scm");
        validIDs.Add("Director_scm");
        validIDs.Add("Mathematician_scm");
        validIDs.Add("Obsessor_scm");
        validIDs.Add("Psychic_scm");
        validIDs.Add("Riddler_scm");
        validIDs.Add("Scanner_scm");
        validIDs.Add("Weaver_scm");

        // Dupery Bluff: Artist, Blood Hound, Journalist, Mailman, Mathematician, Partner, Priest, Private Eye, Reporter, Romantic, Skeptic, Therapist, Weatherman
        validIDs.Add("WING_Dupery_Artist");
        validIDs.Add("WING_Dupery_Blood Hound");
        validIDs.Add("WING_Dupery_Journalist");
        validIDs.Add("WING_Dupery_Mailman");
        validIDs.Add("WING_Dupery_Mathematician");
        validIDs.Add("WING_Dupery_Partner");
        validIDs.Add("WING_Dupery_Priest");
        validIDs.Add("WING_Dupery_Private Eye");
        validIDs.Add("WING_Dupery_Reporter");
        validIDs.Add("WING_Dupery_Romantic");
        validIDs.Add("WING_Dupery_Skeptic");
        validIDs.Add("WING_Dupery_Therapist");
        validIDs.Add("WING_Dupery_Weatherman");

        // Power Play: Huntress, Lookout, Lovestruck, Marksman, Newsman, Fisherman
        validIDs.Add("Huntress_POW");
        validIDs.Add("Lookout_POW");
        validIDs.Add("Lovestruck_POW");
        validIDs.Add("Marksman_POW");
        validIDs.Add("Newsman_POW");
        validIDs.Add("Fisherman_POW");

        // Circus: Mason, Surveyor, Watchman
        validIDs.Add("Mason_LRZH");
        validIDs.Add("Surveyor_scm");
        validIDs.Add("Watchman_LRZH");


        foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
        {
            if (validIDs.Contains(character.characterId)) returnList.Add(character.role);
        }
        return returnList;
    }
}