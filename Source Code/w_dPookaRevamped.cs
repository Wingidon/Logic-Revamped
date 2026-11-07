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
public class w_dPookaRevamped : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Lies and Disguises. Corrupts nearby Villagers.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Start) return;
        wx_SavedScripts sharedScripts = new();
        sharedScripts.DebugMessage($"Pooka at #{charRef.id} acting...");
        bool pookaExtendedRange = CheckConfigOption_Bool("PookaExtendedRange");
        if (pookaExtendedRange)
        {
            sharedScripts.DebugMessage("Pooka extended range enabled! Seeking targets...");
            Il2CppSystem.Collections.Generic.List<Character> targets = sharedScripts.GetClosestCharacterOnEachSide(charRef, "Perfect Good, Perfect Villager, Pure");
            if (targets.Count == 0)
            {
                sharedScripts.DebugMessage("Found no targets! How did this happen?");
                return;
            }
            targets = Characters.Instance.FilterCharactersWithoutResistance(targets, ECharacterStatus.Corrupted);
            targets = Characters.Instance.FilterCharacterMissingStatus(targets, ECharacterStatus.Corrupted);
            if (targets.Count == 0) return;
            foreach (Character character in targets)
            {
                character.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            }
        }
        else
        {
            sharedScripts.DebugMessage("Pooka extended range disabled! Seeking targets...");
            Il2CppSystem.Collections.Generic.List<Character> targets = sharedScripts.GetCharacterNeighbours(charRef);
            targets = Characters.Instance.FilterCharacterType(targets, ECharacterType.Villager);
            targets = Characters.Instance.FilterRealCharacterType(targets, ECharacterType.Villager);
            targets = Characters.Instance.FilterAlignmentCharacters(targets, EAlignment.Good);
            targets = Characters.Instance.FilterRealAlignmentCharacters(targets, EAlignment.Good);
            targets = Characters.Instance.FilterCharacterMissingStatus(targets, ECharacterStatus.Corrupted);
            targets = Characters.Instance.FilterCharactersWithoutResistance(targets, ECharacterStatus.Corrupted);
            if (targets.Count == 0)
            {
                sharedScripts.DebugMessage("No valid targets!");
                return;
            }
            foreach (Character character in targets)
            {
                character.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            }
        }
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, CheckConfigOption_Bool("DemonOutcastDisguise"));
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_dPookaRevamped() : base(ClassInjector.DerivedConstructorPointer<w_dPookaRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_dPookaRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}