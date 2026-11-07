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
public class w_vConfessorAlternate : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(charRef);
        return new ActedInfo(ConjureInfo(false, charRef), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(charRef);
        return new ActedInfo(GetDizzyStatement(charRef), selection);
    }
    public override string Description
    {
        get
        {
            return "If Evil or Corrupted, \"I am dizzy\"";
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

    public string ConjureInfo(bool dizzy, Character charRef)
    {
        if (dizzy)
        {
            return GetDizzyStatement(charRef);
        }
        return $"I am the Confessor";
    }

    public string GetDizzyStatement(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> statements = new();
        statements.Add("I am dizzy"); // Confessor

        statements.Add("I confess!"); // Wingidon
        statements.Add("It was me!"); // Wingidon
        statements.Add("I did it!"); // Wingidon
        statements.Add("I am a cinnamon roll"); // Wingidon
        statements.Add("I am not the Confessor"); // Wingidon
        statements.Add("I could be the Confessor"); // Wingidon
        statements.Add("Am I the Confessor?"); // Wingidon
        statements.Add("Am I a real Confessor?"); // Wingidon
        statements.Add("Uh, line?"); // Wingidon
        statements.Add("I am definitely Good"); // Wingidon
        statements.Add("I am 100% Good"); // Wingidon
        statements.Add("I'm Good, totally!"); // Wingidon

        statements.Add("I am about to cross a road!"); // Tom Scott, anyone?
        statements.Add("I am putting myself at risk of being stabbed"); // Tom Scott
        statements.Add("I am not inside a village"); // Tom Scott, suggested for Dupery Bluff's Priest by @funkythecreature on the Gilded Rune Games Discord.
        statements.Add("The Confessor is dead"); // Suggested by Puppydog101 on Discord for Dupery Bluff's Priest.
        statements.Add("I killed the Confessor"); // Suggested by Puppydog101 on Discord for Dupery Bluff's Priest.

        statements.Add("I am the Spy"); // TF2

        statements.Add("I am the Journalist"); // Dupery
        statements.Add("I am the Clock Maker"); // Dupery
        statements.Add("I am the Priest"); // Priest (Dupery)
        statements.Add("Am I a Good Confessor?"); // Priest (Dupery)
        statements.Add("I don't feel so good..."); // Priest (Dupery)
        statements.Add("I was the Confessor"); // Priest (Dupery)
        statements.Add("I'll be the Confessor"); // Priest (Dupery)
        statements.Add("I am the Drunkard"); // Drunkard (Dupery)

        statements.Add("I am a goldfish"); // Nod to NoLucksGiven

        statements.Add("I was a Confessor"); // Baker
        statements.Add("I am the original Confessor"); // Baker
        statements.Add("I was a Baker"); // Baker
        statements.Add("I am the original Baker"); // Baker
        statements.Add("I was a Priest"); // Baker, Dupery
        statements.Add($"#{charRef.id} is Good"); // Gemcrafter
        statements.Add($"#{charRef.id} is a real Confessor"); // Medium
        statements.Add($"#{charRef.id} is the Confessor"); // Medium (Logic Revamped)
        statements.Add("I am the Drunk"); // Drunk
        statements.Add("Get it twisted!"); // Rambler
        statements.Add("I am a rounding error"); // Rambler
        statements.Add("I am the Bombardier"); // Bombardier
        statements.Add("I am the Rambler"); // Rambler
        statements.Add("I am the Wretch"); // Wretch
        statements.Add("I am the Minion"); // Minion
        statements.Add("I am the Twin Minion"); // Twinion
        statements.Add("I am the Demon"); // Demon

        statements.Add("I am the Boffin"); // Blood on the Clocktower
        statements.Add("I am the Kazali"); // Blood on the Clocktower
        statements.Add("I am the Imp"); // Blood on the Clocktower
        statements.Add("I am the Mezepheles"); // Blood on the Clocktower
        // statements.Add("I am the Nightwatchman"); // Blood on the Clocktower // Nightwatchman overflows to the next line.
        statements.Add("I am the Steward"); // Blood on the Clocktower
        return statements[UnityEngine.Random.RandomRangeInt(0, statements.Count)];
    }
    public w_vConfessorAlternate() : base(ClassInjector.DerivedConstructorPointer<w_vConfessorAlternate>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vConfessorAlternate(System.IntPtr ptr) : base(ptr)
    {
    }
}