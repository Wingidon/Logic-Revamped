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
public class w_vEmpressRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        goodChars.Remove(charRef);
        evilChars.Remove(charRef);
        if (evilChars.Count == 0) return new ActedInfo("Something does not make sense");
        if (goodChars.Count == 0 || goodChars.Count == 1) return new ActedInfo("Something does not make sense");

        selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
        goodChars.Remove(selection[0]);
        selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
        selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);

        string info = ConjureInfo(selection);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        foreach (Character character in Gameplay.CurrentCharacters) possibleTargets.Add(character);
        possibleTargets.Remove(charRef);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[0]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[1]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        int mentionedEvils = 0;
        foreach (Character character in selection)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) mentionedEvils++;
        }

        if (mentionedEvils == 1)
        {
            selection.Clear();
            possibleTargets = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            possibleTargets.Remove(charRef);
            if (possibleTargets.Count < 3) possibleTargets = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
            possibleTargets.Remove(charRef);
            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
            possibleTargets.Remove(selection[0]);
            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
            possibleTargets.Remove(selection[1]);
            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        }

        string info = ConjureInfo(selection);
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Empress";
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
    private string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters)
    {
        return $"One is Evil:\n{new wx_SavedScripts().MentionEveryCharacterInList(characters, "")}";
    }
    public w_vEmpressRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vEmpressRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vEmpressRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}