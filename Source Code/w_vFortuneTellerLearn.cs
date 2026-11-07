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
public class w_vFortuneTellerLearn : w_zRoleBase
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

        bool isEvil = false;
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[0]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        if (selection[0].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;
        if (selection[1].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, isEvil), selection);

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

        bool isEvil = false;
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[0]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        if (selection[0].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;
        if (selection[1].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, !isEvil), selection);

        return returnInfo;
    }
    public override string Description
    {
        get
        {
            return "Learns 2 characters & whether or not either is Evil";
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

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, bool isEvil)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        characters = sharedScripts.SortList(characters);
        string info = $"";
        if (isEvil)
        {
            info = $"At least one is Evil: #{characters[0].id}, #{characters[1].id}";
        }
        else
        {
            info = $"Both are Good: #{characters[0].id}, #{characters[1].id}";
        }
        return info;
    }

    public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
    {
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        for (int i = 0; i < 20; i++)
        {
            foreach (Character character in list)
            {
                if (character.id == i) newList.Add(character);
            }
        }
        return newList;
    }
    public w_vFortuneTellerLearn() : base(ClassInjector.DerivedConstructorPointer<w_vFortuneTellerLearn>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vFortuneTellerLearn(System.IntPtr ptr) : base(ptr)
    {
    }
}