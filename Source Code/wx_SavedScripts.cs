using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppRewired.Glyphs;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static MelonLoader.MelonLaunchOptions;

namespace LogicRevampedMod
{
    // Token: 0x02000007 RID: 7
    [RegisterTypeInIl2Cpp]
    public class wx_SavedScripts : w_zRoleBase
    {
        // Token: 0x06000019 RID: 25 RVA: 0x0000359C File Offset: 0x0000179C
        public wx_SavedScripts() : base(ClassInjector.DerivedConstructorPointer<wx_SavedScripts>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000035B2 File Offset: 0x000017B2
        public wx_SavedScripts(IntPtr ptr) : base(ptr)
        {
        }






        // This is where I store my miscellaneous scripts and some various things that aren't tied to any particular character.

        public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            for (int i = 0; i < Gameplay.CurrentCharacters.Count + 3; i++)
            {
                foreach (Character character in list)
                {
                    if (character.id == i) newList.Add(character);
                }
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<ECharacterType> SortList(Il2CppSystem.Collections.Generic.List<ECharacterType> list)
        {
            Il2CppSystem.Collections.Generic.List<ECharacterType> newList = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Villager)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Outcast)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Minion)
                {
                    newList.Add(characterType);
                }
            }
            foreach (ECharacterType characterType in list)
            {
                if (characterType == ECharacterType.Demon)
                {
                    newList.Add(characterType);
                }
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<Character> GetFakeEvilTeam()
        {
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Character target = new Character();
            int totalEvils = 0;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                possibleTargets.Add(character);
                if (character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    totalEvils++;
                }
            }
            for (int i = 0; i < totalEvils; i++)
            {
                target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                newList.Add(target);
                possibleTargets.Remove(target);
            }
            return newList;
        }
        public Il2CppSystem.Collections.Generic.List<Character> GetFakeGroup(Character charRef, Il2CppSystem.Collections.Generic.List<Character> targets)
        {
            // MelonLogger.Msg("Getting fake group");
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Character target = new Character();
            int totalTargets = targets.Count;
            // MelonLogger.Msg($"Fake group has {totalTargets} characters in it");
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                possibleTargets.Add(character);
            }
            possibleTargets.Remove(charRef);
            for (int i = 0; i < totalTargets; i++)
            {
                if (possibleTargets.Count == 0) break;
                target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                // MelonLogger.Msg($"Added #{target.id} to fake group");
                newList.Add(target);
                possibleTargets.Remove(target);
            }
            MelonLogger.Msg($"Fake group is {MentionEveryCharacterInList(newList, "and")}");
            return newList;
        }

        public int GetDistanceBetweenCharacters(Character char1, Character char2)
        {
            int totalCharCount = Gameplay.CurrentCharacters.Count;
            int tempDist = char1.id - char2.id;
            if (tempDist < 0)
            {
                tempDist = tempDist + totalCharCount;
            }
            int tempDist2 = char2.id - char1.id;
            if (tempDist2 < 0)
            {
                tempDist2 = tempDist2 + totalCharCount;
            }
            if (tempDist > tempDist2)
            {
                return tempDist2;
            }
            return tempDist;
        }

        public int GetClosestDistance(Il2CppSystem.Collections.Generic.List<Character> targets, Character anchor)
        {
            int dist = 10000;
            int calcDist = 0;
            foreach (Character character in targets)
            {
                calcDist = GetDistanceBetweenCharacters(character, anchor);
                if (calcDist != 0 && dist > calcDist)
                {
                    dist = calcDist;
                }
            }
            return dist;
        }

        public int GetFurthestDistance(Il2CppSystem.Collections.Generic.List<Character> targets, Character anchor)
        {
            int dist = 0;
            int calcDist = 0;
            foreach (Character character in targets)
            {
                calcDist = GetDistanceBetweenCharacters(character, anchor);
                if (calcDist != 0 && dist < calcDist)
                {
                    dist = calcDist;
                }
            }
            return dist;
        }


        public float GetTrustworthiness(Character target)
        {
            float trust = 1f;
            if (target.GetRegisterAs().type == ECharacterType.Villager) trust *= 5;
            if (target.GetRegisterAs().type == ECharacterType.Outcast) trust *= 3;
            if (target.GetRegisterAs().type == ECharacterType.Minion) trust *= 3;
            if (target.GetRegisterAlignment() == EAlignment.Good) trust *= 3;
            if (!CharacterHelper.CheckLyingAppearance(target)) trust *= 3;
            if (!CharacterHelper.CheckIfDisguisedAppearance(target)) trust *= 2.5f;
            return trust;
        }

        public string MentionEveryCharacterInList(Il2CppSystem.Collections.Generic.List<Character> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<Character> sortedCharacters = SortList(characters);
            foreach (Character character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"#{character.id}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and #{character.id}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or #{character.id}";
                        }
                        else
                        {
                            returnString = $"{returnString}, #{character.id}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, #{character.id}";
                    }
                }
            }
            return returnString;
        }


        public string MentionEveryRoleInList(Il2CppSystem.Collections.Generic.List<CharacterData> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<CharacterData> sortedCharacters = characters;
            foreach (CharacterData character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"{character.characterName}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {character.characterName}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {character.characterName}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {character.characterName}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {character.characterName}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryStringInList(Il2CppSystem.Collections.Generic.List<string> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<string> sortedCharacters = characters;
            foreach (CharacterData character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"{character}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {character}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {character}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {character}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {character}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryCharacterInUnsortedList(Il2CppSystem.Collections.Generic.List<Character> characters, string andOr)
        {
            string returnString = "Return";
            int characterCount = characters.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<Character> sortedCharacters = characters;
            foreach (Character character in sortedCharacters)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = $"#{character.id}";
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and #{character.id}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or #{character.id}";
                        }
                        else if (andOr == "Then" || andOr == "then")
                        {
                            returnString = $"{returnString}, then #{character.id}";
                        }
                        else
                        {
                            returnString = $"{returnString}, #{character.id}";
                        }
                    }
                    else if (andOr == "Then" || andOr == "then")
                    {
                        returnString = $"{returnString}, then #{character.id}";
                    }
                }
            }
            return returnString;
        }

        public string MentionEveryTypeInList(Il2CppSystem.Collections.Generic.List<ECharacterType> types, string andOr)
        {
            string returnString = "Return";
            int characterCount = types.Count;
            int counter = 0;
            Il2CppSystem.Collections.Generic.List<ECharacterType> sortedTypes = SortList(types);
            foreach (ECharacterType type in sortedTypes)
            {
                if (returnString == "Return")
                {
                    counter++;
                    returnString = type.ToString();
                }
                else
                {
                    counter++;
                    if (counter == characterCount)
                    {
                        if (andOr == "And" || andOr == "and")
                        {
                            returnString = $"{returnString} and {type.ToString()}";
                        }
                        else if (andOr == "Or" || andOr == "or")
                        {
                            returnString = $"{returnString} or {type.ToString()}";
                        }
                        else
                        {
                            returnString = $"{returnString}, {type.ToString()}";
                        }
                    }
                    else
                    {
                        returnString = $"{returnString}, {type.ToString()}";
                    }
                }
            }
            return returnString;
        }

        public bool CheckIfNeighbour(Character character1, Character character2)
        {
            if (GetDistanceBetweenCharacters(character1, character2) == 1) return true;
            return false;
        }
        public bool PercentChance(float percentage)
        {
            if (UnityEngine.Random.RandomRange(0, 100) <= percentage) return true;
            return false;
        }

        public Il2CppSystem.Collections.Generic.List<Character> GetCharacterNeighbours(Character targetChar)
        {
            Il2CppSystem.Collections.Generic.List<Character> outputList = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (CheckIfNeighbour(targetChar, character)) outputList.Add(character);
            }
            return outputList;
        }

        public Il2CppSystem.Collections.Generic.List<Character> GetCharactersWithinRange(Character charRef, int range)
        {
            Il2CppSystem.Collections.Generic.List<Character> outputList = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (GetDistanceBetweenCharacters(charRef, character) <= range && character != charRef)
                {
                    outputList.Add(character);
                }
            }
            return outputList;
        }



        public Il2CppSystem.Collections.Generic.List<string> AddStringToList(string input, Il2CppSystem.Collections.Generic.List<string> list, int weight)
        {
            for (int i = 0; i < weight; i++)
            {
                list.Add(input);
            }
            return list;
        }


        public Il2CppSystem.Collections.Generic.List<Character> GetClosestCharacters(Character charRef, ECharacterType type, EAlignment alignment)
        {
            Il2CppSystem.Collections.Generic.List<Character> returnList = new();

            int checkDist = 10000;
            int curDist = 10000;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != charRef)
                {
                    if (type == ECharacterType.None || (character.GetRegisterAs().type == type && character.dataRef.type == type)) // Check Type
                    {
                        if (alignment == EAlignment.None || (character.GetRegisterAlignment() == alignment && character.dataRef.startingAlignment == alignment)) // Check Alignment
                        {
                            checkDist = GetDistanceBetweenCharacters(charRef, character);
                            if (checkDist == curDist) returnList.Add(character);
                            if (checkDist < curDist)
                            {
                                returnList.Clear();
                                curDist = checkDist;
                                returnList.Add(character);
                            }
                        }
                    }
                }
            }

            DebugMessage($"Closest characters: {MentionEveryCharacterInList(returnList, "")}");

            return returnList;
        }


        public CharacterData GetFaceUpClaim(Character character)
        {
            if (character.bluff) return character.bluff;
            return character.dataRef;
        }

        public string CheckIfThe(string Name)
        {
            Il2CppSystem.Collections.Generic.List<string> pluralNamesV = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a vowel. #X is an Y.
            Il2CppSystem.Collections.Generic.List<string> pluralNamesC = new Il2CppSystem.Collections.Generic.List<string>(); // There may be many of this character, they start with a consonant. #X is a Y.
            Il2CppSystem.Collections.Generic.List<string> demonNames = new Il2CppSystem.Collections.Generic.List<string>(); // This character has an actual name. #X is Y.
                                                                                                                            // Minions
            demonNames.Add("Swarm");
            pluralNamesV.Add("Acolyte");
            pluralNamesC.Add("Fanatic");
            pluralNamesC.Add("Mastermind"); // From Skill Cycler's Riddles mod
            pluralNamesC.Add("Zealot");

            // Vanilla
            demonNames.Add("Baa");
            demonNames.Add("Lilis");
            demonNames.Add("Pooka");

            // Wingidon's Expansion Pack
            demonNames.Add("Agmeres");
            demonNames.Add("Caedoccidere");
            demonNames.Add("Carnicarius");
            demonNames.Add("Emenverax");
            demonNames.Add("Iris");
            demonNames.Add("Leviathan");
            demonNames.Add("Magnere");
            demonNames.Add("Mendaverte");
            demonNames.Add("Praesect");
            demonNames.Add("Sanguitaurus");
            demonNames.Add("Specularus");
            demonNames.Add("Tenecaligo");
            demonNames.Add("Venelum");
            demonNames.Add("Veniyon");
            demonNames.Add("Vidiyon");
            demonNames.Add("Viciyon");

            // Role Ideas Collection
            demonNames.Add("Death");

            // ExtraRandomized
            demonNames.Add("Better Baa");

            // CarlzVilliagePack
            pluralNamesC.Add("Hydra");
            demonNames.Add("Pestilence");

            // CSK's Expansion Pack
            demonNames.Add("Belias");
            if (pluralNamesV.Contains(Name))
            {
                return "an ";
            }
            if (pluralNamesC.Contains(Name))
            {
                return "a ";
            }
            if (demonNames.Contains(Name))
            {
                return "";
            }
            return "the ";
        }




        public CharacterData GetOverrideNotInPlayBluff(Character charRef, bool outcastsAllowed)
        {
            CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
            Il2CppSystem.Collections.Generic.List<string> blacklistBluffs = new Il2CppSystem.Collections.Generic.List<string>();
            blacklistBluffs.Add("Bounty Hunter_39284184");
            if (charRef.dataRef.characterId == "Iris_WING") blacklistBluffs.Add("Baker_22847064");
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = Characters.Instance.FilterAlignmentCharacters(Gameplay.Instance.GetAllAscensionCharacters(), EAlignment.Good);
            Il2CppSystem.Collections.Generic.List<CharacterData> removeBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            possibleBluffs = Characters.Instance.FilterBluffableCharacters(possibleBluffs);
            foreach (CharacterData character in possibleBluffs)
            {
                if (blacklistBluffs.Contains(character.characterId))
                {
                    removeBluffs.Add(character);
                }
                if (!outcastsAllowed && character.type == ECharacterType.Outcast)
                {
                    removeBluffs.Add(character);
                }
            }
            foreach (CharacterData character in removeBluffs)
            {
                possibleBluffs.Remove(character);
            }
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                while (possibleBluffs.Contains(character.dataRef))
                {
                    possibleBluffs.Remove(character.dataRef);
                }
            }
            if (possibleBluffs.Count != 0)
            {
                bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
            }
            DebugMessage($"#{charRef.id} chose Disguise of {bluff.characterName}");
            return bluff;
        }

        public int RoundValToInt(decimal val)
        {
            return (int)Math.Round(val);
        }

        public CharacterData GetOverrideDuplicateBluff(Character charRef, bool outcastsAllowed)
        {
            //DebugMessage("Grabbing duplicate bluff...");
            CharacterData bluff = new();
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                //DebugMessage($"Checking the character in seat #{character.id}");
                //DebugMessage($"They are the {character.alignment.ToString()} {character.dataRef.type.ToString()} {character.dataRef.characterName}.");
                if (character.dataRef.bluffable == true && character.dataRef.startingAlignment == EAlignment.Good)
                {
                    //DebugMessage($"#{character.id} is Good and bluffable!");
                    if (outcastsAllowed || character.dataRef.type != ECharacterType.Outcast)
                    {
                        //DebugMessage($"Adding {character.dataRef.characterName} as possible bluff...");
                        possibleBluffs.Add(character.dataRef);
                    }
                }
            }
            if (possibleBluffs.Count != 0)
            {
                bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
            }
            else bluff = Characters.Instance.GetRandomDuplicateBluff();
            //DebugMessage($"Chose {bluff.characterName} as bluff.");
            DebugMessage($"#{charRef.id} chose Disguise of {bluff.characterName}");
            return bluff;
        }

        public void TurnEvilIfPossible(Character character)
        {

            if (CheckIfAlwaysGood(character)) character.ChangeAlignment(EAlignment.Evil);
        }

        public bool CheckIfAlwaysGood(Character character)
        {
            Il2CppSystem.Collections.Generic.List<string> alwaysGoodIDs = new Il2CppSystem.Collections.Generic.List<string>();
            alwaysGoodIDs.Add("Saint_61372493");
            alwaysGoodIDs.Add("Politician_WING");
            alwaysGoodIDs.Add("Saint_WING");

            if (alwaysGoodIDs.Contains(character.dataRef.characterId)) return true;
            return false;
        }


        public int GetPairsOfCharactersInList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            //MelonLogger.Msg("Getting pairs");
            Il2CppSystem.Collections.Generic.List<Character> allCharactersPlusOne = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                allCharactersPlusOne.Add(character);
            }
            allCharactersPlusOne.Add(Gameplay.CurrentCharacters[0]);

            bool prevCount = false;
            int pairs = 0;
            foreach (Character character in allCharactersPlusOne)
            {
                //MelonLogger.Msg($"Checking #{character.id}");
                if (list.Contains(character) && prevCount == true)
                {
                    pairs++;
                    //MelonLogger.Msg($"Found a pair, there are now {pairs} pair(s)");
                }
                if (list.Contains(character))
                {
                    prevCount = true;
                    //MelonLogger.Msg($"#{character.id} is in the list, so they're ready to be part of the next pair");
                }
                else
                {
                    prevCount = false;
                    //MelonLogger.Msg($"#{character.id} is not in the list, so they will not be part of the next pair");
                }

            }
            return pairs;
        }


        public void DebugMessage(string message)
        {

            bool debugVal = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>("DebugMode").Value;
            if (debugVal)
            {
                MelonLogger.Msg("DEBUG: " + message);
            }

        }



        public ActedInfo ReturnInfoWithSingleSelection(string info, Character selection)
        {
            Il2CppSystem.Collections.Generic.List<Character> selectionList = new Il2CppSystem.Collections.Generic.List<Character>();
            selectionList.Add(selection);
            return new ActedInfo(info, selectionList);
        }



        public Character GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<Character> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public CharacterData GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<CharacterData> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public string GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<string> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }
        public int GetRandomItemOfList(Il2CppSystem.Collections.Generic.List<int> list)
        {
            if (list.Count == 0)
            {
                return -1;
            }
            return list[UnityEngine.Random.RandomRangeInt(0, list.Count)];
        }



        public int MakeNumberWrong(int trueNumber, int falseNumber, int minimum)
        {
            int returnVal = falseNumber;
            if (returnVal < minimum)
            {
                while (returnVal < minimum) returnVal++;
            }
            if (trueNumber != falseNumber) return falseNumber;
            if (falseNumber == minimum) returnVal++;
            else returnVal--;
            return returnVal;
        }

        public int MakeNumberWrongByRange(int trueNumber, int falseNumber, float minimum, float maximum, int maxSubtract, int maxAdd)
        {
            int returnVal = falseNumber;
            if (returnVal < minimum)
            {
                while (returnVal < minimum) returnVal++;
            }
            if (returnVal > maximum)
            {
                while (returnVal > maximum) returnVal--;
            }
            if (trueNumber != falseNumber) return falseNumber;
            Il2CppSystem.Collections.Generic.List<int> possibleModifiers = new Il2CppSystem.Collections.Generic.List<int>();
            for (int i = (maxSubtract * -1); i < (maxAdd + 1); i++)
            {
                if (i != 0 && (returnVal + i <= maximum) && (returnVal + i >= minimum))
                {
                    possibleModifiers.Add(i);
                }
            }
            if (possibleModifiers.Count == 0) return MakeNumberWrong(trueNumber, falseNumber, RoundValToInt((decimal)minimum));
            returnVal += possibleModifiers[UnityEngine.Random.RandomRangeInt(0, possibleModifiers.Count)];
            return returnVal;
        }

        public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
        public CharacterData GrabCharacterDataByID(string characterID)
        {
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

            for (int j = 0; j < allDatas.Length; j++)
            {
                if (characterID == allDatas[j].characterId)
                {
                    return allDatas[j];
                }
            }
            return null;
        }


        public Il2CppSystem.Collections.Generic.List<Character> GetClosestCharacterOnEachSide(Character charRef, string specifications)
        {
            Il2CppSystem.Collections.Generic.List<Character> validTargets = new();
            DebugMessage($"Looking for characters... Criteria: {specifications}");
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                bool add = true;
                if (specifications.Contains("True Villager") && character.dataRef.type != ECharacterType.Villager) add = false;
                if (specifications.Contains("Register Villager") && character.GetRegisterAs().type != ECharacterType.Villager) add = false;
                if (specifications.Contains("Perfect Villager") && (character.GetRegisterAs().type != ECharacterType.Villager || character.dataRef.type != ECharacterType.Villager)) add = false;

                if (specifications.Contains("True Outcast") && character.dataRef.type != ECharacterType.Outcast) add = false;
                if (specifications.Contains("Register Outcast") && character.GetRegisterAs().type != ECharacterType.Outcast) add = false;
                if (specifications.Contains("Perfect Outcast") && (character.GetRegisterAs().type != ECharacterType.Outcast || character.dataRef.type != ECharacterType.Outcast)) add = false;

                if (specifications.Contains("True Minion") && character.dataRef.type != ECharacterType.Minion) add = false;
                if (specifications.Contains("Register Minion") && character.GetRegisterAs().type != ECharacterType.Minion) add = false;
                if (specifications.Contains("Perfect Minion") && (character.GetRegisterAs().type != ECharacterType.Minion || character.dataRef.type != ECharacterType.Minion)) add = false;

                if (specifications.Contains("True Demon") && character.dataRef.type != ECharacterType.Demon) add = false;
                if (specifications.Contains("Register Demon") && character.GetRegisterAs().type != ECharacterType.Demon) add = false;
                if (specifications.Contains("Perfect Demon") && (character.GetRegisterAs().type != ECharacterType.Demon || character.dataRef.type != ECharacterType.Demon)) add = false;

                if (specifications.Contains("True Good") && character.alignment != EAlignment.Good) add = false;
                if (specifications.Contains("Register Good") && character.GetRegisterAlignment() != EAlignment.Good) add = false;
                if (specifications.Contains("Perfect Good") && (character.alignment != EAlignment.Good || character.GetRegisterAlignment() != EAlignment.Good)) add = false;

                if (specifications.Contains("True Evil") && character.alignment != EAlignment.Evil) add = false;
                if (specifications.Contains("Register Evil") && character.GetRegisterAlignment() != EAlignment.Evil) add = false;
                if (specifications.Contains("Perfect Evil") && (character.alignment != EAlignment.Evil || character.GetRegisterAlignment() != EAlignment.Evil)) add = false;

                if (specifications.Contains("True Truthful") && CharacterHelper.CheckLying(character)) add = false;
                if (specifications.Contains("Register Truthful") && CharacterHelper.CheckLyingAppearance(character)) add = false;
                if (specifications.Contains("Perfect Truthful") && (CharacterHelper.CheckLyingAppearance(character) || CharacterHelper.CheckLying(character))) add = false;

                if (specifications.Contains("True Lying") && !CharacterHelper.CheckLying(character)) add = false;
                if (specifications.Contains("Register Lying") && !CharacterHelper.CheckLyingAppearance(character)) add = false;
                if (specifications.Contains("Perfect Lying") && (!CharacterHelper.CheckLyingAppearance(character) || !CharacterHelper.CheckLying(character))) add = false;

                if (specifications.Contains("True Honest") && character.bluff) add = false;
                if (specifications.Contains("Register Honest") && CharacterHelper.CheckIfDisguisedAppearance(character)) add = false;
                if (specifications.Contains("Perfect Honest") && (CharacterHelper.CheckIfDisguisedAppearance(character) || character.bluff)) add = false;

                if (specifications.Contains("True Disguised") && !character.bluff) add = false;
                if (specifications.Contains("Register Disguised") && !CharacterHelper.CheckIfDisguisedAppearance(character)) add = false;
                if (specifications.Contains("Perfect Disguised") && (!CharacterHelper.CheckIfDisguisedAppearance(character) || !character.bluff)) add = false;

                if (specifications.Contains("Pure") && character.statuses.Contains(ECharacterStatus.Corrupted)) add = false;
                if (specifications.Contains("Corrupted") && !character.statuses.Contains(ECharacterStatus.Corrupted)) add = false;

                if (add)
                {
                    validTargets.Add(character);
                    //DebugMessage($"Added #{character.id}");
                }

            }

            validTargets.Remove(charRef);

            if (validTargets.Count == 0)
            {
                DebugMessage("Found no valid targets!");
                return new();
            }

            Il2CppSystem.Collections.Generic.List<Character> tripleVillage = new();
            for (int i = 0; i < 3; i++) foreach (Character character in Gameplay.CurrentCharacters) tripleVillage.Add(character);

            bool foundSelf = false;
            bool foundFirst = false;
            Character firstTarget = new();
            Character secondTarget = new();

            foreach (Character character in tripleVillage)
            {
                if (validTargets.Contains(character) && !foundSelf)
                {
                    foundFirst = true;
                    firstTarget = character;
                }
                if (character == charRef && foundFirst) foundSelf = true;
                if (validTargets.Contains(character) && foundSelf)
                {
                    secondTarget = character;
                    break;
                }
            }

            Il2CppSystem.Collections.Generic.List<Character> returnList = new();
            if (firstTarget != null) returnList.Add(firstTarget);
            if (secondTarget != null && firstTarget != secondTarget) returnList.Add(secondTarget);
            DebugMessage($"Found targets: {MentionEveryCharacterInList(returnList, "and")}");
            return returnList;
        }





        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(EAlignment alignment)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.startingAlignment == alignment)
                {
                    returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(ECharacterType type)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.type == type)
                {
                    returnList.Add(character);
                }
            }
            if (type == ECharacterType.Demon)
            {
                foreach (CharacterData character in GetAllDemons())
                {
                    returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles(EAlignment alignment, ECharacterType type)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
            {
                if (character.startingAlignment == alignment && character.type == type)
                {
                    returnList.Add(character);
                }
            }
            if (type == ECharacterType.Demon)
            {
                foreach (CharacterData character in GetAllDemons())
                {
                    if (character.startingAlignment == alignment) returnList.Add(character);
                }
            }
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetScriptRoles()
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            foreach (CharacterData character in GetScriptRoles(EAlignment.Good)) returnList.Add(character);
            foreach (CharacterData character in GetScriptRoles(EAlignment.Evil)) returnList.Add(character);
            return returnList;
        }

        public Il2CppSystem.Collections.Generic.List<CharacterData> GetAllDemons()
        {
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

            Il2CppSystem.Collections.Generic.List<CharacterData> returnList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            for (int j = 0; j < allDatas.Length; j++)
            {
                if (allDatas[j].type == ECharacterType.Demon) returnList.Add(allDatas[j]);
            }
            return returnList;
        }

        [HarmonyPatch(typeof(Gameplay), "OnCharacterReveal")]
        public static class w_AnyRevealPatch
        {
            public static ETriggerPhase AnyReveal = (ETriggerPhase)1420228423;
            [HarmonyPrefix]
            public static bool CharacterRevealPrefix(Character obj)
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    character.Act(AnyReveal);
                }
                return true;
            }
        }

        // The following two patches are by Skill Cycler. They make the Night Cycle always in-play.
        private static int getRulesCall;

        [HarmonyPatch(typeof(Gameplay), "UpdateRules")]
        public static class UpdateRulesPatch
        {
            public static void Prefix()
            {
                getRulesCall = 0;
            }
        }

        [HarmonyPatch(typeof(Role), "GetRules")]
        public static class GetRulesPatch
        {
            public static void Postfix(Role __instance, ref Il2CppSystem.Collections.Generic.List<SpecialRule> __result)
            {
                getRulesCall++;
                if (__result == null)
                    __result = new Il2CppSystem.Collections.Generic.List<SpecialRule>();

                if (getRulesCall < 3)
                {
                    var rules = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
                    rules.Add(new NightModeRule(4));
                    __result = rules;
                }
                else
                {
                    __result = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
                }


                // This next part was added by me.
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
            }
        }

        // Code by Skill Cycler
        [HarmonyPatch(typeof(Gameplay), nameof(Gameplay.UpdateCharacters))]
        public static class LogCharacters
        {
            public static void Postfix(Gameplay __instance)
            {
                wx_SavedScripts sharedScripts = new();
                sharedScripts.DebugMessage("Logging all characters");
                foreach (Character c in Gameplay.CurrentCharacters)
                {
                    sharedScripts.DebugMessage($"#{c.id} is the {c.dataRef.characterName}");
                }
            }
        }

    }
    

    /*
    [HarmonyPatch(typeof(Gameplay), "ChangeGameplayState")]
    public static class SummaryTriggerPhase
    {
        public static ETriggerPhase summary = (ETriggerPhase)421131311;
        public static void Postfix(Gameplay __instance, ref EGameplayState __state)
        {
            if (__state == EGameplayState.Summary)
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    character.Act(summary);
                }
            }
        }
    }
    */
}
