using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.Runtime.VersionSpecific.EventInfo;
using Il2CppSystem;
using Il2CppTMPro;
using LogicRevampedMod;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using System.Data;
using UnityEngine;

namespace LogicRevampedMod;

[RegisterTypeInIl2Cpp]
public class w_vArchitectRevamped : Architect // For Poet purposes.
{
    public w_vArchitectRevamped() : base(ClassInjector.DerivedConstructorPointer<w_vArchitectRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_vArchitectRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}