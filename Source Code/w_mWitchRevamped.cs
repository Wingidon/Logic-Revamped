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
public class w_mWitchRevamped : Cipher
{
    public override string Description
    {
        get
        {
            return "Hides the last card.";
        }
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, true);
        if (sharedScripts.PercentChance(60))
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_mWitchRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mWitchRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mWitchRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}