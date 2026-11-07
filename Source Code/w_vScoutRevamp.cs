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
public class w_vScoutRevamp : w_zRoleBase
{
    Character target = new();
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        wx_SavedScripts sharedScripts = new();
        if (evilChars.Count == 1) return new ActedInfo("There is only 1 Evil");
        if (evilChars.Count == 0) return new ActedInfo("There are NO Evil characters");
        string chosenCharName = evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)].GetRegisterAs().characterName;

        int lowestDist = 1000;
        int checkDist = 1000;
        foreach (Character character in evilChars)
        {
            if (character.GetRegisterAs().characterName == chosenCharName)
            {
                checkDist = sharedScripts.GetClosestDistance(evilChars, character);
                if (checkDist < lowestDist)
                {
                    lowestDist = checkDist;
                    target = character;
                }
            }
        }

        if (lowestDist == 1000) return new ActedInfo("Something does not make sense");
        string info = ConjureInfo(chosenCharName, lowestDist);
        return new ActedInfo(info);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
        Il2CppSystem.Collections.Generic.List<string> evilNames = new();
        foreach (Character character in Gameplay.CurrentCharacters) if (character.GetRegisterAlignment() == EAlignment.Evil) evilNames.Add(character.GetRegisterAs().characterName);
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters()) if (character.startingAlignment == EAlignment.Evil) evilNames.Add(character.characterName);
        wx_SavedScripts sharedScripts = new();
        if (evilNames.Count == 0 || CheckConfigOption_Bool("BlindModeScout"))
        {
            foreach (CharacterData character in Characters.Instance.FilterCharacterType(Gameplay.Instance.GetAllAscensionCharacters(), ECharacterType.Minion))
            {
                if (character.startingAlignment == EAlignment.Evil) evilNames.Add(character.characterName);
            }
        }
        string chosenCharName = evilNames[UnityEngine.Random.RandomRangeInt(0, evilNames.Count)];

        int trueLowestDist = 100;
        int trueCheckDist = 100;
        foreach (Character character in evilChars)
        {
            if (character.GetRegisterAs().characterName == chosenCharName)
            {
                trueCheckDist = sharedScripts.GetClosestDistance(evilChars, character);
                if (trueCheckDist < trueLowestDist) trueLowestDist = trueCheckDist;
            }
        }

        Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
        Character anchor = fakeEvilTeam[UnityEngine.Random.RandomRangeInt(0, fakeEvilTeam.Count)];
        int falseDist = sharedScripts.GetClosestDistance(fakeEvilTeam, anchor);
        decimal characterCount = Gameplay.CurrentCharacters.Count / 2;
        int maxDist = (int)System.Math.Round(characterCount);
        falseDist = sharedScripts.MakeNumberWrongByRange(trueLowestDist, falseDist, 1, maxDist, 2, 2);

        if (falseDist > Gameplay.CurrentCharacters.Count) falseDist = UnityEngine.Random.RandomRangeInt(1, 4);

        string info = ConjureInfo(chosenCharName, falseDist);
        return new ActedInfo(info);
    }
    public override string Description
    {
        get
        {
            return "Scout";
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
    private string ConjureInfo(string name, int distance)
    {
        if (distance == 0 || distance > Gameplay.CurrentCharacters.Count) return "Something does not make sense";
        string cards = "cards";
        if (distance == 1) cards = "card";
        return $"{name} is {distance} {cards} away from their closest Evil";
    }
    public w_vScoutRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vScoutRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vScoutRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}