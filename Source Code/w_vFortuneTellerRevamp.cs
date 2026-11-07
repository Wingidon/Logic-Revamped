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
public class w_vFortuneTellerRevamp : w_zRoleBase
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

        bool isEvil = false;

        if (selection[0].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;
        if (selection[1].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, isEvil), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);

        bool isEvil = false;

        if (selection[0].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;
        if (selection[1].GetRegisterAlignment() == EAlignment.Evil) isEvil = true;

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, !isEvil), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
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
            info = $"#{characters[0].id} and #{characters[1].id} are both Good";
        }
        return info;
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }
    public w_vFortuneTellerRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vFortuneTellerRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_vFortuneTellerRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}