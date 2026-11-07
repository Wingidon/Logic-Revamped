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
public class w_mChancellorRevamped : Baron // Mostly fine as is, but I want to adjust its Disguise logic.
{
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, true);
        if (sharedScripts.PercentChance(40))
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_mChancellorRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mChancellorRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mChancellorRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}