using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vEnlightenedRevamp : w_zRoleBase
{


    public Il2CppSystem.Collections.Generic.List<Character> GetDirectionalSelection(Character charRef, int direction)
    {
        Il2CppSystem.Collections.Generic.List<Character> clockwiseSelection = new();
        Il2CppSystem.Collections.Generic.List<Character> counterClockwiseSelection = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef)
            {
                int cwCheckDist = 100000;
                int ccwCheckDist = 100000;
                Character char1 = charRef;
                Character char2 = character;
                int totalCharCount = Gameplay.CurrentCharacters.Count;
                int tempDist = char1.id - char2.id; // #5 to #3: 2 steps CCW
                if (tempDist < 0)
                {
                    tempDist = tempDist + totalCharCount;
                }
                int tempDist2 = char2.id - char1.id; // #3 to #5: 2 steps CW
                if (tempDist2 < 0)
                {
                    tempDist2 = tempDist2 + totalCharCount;
                }
                cwCheckDist = tempDist2;
                ccwCheckDist = tempDist;

                if (cwCheckDist < ccwCheckDist) clockwiseSelection.Add(character);
                if (cwCheckDist > ccwCheckDist) counterClockwiseSelection.Add(character);
            }
        }

        if (direction == 0) return clockwiseSelection;
        if (direction == 1) return counterClockwiseSelection;
        return new();

    }
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> charsTwice = new();

        int cwDist = 10000;
        int ccwDist = 10000;

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil && character != charRef)
            {
                int cwCheckDist = 100000;
                int ccwCheckDist = 100000;
                Character char1 = charRef;
                Character char2 = character;
                int totalCharCount = Gameplay.CurrentCharacters.Count;
                int tempDist = char1.id - char2.id; // #5 to #3: 2 steps CCW
                if (tempDist < 0)
                {
                    tempDist = tempDist + totalCharCount;
                }
                int tempDist2 = char2.id - char1.id; // #3 to #5: 2 steps CW
                if (tempDist2 < 0)
                {
                    tempDist2 = tempDist2 + totalCharCount;
                }
                cwCheckDist = tempDist2;
                ccwCheckDist = tempDist;

                if (cwDist > cwCheckDist) cwDist = cwCheckDist;
                if (ccwDist > ccwCheckDist) ccwDist = ccwCheckDist;
            }
        }

        int infoID = 4;
        if (cwDist == 10000 && ccwDist == 10000) infoID = 3;
        else if (cwDist == ccwDist) infoID = 2;
        else if (cwDist > ccwDist) infoID = 1;
        else infoID = 0;

        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        if (infoID == 1) selection = GetDirectionalSelection(charRef, 1);
        if (infoID == 0) selection = GetDirectionalSelection(charRef, 0);
        return new ActedInfo(ConjureInfo(infoID), selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        ActedInfo trueInfo = GetInfo(charRef);
        string correctInfo = trueInfo.desc;
        Il2CppSystem.Collections.Generic.List<string> results = new();
        if (correctInfo != "Closest Evil is:\nClockwise")
        {
            results.Add("Closest Evil is:\nClockwise");
            results.Add("Closest Evil is:\nClockwise");
            results.Add("Closest Evil is:\nClockwise");
        }
        if (correctInfo != "Closest Evil is:\nCounter-Clockwise")
        {
            results.Add("Closest Evil is:\nCounter-Clockwise");
            results.Add("Closest Evil is:\nCounter-Clockwise");
            results.Add("Closest Evil is:\nCounter-Clockwise");
        }
        if (correctInfo != "Closest Evil is:\nEquidistant")
        {
            results.Add("Closest Evil is:\nEquidistant");
        }
        string falseResult = results[UnityEngine.Random.RandomRangeInt(0, results.Count)];
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        if (falseResult == "Closest Evil is:\nClockwise") selection = GetDirectionalSelection(charRef, 0);
        if (falseResult == "Closest Evil is:\nCounter-Clockwise") selection = GetDirectionalSelection(charRef, 1);
        return new ActedInfo(falseResult, selection);
    }
    public override string Description
    {
        get
        {
            return "Enlightened";
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
    private string ConjureInfo(int resultID)
    {
        string info = "Closest Evil is:\n";
        if (resultID == 0) info += "Clockwise";
        else if (resultID == 1) info += "Counter-Clockwise";
        else if (resultID == 2) info += "Equidistant";
        else info = "There are NO Evil characters";
        return info;
    }
    public w_vEnlightenedRevamp() : base(ClassInjector.DerivedConstructorPointer<w_vEnlightenedRevamp>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vEnlightenedRevamp(System.IntPtr ptr) : base(ptr)
    {
    }
}