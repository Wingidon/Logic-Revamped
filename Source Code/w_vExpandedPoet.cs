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
public class w_vExpandedPoet : Gossip
{
    public Il2CppSystem.Collections.Generic.List<Role> GetExpandedInfoRoles()
    {
        Il2CppSystem.Collections.Generic.List<Role> returnList = infoRoles;
        for (int i = 0; i < 10; i++)
        {
            returnList.Add(new w_vDreamerLearn());
            returnList.Add(new w_vDruidLearn());
            returnList.Add(new w_vFortuneTellerLearn());
            returnList.Add(new w_vInvestigatorLearn());
            returnList.Add(new w_vJesterLearn());
            returnList.Add(new w_vJudgeLearn());
        }
        return returnList;
    }

    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init) infoRoles = GetExpandedInfoRoles();
        if (trigger == ETriggerPhase.Day)
        {
            Role chosenRole = GetExpandedInfoRoles()[UnityEngine.Random.RandomRangeInt(0, GetExpandedInfoRoles().Count)];
            chosenRole.Act(trigger, charRef);
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init) infoRoles = GetExpandedInfoRoles();
        if (trigger == ETriggerPhase.Day)
        {
            Role chosenRole = GetExpandedInfoRoles()[UnityEngine.Random.RandomRangeInt(0, GetExpandedInfoRoles().Count)];
            chosenRole.BluffAct(trigger, charRef);
        }
    }
    public w_vExpandedPoet() : base(ClassInjector.DerivedConstructorPointer<w_vExpandedPoet>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vExpandedPoet(System.IntPtr ptr) : base(ptr)
    {
    }
}