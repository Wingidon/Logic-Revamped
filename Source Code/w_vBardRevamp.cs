using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.Runtime.VersionSpecific.EventInfo;
using Il2CppSystem;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vBardRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = Characters.Instance.FilterCharacterContainsStatus(Gameplay.CurrentCharacters, ECharacterStatus.Corrupted);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        wx_SavedScripts sharedScripts = new();
        int distance = sharedScripts.GetClosestDistance(corruptedChars, charRef);
        if (distance == 10000) return new ActedInfo(ConjureInfo(charRef, 0));
        selection = Characters.Instance.GetCharactersAtRange(distance, charRef);
        return new ActedInfo(ConjureInfo(charRef, distance), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        bool noCorruptionsValid = true;
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = Characters.Instance.FilterCharacterContainsStatus(Gameplay.CurrentCharacters, ECharacterStatus.Corrupted);
        Il2CppSystem.Collections.Generic.List<Character> fakeCorruptedChars = sharedScripts.GetFakeGroup(charRef, corruptedChars);
        if (fakeCorruptedChars.Count == 0)
        {
            fakeCorruptedChars = sharedScripts.GetFakeEvilTeam();
        }
        if (corruptedChars.Count == 0) noCorruptionsValid = false;

        if (noCorruptionsValid && Calculator.RollDice(10) == 1) return new ActedInfo(ConjureInfo(charRef, 0));
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        corruptedChars.Remove(charRef);
        int distance = sharedScripts.GetClosestDistance(corruptedChars, charRef);
        int fakeDistance = sharedScripts.GetClosestDistance(fakeCorruptedChars, charRef);
        fakeDistance = sharedScripts.MakeNumberWrongByRange(distance, fakeDistance, 1, sharedScripts.RoundValToInt((Gameplay.CurrentCharacters.Count - fakeCorruptedChars.Count) / 2), 2, 1);
        selection = Characters.Instance.GetCharactersAtRange(fakeDistance, charRef);
        return new ActedInfo(ConjureInfo(charRef, fakeDistance), selection);
    }
    public override string Description
    {
        get
        {
            return "Bard";
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
    private string ConjureInfo(Character charRef, int distance)
    {
        if (distance == 0 || distance > Gameplay.CurrentCharacters.Count) return "There are NO Corrupted characters";
        string info = "I am ";
        if (distance == 1) info += "1 card";
        else info += $"{distance} cards";
        info += " away from closest Corruption";
        return info;
    }
    public w_vBardRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vBardRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vBardRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}