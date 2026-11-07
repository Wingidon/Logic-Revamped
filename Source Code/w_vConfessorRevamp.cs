using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vConfessorRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(charRef);
        bool dizzy = false;
        if (charRef.statuses.Contains(ECharacterStatus.Corrupted)) dizzy = true;
        if (charRef.GetRegisterAlignment() == EAlignment.Evil) dizzy = true;

        // Mod compatibility: Riddles
        if (charRef.dataRef.characterId == "Hypnotist_scm") dizzy = false;

        return new ActedInfo(ConjureInfo(dizzy), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "If Evil or Corrupted, \"I am dizzy\"";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart) charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
        if (trigger != ETriggerPhase.Day) return;
        OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart) charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
        if (trigger != ETriggerPhase.Day) return;
        OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }

    public string ConjureInfo(bool dizzy)
    {
        string info = "I am Good";
        if (dizzy)
        {
            info = "I am dizzy";
        }
        bool confessorReminders = CheckConfigOption_Bool("ConfessorReminders");
        if (confessorReminders) info += "\n\n<i>I am always Truthful, even if Evil or Corrupted</i>";
        return info;
    }
    public w_vConfessorRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vConfessorRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vConfessorRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}