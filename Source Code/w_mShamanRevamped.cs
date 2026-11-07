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
public class w_mShamanRevamped : Illuzionist
{
    public override string Description
    {
        get
        {
            return "Clones a Villager.";
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
    public w_mShamanRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mShamanRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mShamanRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}