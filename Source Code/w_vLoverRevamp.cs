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
public class w_vLoverRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> neighbours = new wx_SavedScripts().GetCharacterNeighbours(charRef);
        int evils = 0;
        foreach (Character character in neighbours) if (character.GetRegisterAlignment() == EAlignment.Evil) evils++;

        string info = ConjureInfo(evils);
        return new ActedInfo(info, neighbours);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> neighbours = sharedScripts.GetCharacterNeighbours(charRef);
        Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
        int evils = 0;
        int fakeEvils = 0;
        foreach (Character character in neighbours)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evils++;
            if (fakeEvilTeam.Contains(character)) fakeEvils++;
        }
        fakeEvils = sharedScripts.MakeNumberWrong(evils, fakeEvils, 0);
        string info = ConjureInfo(fakeEvils);
        return new ActedInfo(info, neighbours);
    }
    public override string Description
    {
        get
        {
            return "Lover";
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
    private string ConjureInfo(int evils)
    {
        string characterPlural = "Evils";
        string conjureNumber = evils.ToString();
        if (evils == 0) conjureNumber = "NO";
        if (evils == 1) characterPlural = "Evil";
        return $"{conjureNumber} {characterPlural} adjacent to me";
    }
    public w_vLoverRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vLoverRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vLoverRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}