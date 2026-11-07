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
public class w_vDruidLearn : w_zRoleBase
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            possibleTargets.Add(character);
        }
        possibleTargets.Remove(charRef);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[0]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[1]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        Il2CppSystem.Collections.Generic.List<Character> outcastsFound = new Il2CppSystem.Collections.Generic.List<Character>();

        foreach (Character character in selection)
        {
            if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                outcastsFound.Add(character);
            }
        }

        string outcastName = "0";
        if (outcastsFound.Count != 0)
        {
            outcastName = outcastsFound[UnityEngine.Random.RandomRangeInt(0, outcastsFound.Count)].GetRegisterAs().characterName;
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, outcastName), selection);
        return returnInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            possibleTargets.Add(character);
        }
        possibleTargets.Remove(charRef);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[0]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
        possibleTargets.Remove(selection[1]);
        selection.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);

        Il2CppSystem.Collections.Generic.List<Character> outcastsFound = new Il2CppSystem.Collections.Generic.List<Character>();

        foreach (Character character in selection)
        {
            if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                outcastsFound.Add(character);
            }
        }

        Il2CppSystem.Collections.Generic.List<string> possibleAnswers = new Il2CppSystem.Collections.Generic.List<string>();
        if (outcastsFound.Count != 0) possibleAnswers.Add("0"); // If there's at least one Outcast, NO Outcasts is a possible answer

        Il2CppSystem.Collections.Generic.List<CharacterData> possibleOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in selection)
        {
            if (character.bluff)
            {
                if (character.bluff.type == ECharacterType.Outcast)
                {
                    possibleOutcasts.Add(character.bluff);
                }
            }
        }


        string blindVal = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry("BlindModeDruid").GetValueAsString();
        if (blindVal == "True" || blindVal == "true")
        {
            MelonLogger.Msg($"Adding Disguised roles to possible pool. Let's muddy these waters!");
            foreach (CharacterData roleName in GetBluffingRoles())
            {
                MelonLogger.Msg($"Added {roleName.characterName} to possible pool.");
                possibleOutcasts.Add(roleName);
            }
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
        {
            if (character.usuallyDisguised) possibleOutcasts.Add(character);
        }
        foreach (Character character in selection)
        {
            if (possibleOutcasts.Contains(character.GetRegisterAs()))
            {
                while (possibleOutcasts.Contains(character.GetRegisterAs()))
                {
                    possibleOutcasts.Remove(character.GetRegisterAs());
                }
            }
        }

        if (possibleOutcasts.Count != 0)
        {
            foreach (CharacterData character in possibleOutcasts)
            {
                possibleAnswers.Add(character.characterName);
            }
        }
        if (possibleAnswers.Count == 0)
        {
            // No possible responses yet, huh? Alright, say something obviously false.
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
            {
                possibleOutcasts.Add(character);
            }
            foreach (Character character in selection)
            {
                if (possibleOutcasts.Contains(character.GetRegisterAs()))
                {
                    while (possibleOutcasts.Contains(character.GetRegisterAs()))
                    {
                        possibleOutcasts.Remove(character.GetRegisterAs());
                    }
                }
            }

            if (possibleOutcasts.Count != 0)
            {
                foreach (CharacterData character in possibleOutcasts)
                {
                    possibleAnswers.Add(character.characterName);
                }
            }
        }
        if (possibleAnswers.Count == 0)
        {
            // No possible responses still?? Damn, okay!
            foreach (CharacterData character in new wx_SavedScripts().GetScriptRoles(ECharacterType.Outcast))
            {
                possibleOutcasts.Add(character);
            }
            foreach (Character character in selection)
            {
                if (possibleOutcasts.Contains(character.GetRegisterAs()))
                {
                    while (possibleOutcasts.Contains(character.GetRegisterAs()))
                    {
                        possibleOutcasts.Remove(character.GetRegisterAs());
                    }
                }
            }

            if (possibleOutcasts.Count != 0)
            {
                foreach (CharacterData character in possibleOutcasts)
                {
                    possibleAnswers.Add(character.characterName);
                }
            }
        }



        string outcastName = "";
        if (possibleAnswers.Count == 0)
        {
            return new ActedInfo("Something does not make sense");
        }
        outcastName = possibleAnswers[UnityEngine.Random.RandomRangeInt(0, possibleAnswers.Count)];

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, outcastName), selection);
        return returnInfo;
    }
    public override string Description
    {
        get
        {
            return "Learns 3 characters & an Outcast among them, if any.";
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

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, string outcastName)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        string allThree = $"{sharedScripts.MentionEveryCharacterInList(characters, "")}";
        string outcastAnnounce = $"there is:\n{outcastName}";
        if (outcastName == "0") outcastAnnounce = $"there are:\nNO Outcasts";
        string totalInfo = $"Among {allThree} {outcastAnnounce}";
        return totalInfo;
    }
    public Il2CppSystem.Collections.Generic.List<CharacterData> GetBluffingRoles()
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<CharacterData> allOutcasts = sharedScripts.GetScriptRoles(ECharacterType.Outcast);
        foreach (CharacterData character in allOutcasts)
        {
            if (character.usuallyDisguised) returnList.Add(character);
        }
        return returnList;
    }
    public w_vDruidLearn() : base(ClassInjector.DerivedConstructorPointer<w_vDruidLearn>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vDruidLearn(System.IntPtr ptr) : base(ptr)
    {
    }
}