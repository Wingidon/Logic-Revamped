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
public class w_oPlagueDoctorRevamp : w_zRoleBase
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("", null);
    }
    public override string Description
    {
        get
        {
            return "Corrupts a character. Picks 1, and if they're Corrupted, Learns an Evil.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new();
            Il2CppSystem.Collections.Generic.List<Character> targets = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
            targets = Characters.Instance.FilterAlignmentCharacters(targets, EAlignment.Good);
            targets = Characters.Instance.FilterCharacterMissingStatus(targets, ECharacterStatus.Corrupted);
            targets = Characters.Instance.FilterCharactersWithoutResistance(targets, ECharacterStatus.Corrupted);
            if (targets.Count == 0) return;
            Character target = targets[UnityEngine.Random.RandomRangeInt(0, targets.Count)];
            target.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        }
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(1, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        if (chars[0] == chRef) return;

        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(chars[0]);

        if (chars[0].statuses.Contains(ECharacterStatus.Corrupted))
        {
            Il2CppSystem.Collections.Generic.List<Character> evilChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil);
            if (evilChars.Count == 0) OnActed(ETriggerPhase.Day, chRef, new ActedInfo($"#{chars[0].id} is Corrupted,\nthere are NO Evil characters", chars));
            else
            {
                Character chosenEvil = evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)];
                selection.Add(chosenEvil);
                OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], chosenEvil, true), selection));
            }
        }
        else
        {
            OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], chars[0], false), chars));
        }
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        if (chars[0] == chRef) return;

        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(chars[0]);

        if (!chars[0].statuses.Contains(ECharacterStatus.Corrupted))
        {
            Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            goodChars.Remove(chars[0]);
            goodChars.Remove(chRef);
            if (goodChars.Count == 0) OnActed(ETriggerPhase.Day, chRef, new ActedInfo($"#{chars[0].id} is Corrupted,\nthere are NO Evil characters", chars));
            else
            {
                Character chosenGood = goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)];
                selection.Add(chosenGood);
                OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], chosenGood, true), selection));
            }
        }
        else
        {
            OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], chars[0], false), chars));
        }
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }

    private string ConjureInfo(Character corrupted, Character evil, bool targetCorrupted)
    {
        if (!targetCorrupted) return $"#{corrupted.id} is not Corrupted";
        return $"#{corrupted.id} is Corrupted,\n#{evil.id} is Evil";
    }
    public w_oPlagueDoctorRevamp() : base(ClassInjector.DerivedConstructorPointer<w_oPlagueDoctorRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_oPlagueDoctorRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}