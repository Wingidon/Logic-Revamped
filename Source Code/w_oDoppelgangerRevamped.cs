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
public class w_oDoppelgangerRevamped : w_zRoleBase
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
            return "Disguises as an in-play Good role";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart) charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        if (trigger != ETriggerPhase.Day) return;
        if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        if (!charRef.bluff) OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }

    public string ConjureInfo()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        remarks.Add("There are no Villagers I can Disguise as!");

        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> bluffs = new();
        bool outcastDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("DoppelOutcastDisguise").Value;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.alignment == EAlignment.Good && character.dataRef.bluffable)
            {
                if (character.dataRef.type == ECharacterType.Villager || (outcastDisguise && character.dataRef.type == ECharacterType.Outcast))
                {
                    bluffs.Add(character.dataRef);
                }
            }
        }
        charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        if (bluffs.Count == 0) return null;
        return bluffs[UnityEngine.Random.RandomRangeInt(0, bluffs.Count)];
    }

    public w_oDoppelgangerRevamped() : base(ClassInjector.DerivedConstructorPointer<w_oDoppelgangerRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oDoppelgangerRevamped(System.IntPtr ptr) : base(ptr)
    {
    }

    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.bluff && charRef.bluff.role != charRef.dataRef.role) return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        else return true;
    }
}