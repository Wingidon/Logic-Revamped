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
public class w_oWretchRevamped : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo(ConjureInfo());
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "Registers as Evil";
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

    public override CharacterData GetRegisterAsRole(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleMinions = new();
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (character.type == ECharacterType.Minion && character.startingAlignment == EAlignment.Evil) possibleMinions.Add(character);
        }

        if (possibleMinions.Count == 0)
        {
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.type == ECharacterType.Minion && character.startingAlignment == EAlignment.Evil) possibleMinions.Add(character);
            }
        }

        if (possibleMinions.Count == 0) possibleMinions.Add(ProjectContext.Instance.gameData.GetCharacterDataOfId("Minion_71804875"));

        return possibleMinions[UnityEngine.Random.RandomRangeInt(0, possibleMinions.Count)];
    }

    public string ConjureInfo()
    {
        Il2CppSystem.Collections.Generic.List<string> remarks = new();
        for (int i = 0; i < 2; i++)
        {
            remarks.Add("I am Good, but the village doesn't agree with me");
            remarks.Add("I don't think they like me...");
            remarks.Add("They think I'm Evil!");
            remarks.Add("To be frank, I've gotten used to it by now...");
            remarks.Add("This feels like something I shouldn't be getting used to...");
            remarks.Add("Whatever, I'm used to it by now...");
            remarks.Add("Nobody ever listens...");
            remarks.Add("I didn't say a word!");
            remarks.Add("I didn't do a thing!");
            remarks.Add("Whenever anything goes wrong, they all want someone to blame...");
            remarks.Add("They all want someone to blame...");
            remarks.Add("I've taken up entomology recently!");
        }

        // Easter egg
        remarks.Add("Last week, the Dreamer called me a cabbage\n\nI still don't know what she means by that, I think it might be an insult of some kind?");

        return remarks[UnityEngine.Random.RandomRangeInt(0, remarks.Count)];
    }

    public w_oWretchRevamped() : base(ClassInjector.DerivedConstructorPointer<w_oWretchRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oWretchRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}