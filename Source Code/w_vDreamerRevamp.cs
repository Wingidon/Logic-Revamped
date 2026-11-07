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
public class w_vDreamerRevamp : w_zRoleBase
{
    Character chRef;
    private Il2CppSystem.Action action1;
    private Il2CppSystem.Action action2;
    private Il2CppSystem.Action action3;
    private void CharacterPicked()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;

        string blindVal = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry("BlindModeDreamer").GetValueAsString();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);

        Il2CppSystem.Collections.Generic.List<string> inPlayRoles = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> outOfPlayRoles = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (Character character in selection)
        {
            inPlayRoles.Add(character.GetRegisterAs().characterName);
        }

        // Prioritise mentioning character bluffs
        foreach (Character character in selection)
        {
            if (character.bluff) outOfPlayRoles.Add(character.bluff.characterName);
        }
        foreach (string role in inPlayRoles)
        {
            if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
        }

        if (outOfPlayRoles.Count == 0)
        {
            // Now let's look at Disguised characters on the script.
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
            {
                if (character.usuallyDisguised) outOfPlayRoles.Add(character.characterName);
            }
            if (blindVal == "True" || blindVal == "true")
            {
                if (UnityEngine.Random.RandomRangeInt(0, 10) < 4) // 33% chance
                {
                    //MelonLogger.Msg($"Adding Disguised roles to possible pool. Let's muddy these waters!");
                    foreach (string roleName in GetBluffingRoles())
                    {
                        //MelonLogger.Msg($"Added {roleName} to possible pool.");
                        outOfPlayRoles.Add(roleName);
                    }
                }
            }
            foreach (string role in inPlayRoles)
            {
                if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
            }
        }

        if (outOfPlayRoles.Count == 0 && (blindVal == "True" || blindVal == "true"))
        {
            //MelonLogger.Msg($"Adding Disguised roles to possible pool. Let's muddy these waters!");
            foreach (string roleName in GetBluffingRoles())
            {
                //MelonLogger.Msg($"Added {roleName} to possible pool.");
                outOfPlayRoles.Add(roleName);
            }
            foreach (string role in inPlayRoles)
            {
                if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
            }
        }

        if (outOfPlayRoles.Count == 0)
        {
            // We're stumped! Grab a random false role from the Deck
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
            {
                outOfPlayRoles.Add(character.characterName);
            }
            foreach (string role in inPlayRoles)
            {
                if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
            }
        }

        string inPlayRole = inPlayRoles[UnityEngine.Random.RandomRangeInt(0, inPlayRoles.Count)];
        string outOfPlayRole = outOfPlayRoles[UnityEngine.Random.RandomRangeInt(0, outOfPlayRoles.Count)];



        if (selection[0].dataRef.characterId == "Wretch_80988916" || selection[1].dataRef.characterId == "Wretch_80988916")
        {
            inPlayRole = "a Cabbage";
            outOfPlayRole = "some Lettuce";
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, inPlayRole, outOfPlayRole), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    private void CharacterPickedLiar()
    {
        CharacterPicker.OnCharactersPicked -= action3;
        CharacterPicker.OnStopPick -= action2;
        string blindVal = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry("BlindModeDreamer").GetValueAsString();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        selection.Add(CharacterPicker.PickedCharacters[0]);
        selection.Add(CharacterPicker.PickedCharacters[1]);

        Il2CppSystem.Collections.Generic.List<string> inPlayRoles = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> outOfPlayRoles = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> chosenRoles = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (Character character in selection)
        {
            inPlayRoles.Add(character.GetRegisterAs().characterName);
        }


        for (int i = 0; i < 2; i++)
        {
            // Prioritise mentioning character bluffs
            foreach (Character character in selection)
            {
                if (character.bluff) outOfPlayRoles.Add(character.bluff.characterName);
            }
            foreach (string role in inPlayRoles)
            {
                if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
            }
            if (chosenRoles.Count != 0)
            {
                foreach (string role in chosenRoles)
                {
                    if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                }
            }


            if (outOfPlayRoles.Count == 0)
            {
                // Now let's look at Disguised characters on the script.
                foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
                {
                    if (character.usuallyDisguised) outOfPlayRoles.Add(character.characterName);
                }
                if (blindVal == "True" || blindVal == "true")
                {
                    if (UnityEngine.Random.RandomRangeInt(0, 10) < 4) // 33% chance
                    {
                        //MelonLogger.Msg($"Adding Disguised roles to possible pool. Let's muddy these waters!");
                        foreach (string roleName in GetBluffingRoles())
                        {
                            //MelonLogger.Msg($"Added {roleName} to possible pool.");
                            outOfPlayRoles.Add(roleName);
                        }
                    }
                }
                foreach (string role in inPlayRoles)
                {
                    if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                }
                if (chosenRoles.Count != 0)
                {
                    foreach (string role in chosenRoles)
                    {
                        if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                    }
                }
            }

            if (outOfPlayRoles.Count == 0 && (blindVal == "True" || blindVal == "true"))
            {
                //MelonLogger.Msg($"Adding Disguised roles to possible pool. Let's muddy these waters!");
                foreach (string roleName in GetBluffingRoles())
                {
                    //MelonLogger.Msg($"Added {roleName} to possible pool.");
                    outOfPlayRoles.Add(roleName);
                }
                foreach (string role in inPlayRoles)
                {
                    if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                }
                if (chosenRoles.Count != 0)
                {
                    foreach (string role in chosenRoles)
                    {
                        if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                    }
                }
            }

            if (outOfPlayRoles.Count == 0)
            {
                // We're stumped! Grab a random false role from the Deck
                foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
                {
                    outOfPlayRoles.Add(character.characterName);
                }
                foreach (string role in inPlayRoles)
                {
                    if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                }
                if (chosenRoles.Count != 0)
                {
                    foreach (string role in chosenRoles)
                    {
                        if (outOfPlayRoles.Contains(role)) while (outOfPlayRoles.Contains(role)) outOfPlayRoles.Remove(role);
                    }
                }
            }
            chosenRoles.Add(outOfPlayRoles[UnityEngine.Random.RandomRangeInt(0, outOfPlayRoles.Count)]);
        }

        ActedInfo returnInfo = new ActedInfo(ConjureInfo(selection, chosenRoles[0], chosenRoles[1]), selection);
        OnActed(ETriggerPhase.Day, chRef, returnInfo);
    }
    private void StopPick()
    {
        CharacterPicker.OnCharactersPicked -= action1;
        CharacterPicker.OnStopPick -= action2;
        CharacterPicker.OnCharactersPicked -= action3;
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

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, string roleOne, string roleTwo)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        characters = sharedScripts.SortList(characters);
        string totalInfo = $"Among\n#{characters[0].id}, #{characters[1].id},\nthere is:";
        if (sharedScripts.PercentChance(50))
        {
            totalInfo += $"\n{roleOne} or {roleTwo}";
        }
        else
        {
            totalInfo += $"\n{roleTwo} or {roleOne}";
        }
            return totalInfo;
    }

    public Il2CppSystem.Collections.Generic.List<string> GetBluffingRoles()
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new Il2CppSystem.Collections.Generic.List<string>();
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<CharacterData> allRoles = sharedScripts.GetScriptRoles();
        foreach (CharacterData character in allRoles)
        {
            if (character.usuallyDisguised) returnList.Add(character.characterName);
        }
        returnList.Add("Baa");
        returnList.Add("Pooka");
        returnList.Add("Lilis"); // Manually add the Demons for optimisation's sake.
        returnList.Remove("Mutant");
        returnList.Remove("Delusion");
        return returnList;
    }
    public w_vDreamerRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vDreamerRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
    public w_vDreamerRevamp(System.IntPtr ptr) : base(ptr)
    {
        action1 = new System.Action(CharacterPicked);
        action2 = new System.Action(StopPick);
        action3 = new System.Action(CharacterPickedLiar);
    }
}