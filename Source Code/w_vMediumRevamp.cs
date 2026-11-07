using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vMediumRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        ActedInfo modCompat = ModCompatibility(charRef);
        if (modCompat.desc != "false") return modCompat;
        Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        goodChars.Remove(charRef);
        if (goodChars.Count == 0) return new ActedInfo("Something does not make sense");
        selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
        string info = ConjureInfo(selection[0], selection[0].GetRegisterAs());
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> bluffingChars = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        wx_SavedScripts sharedScripts = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().characterName != sharedScripts.GetFaceUpClaim(character).characterName)
                if (sharedScripts.GetFaceUpClaim(character).startingAlignment == EAlignment.Good) bluffingChars.Add(character);
        }
        bluffingChars.Remove(charRef);
        if (bluffingChars.Count == 0) return new ActedInfo("Something does not make sense");
        selection.Add(bluffingChars[UnityEngine.Random.RandomRangeInt(0, bluffingChars.Count)]);
        string info = ConjureInfo(selection[0], selection[0].GetCharacterBluffIfAble());
        return new ActedInfo(info, selection);
    }

    public ActedInfo ModCompatibility(Character charRef)
    {
        if (charRef.dataRef.characterId != "Hypnotist_scm") return new ActedInfo("false");
        new wx_SavedScripts().DebugMessage("We got a Hypnotist Medium!");
        Il2CppSystem.Collections.Generic.List<CharacterData> bluffOutcasts = new();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good))
        {
            if (character.startingAlignment == EAlignment.Good && character.usuallyDisguised) bluffOutcasts.Add(character);
        }
        if (bluffOutcasts.Count == 0) return new ActedInfo("false");
        CharacterData chosenOutcast = bluffOutcasts[UnityEngine.Random.RandomRangeInt(0, bluffOutcasts.Count)];
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef && character.dataRef.characterId != chosenOutcast.characterId) possibleTargets.Add(character);
        }
        if (possibleTargets.Count == 0) return new ActedInfo("false");
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        return new ActedInfo(ConjureInfo(selection[0], chosenOutcast), selection);

    }
    public override string Description
    {
        get
        {
            return "Medium";
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
    private string ConjureInfo(Character target, CharacterData targetRole)
    {
        return $"#{target.id} is the {targetRole.characterName}";
    }
    public w_vMediumRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vMediumRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vMediumRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}