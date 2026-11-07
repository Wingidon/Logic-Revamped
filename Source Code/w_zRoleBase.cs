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
public class w_zRoleBase : Role
{

    public bool CheckConfigOption_Bool(string ID)
    {
        bool configOption = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<bool>(ID).Value;
        return configOption;
    }
    public int CheckConfigOption_Int(string ID)
    {
        int configOption = MelonPreferences.GetCategory("LogicRevampedSettings").GetEntry<int>(ID).Value;
        return configOption;
    }

    public bool IsDead(Character charRef)
    {
        return !Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(charRef);
    }

    public Il2CppSystem.Collections.Generic.List<string> GetBaseEvilRemarks()
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new();
        returnList.Add("No fair!");
        returnList.Add("Ow, rude!");
        returnList.Add("You got me!");
        returnList.Add("Oof, that hurt!");
        returnList.Add("That hurt!");
        returnList.Add("Hey!");
        returnList.Add("You got lucky!");
        returnList.Add("Cheap shot!");
        returnList.Add("Got me!");
        returnList.Add("I'm dead!");
        returnList.Add("They got me!");
        returnList.Add("I'm hit!");
        returnList.Add("I'm down!");
        returnList.Add("I'll get you next time!");
        returnList.Add("I'll be back!");
        returnList.Add("This is NOT the last you'll see of me!");
        returnList.Add("I'll come back stronger than ever!");
        returnList.Add("Another time, perhaps...");
        returnList.Add("Hm, you're better than I thought you'd be");
        returnList.Add("This might actually be a challenge!");
        returnList.Add("I need to stop going easy on you!");
        returnList.Add("I beg your pardon!");
        returnList.Add("Jeez, you could just exile me, that's a bit far!");
        returnList.Add("You didn't have to go THAT far!");
        returnList.Add("Gah!");
        returnList.Add("Owwww...");
        returnList.Add("Agh, you...");
        returnList.Add("Ough...");
        returnList.Add("Ouch!");
        returnList.Add("Oof!");
        returnList.Add("Agh, that stings!");
        returnList.Add("Ow, fine!");
        returnList.Add("Ow, damnit!");
        returnList.Add("Ow, alright, I get it!");
        returnList.Add("Ow, alright, stop!");
        return returnList;
    }
    public w_zRoleBase() : base(ClassInjector.DerivedConstructorPointer<w_zRoleBase>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_zRoleBase(System.IntPtr ptr) : base(ptr)
    {
    }
}