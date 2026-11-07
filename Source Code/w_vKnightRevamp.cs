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
public class w_vKnightRevamp : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Can't die. Noteworthy.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        bool extraFlavour = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("RoleFlavour").Value;
        if (extraFlavour) OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        bool extraFlavour = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("RoleFlavour").Value;
        if (extraFlavour) OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo(ConjureInfo());
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.alignment == EAlignment.Evil) return true;
        if (charRef.statuses.Contains(ECharacterStatus.Corrupted)) return true;
        if (charRef.statuses.Contains(ECharacterStatus.BrokenAbility)) return true;

        if (CheckConfigOption_Bool("RoleFlavour")) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(GetFailAttackLine()));
        return false;
    }


    public string GetFailAttackLine()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        for (int i = 0; i < 3; i++)
        {
            remarks.Add("I can't die");
            remarks.Add("I am immortal");
            remarks.Add("I am safe from execution");
            remarks.Add("I am safe from the Demon");
            remarks.Add("I am safe from your power");
            remarks.Add("I will guard this village until my final breath");
            remarks.Add("I will stand my ground");
            remarks.Add("I will never fall");
            remarks.Add("My armour is impervious");
            remarks.Add("My guard is unfaltering");
            remarks.Add("My armour is impenetrable");
            remarks.Add("My guard is unbreakable");
            remarks.Add("I will protect this village until my dying breath");
            remarks.Add("Careful!");
            remarks.Add("Watch where you're pointing that thing!");
            remarks.Add("Nearly caught me off-guard there!");
            remarks.Add("Understandable");
            remarks.Add("Not today!");
            remarks.Add("I live!");
            remarks.Add("I won't fall. Not today, at least");
            remarks.Add("Today is not the day I die");
            remarks.Add("You think I'm suspicious? I'm offended!\n\nKidding, you know I'm fine with being tested");
            remarks.Add("Reminds me of that one time with the Slayer");
            remarks.Add("Parried!");
            remarks.Add("Blocked!");
            remarks.Add("Cheers for the practice!");
        }

        // Easter egg
        remarks.Add("As David said to Goliath, as Theseus said to the Minotaur, as Arjuna said to Bhagadatta...\n\nNo.");
        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }
    public string ConjureInfo()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        for (int i = 0; i < 3; i++)
        {
            remarks.Add("I can't die");
            remarks.Add("I am immortal");
            remarks.Add("I am safe from execution");
            remarks.Add("I am safe from the Demon");
            remarks.Add("I am safe from your power");
            remarks.Add("I will guard this village until my final breath");
            remarks.Add("I will stand my ground");
            remarks.Add("I will never fall");
            remarks.Add("I am ready");
            remarks.Add("Ready for action");
            remarks.Add("Ready!");
            remarks.Add("My armour is impervious");
            remarks.Add("My guard is unfaltering");
            remarks.Add("My armour is impenetrable");
            remarks.Add("My guard is unbreakable");
            remarks.Add("I will protect this village until my dying breath");
            remarks.Add("Need something?");
            remarks.Add("If you see trouble, let me know");
            remarks.Add("Something's wrong here...\nI can feel it");
            remarks.Add("This place grows eerily quiet at night");
            remarks.Add("I saw something in the woods the other night, don't let your guard down");
            remarks.Add("Something is watching from the shadows...");
            remarks.Add("They're watching from afar...");
            remarks.Add("I don't like this... Keep your guard high");
            remarks.Add("Something here isn't sitting right with me");
            remarks.Add("You feel it too, don't you?");
            remarks.Add("I swear, this village is haunted...");
            remarks.Add("This place is cursed...");
            remarks.Add("Play its game for now...");
            remarks.Add("Careful!");
            remarks.Add("At attention!");
            remarks.Add("Present!");
            remarks.Add("I'm here!");
        }

        // Easter egg
        remarks.Add("As David said to Goliath, as Theseus said to the Minotaur, as Arjuna said to Bhagadatta...\n\nNo.");
        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }
    public w_vKnightRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vKnightRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vKnightRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}