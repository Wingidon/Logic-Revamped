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
public class w_mPuppeteerRevamped : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Creates a Puppet.";
        }
    }

    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new();
            sharedScripts.DebugMessage($"Puppeteer at #{charRef.id} acting...");
            CharacterData puppetData = sharedScripts.GrabCharacterDataByID("Puppet_15989619");
            if (puppetData == null)
            {
                sharedScripts.DebugMessage("Couldn't find Puppet data! Aborting!");
                return;
            }

            bool extendedRange = CheckConfigOption_Bool("PuppeteerExtendedRange");
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new();
            if (extendedRange)
            {
                sharedScripts.DebugMessage("Puppeteer Extended Range enabled! Seeking targets...");
                possibleTargets = sharedScripts.GetClosestCharacters(charRef, ECharacterType.Villager, EAlignment.Good);
            }
            else
            {
                sharedScripts.DebugMessage("Puppeteer Extended Range disabled! Seeking targets...");
                possibleTargets = sharedScripts.GetCharacterNeighbours(charRef);
                possibleTargets = Characters.Instance.FilterAlignmentCharacters(possibleTargets, EAlignment.Good);
                possibleTargets = Characters.Instance.FilterRealAlignmentCharacters(possibleTargets, EAlignment.Good);
                possibleTargets = Characters.Instance.FilterCharacterType(possibleTargets, ECharacterType.Villager);
                possibleTargets = Characters.Instance.FilterRealCharacterType(possibleTargets, ECharacterType.Villager);
                sharedScripts.DebugMessage($"Possible targets: {sharedScripts.MentionEveryCharacterInList(possibleTargets, "")}");
            }

            if (possibleTargets.Count == 0)
            {
                sharedScripts.DebugMessage("Found no possible target! Aborting!");
                return;
            }

            Character target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
            CharacterData prevBluff = target.dataRef;
            target.Init(puppetData);
            target.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
            target.statuses.AddStatus(ECharacterStatus.BrokenAbility, charRef);
            target.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            target.statuses.AddStatus(ECharacterStatus.AlteredCharacter, charRef);
            target.GiveBluff(prevBluff);
            sharedScripts.DebugMessage($"Puppeteered #{target.id}!");
        }
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = new();
        if (sharedScripts.PercentChance(60))
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        else bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, true);
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_mPuppeteerRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mPuppeteerRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mPuppeteerRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}





