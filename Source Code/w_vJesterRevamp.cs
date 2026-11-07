using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static MelonLoader.MelonLaunchOptions;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vJesterRevamp : w_zRoleBase
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
        selection.Add(CharacterPicker.PickedCharacters[2]);
        int evils = 0;

        foreach (Character character in selection)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evils++;
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, evils), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;
        wx_SavedScripts sharedScripts = new();

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);
        selection.Add(CharacterPicker.PickedCharacters[2]);
        int evils = 0;
        int trueEvils = 0;
        Il2CppSystem.Collections.Generic.List<Character> fakeEvils = sharedScripts.GetFakeEvilTeam();

        foreach (Character character in selection)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) trueEvils++;
            if (fakeEvils.Contains(character)) evils++;
        }

        evils = sharedScripts.MakeNumberWrong(trueEvils, evils, 0);

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, evils), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    public override string Description
    {
        get
        {
            return "Learns 3 and how many are Evil";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(3, charRef);
        CharacterPicker.OnCharactersPicked += action1;
        CharacterPicker.OnStopPick += action2;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        chRef = charRef;
        CharacterPicker.Instance.StartPickCharacters(3, charRef);
        CharacterPicker.OnCharactersPicked += action3;
        CharacterPicker.OnStopPick += action2;
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, int num)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        characters = sharedScripts.SortList(characters);
        string info = $"#{characters[0].id}, #{characters[1].id}, #{characters[2].id}:\n";
        if (num == 0) info += "NONE are Evil";
        else if (num == 1) info += "1 is Evil";
        else info += $"{num} are Evil";
        return info;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_vJesterRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vJesterRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_vJesterRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}