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
public class w_vInvestigatorRevamp : w_zRoleBase
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);

        ECharacterType suspiciousType = ECharacterType.Villager;

        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        types.Add(ECharacterType.Villager);
        types.Add(ECharacterType.Outcast);
        types.Add(ECharacterType.Minion);
        types.Add(ECharacterType.Demon);
        foreach (ECharacterType type in types)
        {
            foreach (Character character in selection)
            {
                if (character.GetRegisterAs().type == type)
                {
                    suspiciousType = type;
                }
            }
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, suspiciousType), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);

        ECharacterType trueSuspiciousType = ECharacterType.Villager;
        ECharacterType falseSuspiciousType = ECharacterType.Villager;

        Il2CppSystem.Collections.Generic.List<ECharacterType> types = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        types.Add(ECharacterType.Villager);
        types.Add(ECharacterType.Outcast);
        types.Add(ECharacterType.Minion);
        types.Add(ECharacterType.Demon);
        foreach (ECharacterType type in types)
        {
            foreach (Character character in selection)
            {
                if (character.GetRegisterAs().type == type)
                {
                    trueSuspiciousType = type;
                    falseSuspiciousType = type;
                }
            }
        }

        bool preferVillager = false;
        bool villagerBluff = false;
        bool outcastBluff = false;
        bool bluffingOutcast = false;
        bool evilPicked = false;
        bool faceUpOutcast = false;
        int chosenVillagers = 0;

        // If both are claiming Villager, but they aren't both *actually* Villagers,
        foreach (Character character in selection)
        {
            if (character.GetRegisterAs().type == ECharacterType.Villager)
            {
                chosenVillagers++;
            }
            else if (character.bluff)
            {
                if (character.bluff.type == ECharacterType.Villager)
                {
                    villagerBluff = true;
                }
                if (character.bluff.type == ECharacterType.Outcast)
                {
                    outcastBluff = true;
                }
            }
            else if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                faceUpOutcast = true;
            }
            if (character.GetRegisterAs().type != ECharacterType.Villager && character.GetRegisterAs().type != ECharacterType.Outcast)
            {
                evilPicked = true;
            }
        }
        if (trueSuspiciousType != ECharacterType.Villager && villagerBluff) // If someone's Disguised as a Villager, prefer to say they're both Villagers.
        {
            preferVillager = true;
            falseSuspiciousType = ECharacterType.Villager;
        }

        if (faceUpOutcast)
        {
            preferVillager = false;
            falseSuspiciousType = ECharacterType.Outcast;
        }

        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
        {
            if (character.usuallyDisguised)
            {
                bluffingOutcast = true; break;
            }
        }

        if (outcastBluff && trueSuspiciousType != ECharacterType.Outcast)
        {
            preferVillager = false;
            falseSuspiciousType = ECharacterType.Outcast;
        }

        if (!evilPicked)
        {
            if (sharedScripts.PercentChance(25))
            {
                falseSuspiciousType = ECharacterType.Demon;
            }
            else falseSuspiciousType = ECharacterType.Minion;
        }

        Il2CppSystem.Collections.Generic.List<ECharacterType> randomTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>(); // Used if none of the above conditions are met
        if (outcastBluff || bluffingOutcast)
        {
            if (trueSuspiciousType != ECharacterType.Outcast)
            {
                randomTypes.Add(ECharacterType.Outcast);
                randomTypes.Add(ECharacterType.Outcast);
                randomTypes.Add(ECharacterType.Outcast);
                randomTypes.Add(ECharacterType.Outcast);
            }
        }
        if (trueSuspiciousType != ECharacterType.Minion)
        {
            randomTypes.Add(ECharacterType.Minion);
            randomTypes.Add(ECharacterType.Minion);
            randomTypes.Add(ECharacterType.Minion);
        }
        if (trueSuspiciousType != ECharacterType.Demon)
        {
            randomTypes.Add(ECharacterType.Demon);
        }


        if (preferVillager) falseSuspiciousType = ECharacterType.Villager;

        if (falseSuspiciousType == trueSuspiciousType)
        {
            falseSuspiciousType = randomTypes[UnityEngine.Random.RandomRangeInt(0, randomTypes.Count)];
        }

        // My hopes are that this will result in somewhat-convincing Lies.

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, falseSuspiciousType), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    public override string Description
    {
        get
        {
            return "Learns 2 characters and what the most Suspicious type among them is.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(2, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, ECharacterType suspiciousType)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        characters = sharedScripts.SortList(characters);
        string info = $"#{characters[0].id} or #{characters[1].id} is ";
        if (suspiciousType == ECharacterType.Villager)
        {
            info = $"#{characters[0].id} and #{characters[1].id} are both Villagers";
        }
        else if (suspiciousType == ECharacterType.Outcast)
        {
            info += "an Outcast";
        }
        else
        {
            info += $"a {suspiciousType.ToString()}";
        }
        return info;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_vInvestigatorRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vInvestigatorRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_vInvestigatorRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}