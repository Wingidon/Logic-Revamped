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
public class w_vBakerRevamp : w_zRoleBase // Workie :)
{
    string myPreviousRole = "original";
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            wx_SavedScripts sharedScripts = new();
            Il2CppSystem.Collections.Generic.List<Character> selection = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
            selection.Remove(charRef);
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo(ConjureInfo(myPreviousRole, charRef), selection));
            if (charRef.statuses.Contains(ECharacterStatus.BrokenAbility)) return;
            CharacterData bakerData = ProjectContext.Instance.gameData.GetCharacterDataOfId("Baker_22847064");
            if (bakerData == null)
            {
                sharedScripts.DebugMessage("!!Baker data is null!!");
                return;
            }
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = Characters.Instance.FilterAlignmentCharacters(selection, EAlignment.Good);
            possibleTargets = Characters.Instance.FilterRealAlignmentCharacters(possibleTargets, EAlignment.Good);
            possibleTargets = Characters.Instance.FilterCharacterType(possibleTargets, ECharacterType.Villager);
            possibleTargets = Characters.Instance.FilterRealCharacterType(possibleTargets, ECharacterType.Villager);

            if (possibleTargets.Count == 0) return;
            Character target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
            sharedScripts.DebugMessage($"Baker at #{charRef.id} turning the {target.dataRef.characterName} at #{target.id} into new Baker");
            CharacterData targetData = target.dataRef;
            target.InitWithNoReset(bakerData);
            target.bluff = targetData;
            target.Act((ETriggerPhase)211151825);
        }
        if (trigger == (ETriggerPhase)211151825)
        {
            wx_SavedScripts sharedScripts = new();
            if (!charRef.bluff)
            {
                sharedScripts.DebugMessage("!!New Baker has no bluff!!");
                return;
            }
            myPreviousRole = charRef.bluff.characterName;
            charRef.bluff = null;
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            wx_SavedScripts sharedScripts = new();
            Il2CppSystem.Collections.Generic.List<Character> selection = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
            selection.Remove(charRef);
            Il2CppSystem.Collections.Generic.List<string> possiblePrevRoles = new();
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good))
            {
                if (character.type == ECharacterType.Villager)
                    if (character.characterName != myPreviousRole)
                        possiblePrevRoles.Add(character.characterName);
            }
            if (possiblePrevRoles.Count == 0) possiblePrevRoles.Add("Villager in a bugged deck, report this to Wingidon and include a screenshot of the Deck please.");
            string chosenPrevRole = possiblePrevRoles[UnityEngine.Random.RandomRangeInt(0, possiblePrevRoles.Count)];
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo(ConjureInfo(chosenPrevRole, charRef), selection));
        }
        if (trigger == (ETriggerPhase)211151825)
        {
            if (charRef.dataRef.characterId != "Baker_22847064") return; // Non-Bakers shouldn't be running Baker logic.
            wx_SavedScripts sharedScripts = new();
            if (!charRef.bluff)
            {
                sharedScripts.DebugMessage("!!New Baker has no bluff!!");
                return;
            }
            myPreviousRole = charRef.bluff.characterName;
            charRef.bluff = null;
        }
    }
    public string ConjureInfo(string prevCharacter, Character charRef)
    {
        if (charRef.dataRef.characterId == "Puppet_15989619") return "I was the Baker";
        if (prevCharacter == "original")
        {
            return $"I am the start of my chain";
        }
        return $"I was the {prevCharacter}";
    }

    public w_vBakerRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vBakerRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vBakerRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}