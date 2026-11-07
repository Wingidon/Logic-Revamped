using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vSlayerRevamp : w_zRoleBase
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
            return "Picks 1 and kills if they're Evil";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
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
        if (!Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(chars[0])) return;

        bool attacked = false;
        if (chars[0].GetRegisterAlignment() == EAlignment.Evil)
        {
            attacked = true;
            /*
            chars[0].Reveal();
            chars[0].RevealReal();
            chars[0].RevealAllReal();
            chars[0].KillByDemon(chRef);
            // if (chars[0].bluff) chars[0].disguiseIcon.SetActive(true);
            if (chars[0].alignment == EAlignment.Good)
            {
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(chars[0].role.GetDamageToYou());
            }
            */
            if (chars[0].role.CheckIfCanBeKilled(chars[0])) chars[0].ExecuteAndReveal();
        }

        OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], attacked), chars));
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        chars.Add(CharacterPicker.PickedCharacters[0]);
        if (chars[0] == chRef) return;
        if (!Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(chars[0])) return;

        OnActed(ETriggerPhase.Day, chRef, new ActedInfo(ConjureInfo(chars[0], false), chars));
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
    }

    private string ConjureInfo(Character target, bool couldKill)
    {
        if (couldKill) return $"I attacked Evil at #{target.id}";
        return $"I couldn't\nkill #{target.id}";
    }
    public w_vSlayerRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vSlayerRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_vSlayerRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}