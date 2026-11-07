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
public class w_vJudgeLearn : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            possibleTargets.Add(character);
        }
        possibleTargets.Remove(charRef);

        bool isLying = false;
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        isLying = CharacterHelper.CheckLyingAppearance(selection[0]);

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, isLying), selection);

        return returnInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            possibleTargets.Add(character);
        }
        possibleTargets.Remove(charRef);

        bool isLying = false;
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        isLying = CharacterHelper.CheckLyingAppearance(selection[0]);

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, !isLying), selection);

        return returnInfo;
    }
    public override string Description
    {
        get
        {
            return "Learns a character & whether or not they're Lying";
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

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, bool isLying)
    {
        if (isLying)
        {
            return $"#{characters[0].id} is Lying";
        }
        return $"#{characters[0].id} is Truthful";
    }
    public w_vJudgeLearn() : base(ClassInjector.DerivedConstructorPointer<w_vJudgeLearn>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vJudgeLearn(System.IntPtr ptr) : base(ptr)
    {
    }
}