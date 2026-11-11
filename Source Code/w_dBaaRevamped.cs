using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.InteropTypes.Fields;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_dBaaRevamped : w_zRoleBase
{
    Il2CppSystem.Collections.Generic.List<CharacterData> blockedRoles = new();

    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override string Description
    {
        get
        {
            return "Lies and Disguises. Kills at night.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.OnDied)
        {
            bool revealRolesOnDeath = CheckConfigOption_Bool("BaaRevealDeath");
            if (!revealRolesOnDeath) return;
            if (blockedRoles.Count != 0)
            {
                foreach (CharacterData character in blockedRoles)
                {
                    DeckView.RemoveObscuredDeckView(character);
                }
            }
        }
        if (trigger != ETriggerPhase.AfterRoundStart) return;
        /*
        configCategory.CreateEntry("BaaHiddenOutcasts", 1, "BaaHiddenOutcasts", "\n\n\nDEMONS\nHow many Outcast roles should Baa hide from the Deck?\nIf set to 4 or higher, Baa hides every Outcast in the Deck.");
        configCategory.CreateEntry("BaaHiddenMinions", 0, "BaaHiddenMinions", "\nHow many Minion roles should Baa hide from the Deck?\nIf set to 4 or higher, Baa hides every Minion in the Deck.");
        configCategory.CreateEntry("BaaRevealDeath", true, "BaaRevealDeath", "\nIf disabled, Baa will not reveal the hidden roles on death.");
        configCategory.CreateEntry("BaaAddOutcastsMin", 0, "BaaAddOutcastsMin", "\nThe minimum number of Outcasts that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddOutcastsMax", 0, "BaaAddOutcastsMax", "\nThe maximum number of Outcasts that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddMinionsMin", 0, "BaaAddMinionsMin", "\nThe minimum number of Minions that Baa can add to the Deck.");
        configCategory.CreateEntry("BaaAddMinionsMax", 0, "BaaAddMinionsMax", "\nThe maximum number of Minions that Baa can add to the Deck.");
        */

        if (allDatas.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                allDatas = new CharacterData[loadedCharList.Length];
                for (int j = 0; j < loadedCharList.Length; j++)
                {
                    allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                }
            }
        }

        int hiddenOutcasts = CheckConfigOption_Int("BaaHiddenOutcasts");
        int hiddenMinions = CheckConfigOption_Int("BaaHiddenMinions");
        bool revealDeath = CheckConfigOption_Bool("BaaRevealDeath");
        int addOutcastsMin = CheckConfigOption_Int("BaaAddOutcastsMin");
        int addOutcastsMax = CheckConfigOption_Int("BaaAddOutcastsMax");
        int addMinionsMin = CheckConfigOption_Int("BaaAddMinionsMin");
        int addMinionsMax = CheckConfigOption_Int("BaaAddMinionsMax");

        bool hideAllOutcasts = hiddenOutcasts >= 4;
        bool hideAllMinions = hiddenMinions >= 4;


        // First, let's add roles to the Deck.
        int addOutcasts = UnityEngine.Random.RandomRangeInt(addOutcastsMin, addOutcastsMax + 1);
        int addMinions = UnityEngine.Random.RandomRangeInt(addMinionsMin, addMinionsMax + 1);
        Il2CppSystem.Collections.Generic.List<CharacterData> allOutcasts = new();
        Il2CppSystem.Collections.Generic.List<CharacterData> allMinions = new();
        foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
        {
            if (character.type == ECharacterType.Outcast) allOutcasts.Add(character);
            if (character.type == ECharacterType.Minion) allMinions.Add(character);
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            allOutcasts.Remove(character);
            allMinions.Remove(character);
        }

        if (addOutcasts != 0)
        {
            for (int i = 0; i < addOutcasts; i++)
            {
                if (allOutcasts.Count != 0)
                {
                    CharacterData addedOutcast = allOutcasts[UnityEngine.Random.RandomRangeInt(0, allOutcasts.Count)];
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Outcast, addedOutcast);
                    allOutcasts.Remove(addedOutcast);
                }
            }
        }

        if (addMinions != 0)
        {
            for (int i = 0; i < addMinions; i++)
            {
                if (allMinions.Count != 0)
                {
                    CharacterData addedMinion = allMinions[UnityEngine.Random.RandomRangeInt(0, allMinions.Count)];
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, addedMinion);
                    allMinions.Remove(addedMinion);
                }
            }
        }



        // Lycan/Puppeteer fix
        Il2CppSystem.Collections.Generic.List<CharacterData> scriptChars = Gameplay.Instance.GetScriptCharacters();
        foreach (CharacterData character in scriptChars)
        {
            if (character.bundledCharacters.Count != 0)
            {
                foreach (CharacterData bundledChar in character.bundledCharacters)
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(bundledChar.type, bundledChar);
                }
            }
        }


        // Handle hidden Outcasts
        if (hideAllOutcasts)
        {
            foreach (CharacterData character in allDatas)
            {
                if (character.type == ECharacterType.Outcast) blockedRoles.Add(character);
            }
        }
        else if (hiddenOutcasts != 0)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> outcasts = new();
            Il2CppSystem.Collections.Generic.List<CharacterData> faceDownOutcasts = new();
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
            {
                outcasts.Add(character);
                if (character.usuallyDisguised) faceDownOutcasts.Add(character);
            }

            for (int i = 0; i < hiddenOutcasts; i++)
            {
                if (faceDownOutcasts.Count != 0)
                {
                    CharacterData hiddenOutcast = faceDownOutcasts[UnityEngine.Random.RandomRangeInt(0, faceDownOutcasts.Count)];
                    blockedRoles.Add(hiddenOutcast);
                    faceDownOutcasts.Remove(hiddenOutcast);
                    outcasts.Remove(hiddenOutcast);
                }
                else if (outcasts.Count != 0)
                {
                    CharacterData hiddenOutcast = outcasts[UnityEngine.Random.RandomRangeInt(0, outcasts.Count)];
                    blockedRoles.Add(hiddenOutcast);
                    outcasts.Remove(hiddenOutcast);
                }
            }
        }

        // ...and hidden Minions
        if (hideAllMinions)
        {
            foreach (CharacterData character in allDatas)
            {
                if (character.type == ECharacterType.Minion) blockedRoles.Add(character);
            }
        }
        else if (hiddenMinions != 0)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> minions = new();
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion))
            {
                minions.Add(character);
            }

            for (int i = 0; i < hiddenMinions; i++)
            {
                if (minions.Count != 0)
                {
                    CharacterData hiddenMinion = minions[UnityEngine.Random.RandomRangeInt(0, minions.Count)];
                    blockedRoles.Add(hiddenMinion);
                    minions.Remove(hiddenMinion);
                }
            }
        }





        Il2CppSystem.Collections.Generic.List<CharacterData> inPlayRoles = new(); // For optimisation's sake.
        foreach (Character character in Gameplay.CurrentCharacters) inPlayRoles.Add(character.dataRef);

        // Actually hide everything now.
        if (blockedRoles.Count != 0)
        {
            foreach (CharacterData character in blockedRoles)
            {
                SetMessedByEvil(character, charRef, inPlayRoles);
                DeckView.AddToObscuredDeckView(character);
            }
        }
    }

    private void SetMessedByEvil(CharacterData pickedOutcast, Character charRef, Il2CppSystem.Collections.Generic.List<CharacterData> inPlayRoles)
    {
        if (!inPlayRoles.Contains(pickedOutcast)) return;
        foreach (Character ch in Gameplay.CurrentCharacters)
            if (ch.dataRef == pickedOutcast)
                ch.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, CheckConfigOption_Bool("DemonOutcastDisguise"));
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_dBaaRevamped() : base(ClassInjector.DerivedConstructorPointer<w_dBaaRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_dBaaRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}