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
public class w_oBombardierRevamp : w_zRoleBase
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
            return "Explodes when executed";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.OnExecuted && charRef.alignment == EAlignment.Good)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(999999);
        }
        if (trigger != ETriggerPhase.Day) return;
        bool extraFlavour = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("RoleFlavour").Value;
        if (extraFlavour) OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        bool extraFlavour = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("RoleFlavour").Value;
        if (extraFlavour) OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }

    public string ConjureInfo()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        remarks.Add("Try me!");
        remarks.Add("You want some?");
        remarks.Add("Don't even think about it!");
        remarks.Add("Try it.\nI <i>dare</i> you");
        remarks.Add("Do I look like a sane man?");
        remarks.Add("You even point that dagger at me and I'll blow this place to smithereens!");
        remarks.Add("Bring it!");
        remarks.Add("I don't fear the likes of you!");
        remarks.Add("Stay back!");
        remarks.Add("I'm warning you!");
        remarks.Add("I will explode!");
        remarks.Add("With steel cross my heart, and you will not live to see any of the consequences!");
        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }

    public override int GetDamageToYou()
    {
        return 999999;
    }

    public w_oBombardierRevamp() : base(ClassInjector.DerivedConstructorPointer<w_oBombardierRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oBombardierRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}