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
public class w_mTwinMinion_Revamped : w_zRoleBase
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

        int truthChance = CheckConfigOption_Int("TwinionTruthChance");
        if (truthChance != 0)
        {
            if (sharedScripts.PercentChance(truthChance))
            {
                charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
            }
        }
		if (CheckConfigOption_Bool("TwinionDuplicity"))
		{
			return sharedScripts.GetOverrideDuplicateBluff(charRef, true);
		}
		CharacterData bluff = sharedScripts.GetOverrideDuplicateBluff(charRef, true);
        if (sharedScripts.PercentChance(60))
        {
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_mTwinMinion_Revamped() : base(ClassInjector.DerivedConstructorPointer<w_mTwinMinion_Revamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_mTwinMinion_Revamped(System.IntPtr ptr) : base(ptr)
    {
    }
}