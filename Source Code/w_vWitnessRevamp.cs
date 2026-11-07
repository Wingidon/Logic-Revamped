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
public class w_vWitnessRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> affected = new();
        foreach (Character character in Gameplay.CurrentCharacters) if (character.statuses.Contains(ECharacterStatus.MessedUpByEvil)) affected.Add(character);
        if (affected.Count == 0) return new ActedInfo(ConjureInfo(charRef, true));
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(affected[UnityEngine.Random.RandomRangeInt(0, affected.Count)]);
        string info = ConjureInfo(selection[0], false);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> affected = new();
        Il2CppSystem.Collections.Generic.List<Character> unaffected = new();
        foreach (Character character in Gameplay.CurrentCharacters) if (character.statuses.Contains(ECharacterStatus.MessedUpByEvil)) affected.Add(character);
            else unaffected.Add(character);
        if (unaffected.Count == 0) return new ActedInfo(ConjureInfo(charRef, true));

        // for (int i = 0; i < 100; i++) MelonLogger.Msg(new wx_SavedScripts().PercentChance(15)); // Debug
        if (affected.Count != 0 && new wx_SavedScripts().PercentChance(15)) return new ActedInfo(ConjureInfo(charRef, true)); // 15% chance to say NO character was affected by an Evil if such a statement would be false.

        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(unaffected[UnityEngine.Random.RandomRangeInt(0, unaffected.Count)]);
        string info = ConjureInfo(selection[0], false);
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Witness";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }
    private string ConjureInfo(Character target, bool none)
    {
        if (none) return "NO character was affected by an Evil ability";
        return $"#{target.id} was affected by an Evil ability";
    }
    public w_vWitnessRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vWitnessRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vWitnessRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}