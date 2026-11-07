using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_oLycanthropeRevamped : w_zRoleBase
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
        if (trigger == ETriggerPhase.Night)
        {
            if (Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(charRef))
            {
                if (Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef))
                {
                    CharacterData werewolfData = ProjectContext.Instance.gameData.GetCharacterDataOfId("Werewolf_78350415");
                    CharacterData bluff = charRef.bluff;
                    charRef.Init(werewolfData);
                    charRef.statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
                    charRef.statuses.statuses.Remove(ECharacterStatus.WorkingAbility);
                    charRef.GiveBluff(bluff);
                }
            }
        }
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
        charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
        bool outcastDisguise = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanOutcastDisguise").Value;
        bool doubleClaim = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("LycanDoubleClaim").Value;
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = new();
        bool forceNotInPlay = true; // Avoid trying to claim an in-play role when there are none to claim.
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.dataRef.bluffable)
            {
                if (character.dataRef.type != ECharacterType.Outcast || outcastDisguise)
                {
                    forceNotInPlay = false;
                    break;
                }
            }
        }
        if (!doubleClaim || sharedScripts.PercentChance(75) || forceNotInPlay)
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, outcastDisguise);
        }
        else
        {
            bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, outcastDisguise);
        }
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }

    public w_oLycanthropeRevamped() : base(ClassInjector.DerivedConstructorPointer<w_oLycanthropeRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oLycanthropeRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.bluff && charRef.bluff.role != charRef.dataRef.role) return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        else return true;
    }
}