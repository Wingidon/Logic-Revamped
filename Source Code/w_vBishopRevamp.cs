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
public class w_vBishopRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> villagerChars = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
        Il2CppSystem.Collections.Generic.List<Character> outcastChars = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Outcast);
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Minion);
        foreach (Character character in Gameplay.CurrentCharacters) if (character.GetRegisterAs().type == ECharacterType.Demon) evilChars.Add(character);

        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new();
        if (evilChars.Count != 0)
        {
            selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
            types.Add(selection[0].GetRegisterAs().type);
        }
        if (villagerChars.Count != 0)
        {
            selection.Add(villagerChars[UnityEngine.Random.RandomRangeInt(0, villagerChars.Count)]);
            types.Add(ECharacterType.Villager);
        }
        if (outcastChars.Count != 0)
        {
            selection.Add(outcastChars[UnityEngine.Random.RandomRangeInt(0, outcastChars.Count)]);
            types.Add(ECharacterType.Outcast);
        }

        string info = ConjureInfo(selection, types);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new();
        int vilCount = 0;
        int outCount = 0;
        int minCount = 0;
        int demCount = 0;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            possibleTargets.Add(character);
            if (character.GetRegisterAs().type == ECharacterType.Villager) vilCount++;
            if (character.GetRegisterAs().type == ECharacterType.Outcast) outCount++;
            if (character.GetRegisterAs().type == ECharacterType.Minion) minCount++;
            if (character.GetRegisterAs().type == ECharacterType.Demon) demCount++;
        }

        if (vilCount != 0) types.Add(ECharacterType.Villager);
        if (outCount != 0) types.Add(ECharacterType.Outcast);
        if (minCount != 0)
        {
            if (demCount == 0 || sharedScripts.PercentChance(66)) types.Add(ECharacterType.Minion);
            else types.Add(ECharacterType.Demon);
        }
        else types.Add(ECharacterType.Demon);

        if (types.Count == 0) return new ActedInfo("Something does not make sense");

        ECharacterType missingType = types[UnityEngine.Random.RandomRangeInt(0, types.Count)];
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().type == missingType) possibleTargets.Remove(character);
        }

        if (possibleTargets.Count < types.Count)
        {
            possibleTargets = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
        }

        foreach (ECharacterType type in types)
        {
            selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
            possibleTargets.Remove(selection[selection.Count - 1]);
        }

        string info = ConjureInfo(selection, types);
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Bishop";
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
    private string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, Il2CppSystem.Collections.Generic.List<ECharacterType> types)
    {
        if (characters.Count == 0) return "Something does not make sense";
        if (characters.Count == 1)
        {
            string an = "a";
            if (types[0] == ECharacterType.Outcast) an = "an";
            return $"#{characters[0].id} is {an} {types[0].ToString()}";
        }
        string returnInfo = "Among\n";
        wx_SavedScripts sharedScripts = new();
        returnInfo += sharedScripts.MentionEveryCharacterInList(characters, "");
        returnInfo += ",\nthere is:\n";
        returnInfo += sharedScripts.MentionEveryTypeInList(types, "");
        return returnInfo;
    }
    public w_vBishopRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vBishopRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vBishopRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}