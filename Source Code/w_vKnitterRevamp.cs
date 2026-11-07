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
public class w_vKnitterRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        int pairs = new wx_SavedScripts().GetPairsOfCharactersInList(evilChars);

        string info = ConjureInfo(pairs);
        return new ActedInfo(info);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        int pairs = sharedScripts.GetPairsOfCharactersInList(evilChars);
        Il2CppSystem.Collections.Generic.List<Character> fakeEvilChars = sharedScripts.GetFakeEvilTeam();
        int fakePairs = sharedScripts.GetPairsOfCharactersInList(fakeEvilChars);
        fakePairs = sharedScripts.MakeNumberWrong(pairs, fakePairs, 0);

        string info = ConjureInfo(fakePairs);
        return new ActedInfo(info);
    }
    public override string Description
    {
        get
        {
            return "Knitter";
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
    private string ConjureInfo(int pairs)
    {
        string pairsPlural = "pairs";
        string conjureNumber = pairs.ToString();
        string isAre = "are";
        if (pairs == 0) conjureNumber = "NO";
        if (pairs == 1)
        {
            pairsPlural = "pair";
            isAre = "is only";
        }
        return $"There {isAre} {conjureNumber}\n{pairsPlural} of Evil";
    }
    public w_vKnitterRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vKnitterRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vKnitterRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}