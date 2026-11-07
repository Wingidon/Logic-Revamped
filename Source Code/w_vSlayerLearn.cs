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
public class w_vSlayerLearn : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        bool haveKilled = false;
        foreach (Character character in Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters))
        {
            possibleTargets.Add(character);
            if (character.GetRegisterAlignment() == EAlignment.Evil && character.alignment == EAlignment.Good) // Avoid picking the Wretch
            {
                possibleTargets.Remove(character);
            }
            if (character.dataRef.characterId == "Wretch_80988916") // Backup hard-code to avoid picking Wretch
            {
                possibleTargets.Remove(character);
            }
        }
        possibleTargets.Remove(charRef);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);


        if (selection[0].GetRegisterAlignment() == EAlignment.Evil)
        {
            haveKilled = selection[0].role.CheckIfCanBeKilled(selection[0]);
            if (haveKilled)
            {
                if (Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(selection[0]))
                {
                    selection[0].onReveal.Invoke();
                    selection[0].ChangeState(ECharacterState.Alive);
                    selection[0].Act(ETriggerPhase.Day);
                    selection[0].Reveal();
                }
                selection[0].RevealAllReal();
                selection[0].KillByDemon(charRef);
            }
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, haveKilled), selection);
        return returnInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        bool haveKilled = false;
        foreach (Character character in Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters))
        {
            possibleTargets.Add(character);
            if (character.GetRegisterAlignment() == EAlignment.Evil && character.alignment == EAlignment.Good) // Avoid picking the Wretch
            {
                possibleTargets.Remove(character);
            }
            if (character.dataRef.characterId == "Wretch_80988916") // Backup hard-code to avoid picking Wretch
            {
                possibleTargets.Remove(character);
            }
        }
        possibleTargets.Remove(charRef);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, false), selection);
        return returnInfo;
    }
    public override string Description
    {
        get
        {
            return "Learns 2 characters & a role that is and isn't among them.";
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

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, bool successful)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        string totalInfo = $"I couldn't kill #{characters[0].id}";
        if (successful) totalInfo = $"I killed Evil at #{characters[0].id}";
        return totalInfo;
    }
    public w_vSlayerLearn() : base(ClassInjector.DerivedConstructorPointer<w_vSlayerLearn>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vSlayerLearn(System.IntPtr ptr) : base(ptr)
    {
    }
}