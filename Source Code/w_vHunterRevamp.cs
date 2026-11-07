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
public class w_vHunterRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        wx_SavedScripts sharedScripts = new();
        evilChars.Remove(charRef);
        int distance = sharedScripts.GetClosestDistance(evilChars, charRef);
        if (distance == 10000) return new ActedInfo(ConjureInfo(charRef, 0));
        selection = Characters.Instance.GetCharactersAtRange(distance, charRef);
        return new ActedInfo(ConjureInfo(charRef, distance), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        Il2CppSystem.Collections.Generic.List<Character> fakeEvilChars = sharedScripts.GetFakeGroup(charRef, evilChars);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        evilChars.Remove(charRef);
        int distance = sharedScripts.GetClosestDistance(evilChars, charRef);
        int fakeDistance = sharedScripts.GetClosestDistance(fakeEvilChars, charRef);
        fakeDistance = sharedScripts.MakeNumberWrong(distance, fakeDistance, 1);
        selection = Characters.Instance.GetCharactersAtRange(fakeDistance, charRef);
        return new ActedInfo(ConjureInfo(charRef, fakeDistance), selection);
    }
    public override string Description
    {
        get
        {
            return "Hunter";
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
        if (distance == 0 || distance > Gameplay.CurrentCharacters.Count) return "There are NO Evil characters";
        string info = "Closest Evil is\n";
        if (distance == 1) info += "1 card away from me";
        else info += $"{distance} cards away from me";
        return info;
    }
    public w_vHunterRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vHunterRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vHunterRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}