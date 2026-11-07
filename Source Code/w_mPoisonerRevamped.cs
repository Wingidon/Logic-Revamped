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
public class w_mPoisonerRevamped : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Lies and Disguises. Poisons an adjacent Villager.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Start) return;
        bool extendedRange = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("PoisonerExtendedRange").Value;
        Il2CppSystem.Collections.Generic.List<Character> targets = new();
        wx_SavedScripts sharedScripts = new();
        if (extendedRange)
        {
            int dist = 100;
            int checkDist = 100;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAs().type == ECharacterType.Villager && character.GetRegisterAlignment() == EAlignment.Good && !character.statuses.Contains(ECharacterStatus.Corrupted))
                {
                    checkDist = sharedScripts.GetDistanceBetweenCharacters(charRef, character);
                    if (checkDist < dist)
                    {
                        targets.Clear();
                        targets.Add(character);
                        dist = checkDist;
                    }
                    else if (checkDist == dist)
                    {
                        targets.Add(character);
                    }
                }
            }
        }
        else
        {
            targets = sharedScripts.GetCharacterNeighbours(charRef);
            targets = Characters.Instance.FilterAlignmentCharacters(targets, EAlignment.Good);
            targets = Characters.Instance.FilterCharacterType(targets, ECharacterType.Villager);
            targets = Characters.Instance.FilterCharacterMissingStatus(targets, ECharacterStatus.Corrupted);
        }


        if (targets.Count != 0) targets = Characters.Instance.FilterCharactersWithoutResistance(targets, ECharacterStatus.Corrupted);

        if (targets.Count == 0) sharedScripts.DebugMessage($"Poisoner at #{charRef.id} found no valid poison targets");
        else
        {
            sharedScripts.DebugMessage($"Poisoner at #{charRef.id} found the following target(s): {sharedScripts.MentionEveryCharacterInList(targets, "")}");
            Character target = targets[UnityEngine.Random.RandomRangeInt(0, targets.Count)];
            sharedScripts.DebugMessage($"Chose #{target.id}");
            target.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            target.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
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
    public w_mPoisonerRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mPoisonerRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mPoisonerRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}