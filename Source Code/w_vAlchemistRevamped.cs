using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.Runtime.VersionSpecific.EventInfo;
using Il2CppSystem;
using Il2CppTMPro;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vAlchemistRevamped : w_zRoleBase
{
    ECharacterStatus cured = (ECharacterStatus)3211854;
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = sharedScripts.GetCharactersWithinRange(charRef, 2);
        int corruptions = 0;
        foreach (Character character in selection)
        {
            if (character.statuses.Contains(ECharacterStatus.Corrupted) || character.statuses.Contains(cured)) corruptions++;
        }
        return new ActedInfo(ConjureInfo(charRef, corruptions), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = sharedScripts.GetCharactersWithinRange(charRef, 2);
        int corruptions = 0;
        int fakeCorruptions = 0;
        // Count up possible fake Corruptions
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.statuses.Contains(ECharacterStatus.Corrupted) || character.statuses.Contains(cured)) corruptedChars.Add(character);
        }
        Il2CppSystem.Collections.Generic.List<Character> fakeCorruptedChars = sharedScripts.GetFakeGroup(charRef, corruptedChars);
        foreach (Character character in selection)
        {
            if (character.statuses.Contains(ECharacterStatus.Corrupted) || character.statuses.Contains(cured)) corruptions++;
            if (fakeCorruptedChars.Contains(character)) fakeCorruptions++;
        }

        fakeCorruptions = sharedScripts.MakeNumberWrong(corruptions, fakeCorruptions, 0);
        return new ActedInfo(ConjureInfo(charRef, fakeCorruptions), selection);
    }
    public override string Description
    {
        get
        {
            return "Alchemist. Noteworthy.";
        }
    }
    public override void OnSpawn(Character charRef)
    {
        charRef.statuses.AddResistance(ECharacterStatus.Corrupted, charRef);
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new();
            Il2CppSystem.Collections.Generic.List<Character> cureTargets = sharedScripts.GetCharactersWithinRange(charRef, 2);
            cureTargets = Characters.Instance.FilterCharacterType(cureTargets, ECharacterType.Villager);
            if (cureTargets.Count != 0)
            {
                foreach (Character character in cureTargets)
                {
                    if (character.statuses.Contains(ECharacterStatus.Corrupted))
                    {
                        if (character.role.CheckIfCanRemoveStatus(ECharacterStatus.Corrupted))
                        {
                            while (character.statuses.Contains(ECharacterStatus.Corrupted)) character.statuses.statuses.Remove(ECharacterStatus.Corrupted);
                            character.statuses.AddStatus(cured, charRef);
                        }
                    }
                }
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }
    private string ConjureInfo(Character charRef, int corruptions)
    {
        if (corruptions == 0) return "NO one was Corrupted around me";
        string info = "There ";
        if (corruptions == 1) info += "was ";
        else info += "were ";
        info += corruptions;
        info += " Corruption";
        if (corruptions != 1) info += "s";
        info += " around me";
        return info;
    }
    public w_vAlchemistRevamped() : base(ClassInjector.DerivedConstructorPointer<w_vAlchemistRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vAlchemistRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}