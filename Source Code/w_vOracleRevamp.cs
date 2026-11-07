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
public class w_vOracleRevamp : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> minionChars = new();
        Il2CppSystem.Collections.Generic.List<Character> nonMinionChars = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().type == ECharacterType.Minion) minionChars.Add(character);
            else nonMinionChars.Add(character);
        }

        if (minionChars.Count == 0) return new ActedInfo("There are no Minions");
        if (nonMinionChars.Count == 0) return new ActedInfo("Everyone else is a Minion");
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(minionChars[UnityEngine.Random.RandomRangeInt(0, minionChars.Count)]);
        CharacterData relevantMinion = selection[0].GetRegisterAs();
        nonMinionChars.Clear();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().characterName != relevantMinion.characterName) nonMinionChars.Add(character);
        }
        selection.Add(nonMinionChars[UnityEngine.Random.RandomRangeInt(0, nonMinionChars.Count)]);

        string info = ConjureInfo(selection, relevantMinion);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleNamedMinions = new();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion)) possibleNamedMinions.Add(character);
        Il2CppSystem.Collections.Generic.List<Character> nonMinionChars = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().type == ECharacterType.Minion) possibleNamedMinions.Add(character.GetRegisterAs());
            else nonMinionChars.Add(character);
        }

        if (nonMinionChars.Count == 0 || nonMinionChars.Count == 1) return new ActedInfo("There are no Minions");
        if (possibleNamedMinions.Count == 0 || CheckConfigOption_Bool("BlindModeOracle")) possibleNamedMinions = Characters.Instance.FilterCharacterType(Gameplay.Instance.GetAllAscensionCharacters(), ECharacterType.Minion);

        CharacterData namedMinion = possibleNamedMinions[UnityEngine.Random.RandomRangeInt(0, possibleNamedMinions.Count)];
        Il2CppSystem.Collections.Generic.List<Character> selection = new();

        nonMinionChars.Clear();
        foreach (Character character in Gameplay.CurrentCharacters) if (character.GetRegisterAs().characterName != namedMinion.characterName) if (character.dataRef.characterId != "Wretch_80988916") nonMinionChars.Add(character);

        selection.Add(nonMinionChars[UnityEngine.Random.RandomRangeInt(0, nonMinionChars.Count)]);
        nonMinionChars.Remove(selection[0]);
        selection.Add(nonMinionChars[UnityEngine.Random.RandomRangeInt(0, nonMinionChars.Count)]);
        string info = ConjureInfo(selection, namedMinion);
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Oracle";
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
    private string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, CharacterData chosenMinionRole)
    {
        wx_SavedScripts shared = new();
        characters = shared.SortList(characters);
        string info = $"#{characters[0].id} or #{characters[1].id} is the {chosenMinionRole.characterName}";
        return info;
    }
    public w_vOracleRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vOracleRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vOracleRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}