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
public class w_vGemcrafterRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        goodChars.Remove(charRef);
        if (goodChars.Count == 0)
        {
            return new ActedInfo(ConjureInfo(charRef, charRef));
        }
        Character target = goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)];
        selection.Add(target);
        return new ActedInfo(ConjureInfo(charRef, target), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        goodChars.Remove(charRef);
        if (goodChars.Count == 0)
        {
            return new ActedInfo(ConjureInfo(charRef, charRef));
        }
        Character target = goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)];
        selection.Add(target);
        return new ActedInfo(ConjureInfo(charRef, target), selection);
    }
    public override string Description
    {
        get
        {
            return "Gemcrafter";
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
    private string ConjureInfo(Character charRef, Character target)
    {
        if (charRef == target) return $"There are NO other Good characters";
        return $"#{target.id} is Good";
    }
    public w_vGemcrafterRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vGemcrafterRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vGemcrafterRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}