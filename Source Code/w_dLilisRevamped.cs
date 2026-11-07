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
public class w_dLilisRevamped : w_zRoleBase
{
    public override string Description
    {
        get
        {
            return "Lies and Disguises. Kills at night.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Night) return;
        if (IsDead(charRef)) return;
        Il2CppSystem.Collections.Generic.List<Character> targets = new();
        Il2CppSystem.Collections.Generic.List<Character> revealedCharacters = Characters.Instance.FilterRevealedCharacters(Gameplay.CurrentCharacters);
        Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = new();
        foreach (Character character in Gameplay.CurrentCharacters) if (!revealedCharacters.Contains(character)) unrevealedCharacters.Add(character);
        Health health = PlayerController.PlayerInfo.health;

        int damage = CheckConfigOption_Int("LilisDamage");
        int revealedDamage = CheckConfigOption_Int("LilisDamage") + CheckConfigOption_Int("LilisRevealedDamageBonus");
        bool evilAttack = CheckConfigOption_Bool("LilisEvilAttack");
        bool selfAttack = CheckConfigOption_Bool("LilisSelfAttack");
        bool killRevealed = CheckConfigOption_Bool("LilisKillRevealed");

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (unrevealedCharacters.Contains(character) || killRevealed)
            {
                if (character.GetRegisterAlignment() == EAlignment.Good && !IsDead(character))
                {
                    targets.Add(character);
                }
            }
        }

        if (!selfAttack) targets.Remove(charRef);

        if (targets.Count == 0)
        {
            if (evilAttack)
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (unrevealedCharacters.Contains(character) || killRevealed)
                    {
                        if (!IsDead(character))
                        {
                            targets.Add(character);
                        } // These brackets are atrocious
                    } // I don't care though lmao
                }
            }
        }
        if (!selfAttack) targets.Remove(charRef);


        int totalDamage = damage;
        if (targets.Count != 0)
        {
            Character killTarget = targets[UnityEngine.Random.RandomRangeInt(0, targets.Count)];
            killTarget.KillByDemon(charRef);
            if (revealedCharacters.Contains(killTarget))
            {
                totalDamage = revealedDamage;
                killTarget.revealed = true;
                killTarget.RevealAllReal();
                killTarget.RevealStatusesIfAble();
            }
        }

        health.Damage(totalDamage);

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, CheckConfigOption_Bool("DemonOutcastDisguise"));
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public w_dLilisRevamped() : base(ClassInjector.DerivedConstructorPointer<w_dLilisRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_dLilisRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}