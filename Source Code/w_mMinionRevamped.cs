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
public class w_mMinionRevamped : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Lies and Disguises.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        int truthChance = CheckConfigOption_Int("MinionTruthChance");
        if (truthChance != 0)
        {
            if (sharedScripts.PercentChance(truthChance))
            {
                charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
            }
        }
        if (CheckConfigOption_Bool("MinionUniquity"))
        {
            return sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        CharacterData bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, true);
        if (sharedScripts.PercentChance(60))
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_mMinionRevamped() : base(ClassInjector.DerivedConstructorPointer<w_mMinionRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mMinionRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}