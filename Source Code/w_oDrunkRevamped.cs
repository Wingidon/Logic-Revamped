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
public class w_oDrunkRevamped : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo(ConjureInfo());
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "Lies and Disguises as an in-play Good role.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start || trigger == ETriggerPhase.AfterRoundStart) charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        if (trigger != ETriggerPhase.Day) return;
        if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start || trigger == ETriggerPhase.AfterRoundStart) charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        if (trigger != ETriggerPhase.Day) return;
        if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }

    public string ConjureInfo()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        remarks.Add("Something does not make sense");

        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        bool outcastDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("DrunkOutcastDisguise").Value;
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, outcastDisguise);
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public override bool CheckIfCanRemoveStatus(ECharacterStatus status)
    {
        if (status == ECharacterStatus.Corrupted) return false;
        return true;
    }
    public override int GetDamageToYou()
    {
        int drunkDamage = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<int>("DrunkDamage").Value;
        return drunkDamage;
    }
    public w_oDrunkRevamped() : base(ClassInjector.DerivedConstructorPointer<w_oDrunkRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oDrunkRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}