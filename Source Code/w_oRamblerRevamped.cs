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
public class w_oRamblerRevamped : w_zRoleBase // Just gonna add some extra quotes.
{
    public override string Description
    {
        get
        {
            return "#X, shut up!";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            ShutUpAdjacentTruthfulls(charRef);
        }
        if (trigger == ETriggerPhase.Day)
        {
            string info = "";
            info = GetRandomQuote(charRef);
            ActedInfo newInfo = new ActedInfo(info, Characters.Instance.GetAdjacentCharacters(charRef));

            OnActed(trigger, charRef, newInfo);
        }
    }

    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            ShutUpAdjacentLyings(charRef);
        }
        if (trigger == ETriggerPhase.Day)
        {
            string info = "";
            info = GetRandomQuote(charRef);
            ActedInfo newInfo = new ActedInfo(info, Characters.Instance.GetAdjacentCharacters(charRef));

            OnActed(trigger, charRef, newInfo);
        }
    }

    public string GetRandomQuote(Character charRef)
    {

        Character random = new();
        Il2CppSystem.Collections.Generic.List<Character> refCharacters = new();
        foreach (Character character in Gameplay.CurrentCharacters) refCharacters.Add(character);
        refCharacters.Remove(charRef);
        random = refCharacters[UnityEngine.Random.RandomRangeInt(0, refCharacters.Count)];

        Il2CppSystem.Collections.Generic.List<string> randomNonsense = new();
        // foreach (string nonsense in quotes) randomNonsense.Add(nonsense);


        randomNonsense.Add("Get it twisted! Your actions impact nothing"); // Vanilla
        randomNonsense.Add("When you duct tape your mouth, nothing can stop you"); // Vanilla
        randomNonsense.Add("I got the sofa without making any payments on it"); // Vanilla
        randomNonsense.Add("Statistically, you're like 12.5% grandma"); // Vanilla
        randomNonsense.Add("I usually spend an hour a day just making myself sneeze"); // Vanilla
        randomNonsense.Add("I just had a brain… genius moment"); // Vanilla
        randomNonsense.Add("I keep waking up my cat with my exaltations"); // Vanilla
        randomNonsense.Add("I got a new think"); // Vanilla
        randomNonsense.Add("One time, the Poet called me an egg"); // Wingidon, added officially
        randomNonsense.Add("As long as you don't die, you win"); // Vanilla
        randomNonsense.Add("I'm probably the most humble person I know"); // Vanilla
        randomNonsense.Add("I am an idiot sandwich"); // Vanilla
        randomNonsense.Add("Mostly what keeps my house clean is the fear of God"); // Vanilla
        randomNonsense.Add("Successful people are better people"); // Vanilla
        randomNonsense.Add("Its easier to order a sandwich than to develop personality"); // Vanilla
        randomNonsense.Add("Look, I've got being annoying down to a science"); // Vanilla
        randomNonsense.Add("To confuse your enemies, you must first confuse yourself"); // Vanilla
        randomNonsense.Add("To stay unpredictable, never know what you are doing"); // Vanilla
        randomNonsense.Add("Oh, how the turns have tabled"); // YziWithAnUzi, added officially
        randomNonsense.Add("Hey Village, Rambler here"); // Vanilla
        randomNonsense.Add("If you are lost, simply remember where you are"); // Vanilla
        randomNonsense.Add("If you fail, simply succeed next time"); // Vanilla
        randomNonsense.Add("If you close your eyes, things become harder to see"); // Vanilla
        randomNonsense.Add("Don't love your job, job your love"); // Vanilla
        randomNonsense.Add("Don’t feed the hand that bites"); // Vanilla
        randomNonsense.Add("The more questions you ask the more confused you’ll be"); // Asds751, added officially
        randomNonsense.Add("If you are mad, just calm down"); // Vanilla
        randomNonsense.Add("An apple is like… it’s made out of edible wood"); // Vanilla
        randomNonsense.Add("If it almost rhymes, it must almost be true"); // Vanilla
        randomNonsense.Add("You don’t ever learn information permanently, you rent it"); // Vanilla
        randomNonsense.Add("Roughly half of all outcomes are worse than the other half"); // Vanilla
        randomNonsense.Add("I have been alive for almost my whole life"); // Vanilla
        randomNonsense.Add("The longer something takes, the longer it feels"); // Vanilla



        // Extras taken from https://discord.com/channels/1148903384968089640/1495910186492362772
        randomNonsense.Add("Have you tried winning?"); // Asds751
        randomNonsense.Add("This statement is a Lie"); // Starfall the Shaman
        randomNonsense.Add("Boy, do I love hearing myself talk!"); // ExiliadoVerde89
        randomNonsense.Add("You better be listening, this is pretty important stuff!"); // FireDragonFlare
        randomNonsense.Add("I had to take a break from drowning to get a glass of water"); // Winder
        randomNonsense.Add("That never works, except when I do it because it works perfectly"); // Winder
        randomNonsense.Add("If you see twins, just kill one of them. Gets rid of a lot of confusion"); // Winder
        randomNonsense.Add("Total misplay"); // Chara
        randomNonsense.Add("Oh, how the tables have tabled!"); // Wingidon
        randomNonsense.Add("You can't trust anyone here! Except me. You can trust me"); // Asds751
        randomNonsense.Add("You go in the crystal for a minute, it's going to feel like one week!"); // Bepabiba
        randomNonsense.Add("People die when they are killed"); // Winder
        randomNonsense.Add("Because bread tastes better than key"); // YziWithAnUzi
        randomNonsense.Add("Just be correct, it's not hard"); // Meggo_The_Eggo
        randomNonsense.Add("I think I am actually the exact middle of millenials"); // FireDragonFlare
        randomNonsense.Add("They call me <i>The Rambler</i>"); // ButterStutter
        randomNonsense.Add("Your village security is really good...or is it?"); // Asds751
        randomNonsense.Add("Let's go rambling!\n\nAw, dangit!\nAw, dangit!"); // CocoFleur49, Asds751
        randomNonsense.Add("Spill the tea on Bob"); // FiTheDragonfly
        randomNonsense.Add("You know, I'm something of a Rambler myself"); // IwerSonsch
        randomNonsense.Add("My source is that I made it up"); // Bitterbug
        randomNonsense.Add("A good king is a dead king"); // Bitterbug
        randomNonsense.Add("Leave the art for the yearners"); // FiTheDragonfly
        randomNonsense.Add("Can you believe how much I paid for this?"); // Bepabiba
        randomNonsense.Add("With great power comes great need to take a nap"); // YziWithAnUzi
        randomNonsense.Add("I am the one who talks"); // IwerSonsch
        randomNonsense.Add("My yapping has no effect on you? Who decided that?"); // Asds751
        randomNonsense.Add("Did you know that 83% of statistics are made up?"); // Meggo_The_Eggo
        randomNonsense.Add("50% of people have an IQ below 100"); // Asds751
        randomNonsense.Add("You're not supposed to die during pinball"); // Hope.Chr
        randomNonsense.Add("Doors just seem to magically pop into existence"); // Hope.Chr
        randomNonsense.Add("Why do you bake cookies but cook bacon?"); // Multifan2
        randomNonsense.Add("Would you rather have unlimited bacon but no games, or unlimited games but no games?"); // Multifan2
        randomNonsense.Add("I am a man of my own words"); // Deition676
        randomNonsense.Add("Every new beginning comes from some other beginning's end"); // DankB019001
        randomNonsense.Add("The best way to win is to not lose"); // DankB019001
        randomNonsense.Add("Ugh, you made me read"); // DankB019001
        randomNonsense.Add("Get in the crystal, sorry buddy"); // PhimbulWinter
        randomNonsense.Add("I'm the only person that has heard everything I've said"); // DeanWithCoolBean
        randomNonsense.Add("I'm AT LEAST 1 card away from closest Evil"); // ZakMakLak
        randomNonsense.Add("The closest Evil is in this village"); // ZakMakLak
        randomNonsense.Add("Closest Evil is... my clock is broken"); // ZakMakLak
        randomNonsense.Add("I cured 0 Corruptions\n\nWhat? It's not my job"); // ZakMakLak
        randomNonsense.Add("Rambler is 1 card away from closest character"); // ZakMakLak
        randomNonsense.Add("If there were no Evil, there could be no Good"); // Bepabiba
        randomNonsense.Add("Death is a social construct"); // Meggo_The_Eggo
        randomNonsense.Add("The best kind of prize is a surprise!"); // Bitterbug
        randomNonsense.Add("It is better to be rich and healthy than poor and sick"); // Booblesnoot
        randomNonsense.Add("You are just mad because you are angry"); // Skadoodle
        randomNonsense.Add("Why are the Evils here anyways? They don't have hobbies, I suppose"); // T1cke
        randomNonsense.Add("Perchance!"); // Meggo_The_Eggo
        randomNonsense.Add("To get to the beginning, you first have to start"); // CakeKop
        randomNonsense.Add("One day it's Sunday, and the next day it's Monday"); // CakeKop
        randomNonsense.Add("Did you know you slept today?"); // T1cke
        randomNonsense.Add("What if Mike is short for Micycle?"); // FunkyTheCreature
        randomNonsense.Add("What if Bike is short for Bichael?"); // FunkyTheCreature
        randomNonsense.Add("Big red buttons were meant to be pressed"); // Zhormungandr
        randomNonsense.Add("I know, that you know, that I know, that you know, that I know, that you know... huh?"); // Zhormungandr
        randomNonsense.Add("Something really interesting"); // Zhormungandr
        randomNonsense.Add("Lovely weather we're having!"); // Zhormungandr
        randomNonsense.Add("Remember to take a break every once in a while!"); // Zhormungandr
        randomNonsense.Add("Did you know that- Nevermind, I forgot"); // Bitterbug
        randomNonsense.Add("So, funny thing, I... Hang on... Nope, the thought's gone"); // Wingidon
        randomNonsense.Add("Hey, how do you get from village to village so fast?"); // Screps_Doodle
        randomNonsense.Add("Given that 1=2, I can prove that I am the Pope"); // SomeoneUnknown
        randomNonsense.Add("Too many soups spoil the cook"); // ImagineExisting
        randomNonsense.Add("I have no idea what I'm talking about half the time"); // FireDragonFlare
        randomNonsense.Add("When life gives you lemons, eat them!"); // ImagineExisting
        randomNonsense.Add("When life gives you lemons, make apple juice! Never let them know your next move!"); //ZakMakLak
        randomNonsense.Add("You know running out of health makes you lose, right?"); // Phimbulwinter
        randomNonsense.Add("Life is like a box of chocolates: Sometimes, you get punched in the face"); // TheRealLordDragon
        randomNonsense.Add("When life gives you lemons, return to sender!"); // 11ProGamer28, Wingidon
        randomNonsense.Add("When life gives you lemons, don't just accept that! Make life take its lemons back! I don't want its lemons!"); // Wingidon
        randomNonsense.Add("Insert random reference here"); // 11ProGamer28
        randomNonsense.Add("I think the moon is the real Evil here"); // FireDragonFlare
        randomNonsense.Add("Maybe the real Evils were the friends we made along the way"); // Meggo_The_Eggo
        randomNonsense.Add("Last Halloween, I dressed up as an egg and the Poet got mad at me"); // Deition676
        randomNonsense.Add("50% of apples end in oranges"); // PhimbulWinter
        randomNonsense.Add("If you cracked open a cold one in one yard and made a milkshake in another, which yard would the boys go to?"); // Wingidon
        randomNonsense.Add("Of course, all of this nonsense hinges on whether or not the boys are back in town"); // Wingidon
        randomNonsense.Add("What if \'soy milk\' is just milk introducing itself in Spanish?"); // Wingidon
        randomNonsense.Add("So I am an Outcast, but Evil is Incast"); // Git3727
        randomNonsense.Add("A monad is just a monoid in the category of endofunctors"); // Xicalyte (@paradoxically_attain)
        randomNonsense.Add("I'm not talking about bananas right now"); // Xicalyte (@paradoxically_attain)
        randomNonsense.Add("Would you rather become infertile or invade Japan?"); // Xicalyte (@paradoxically_attain)
        randomNonsense.Add($"If you were wanting to roll a dice right now, the number is {UnityEngine.Random.RandomRangeInt(1, 7)}"); // Xicalyte (@paradoxically_attain)
        randomNonsense.Add($"Just rolled a d20, it landed on {UnityEngine.Random.RandomRangeInt(1, 21)}"); // Wingidon, based on the above statement
        randomNonsense.Add($"Just flip a coin; that easy"); // Bepabiba
        string headsOrTails = "Heads or tails?\n\n";
        if (new wx_SavedScripts().PercentChance(50)) headsOrTails += "Tails!"; else headsOrTails += "Heads!";
        randomNonsense.Add(headsOrTails); // Wingidon, based on the above statement.
        Il2CppSystem.Collections.Generic.List<string> yesOrNo = new(); yesOrNo.Add("yes"); yesOrNo.Add("yes"); yesOrNo.Add("no"); yesOrNo.Add("no"); yesOrNo.Add("maybe");
        randomNonsense.Add($"When in doubt, the answer is {yesOrNo[UnityEngine.Random.RandomRangeInt(0, yesOrNo.Count)]}"); // FireDragonFlare, Booblesnoot
        randomNonsense.Add("When in doubt, just do the right thing and you can't go wrong"); // Phimbulwinter
        randomNonsense.Add("I haven't rambled my last ramble yet"); // DeterminedDaaf
        randomNonsense.Add("One time I tried to buy the Slayer's hair"); // DeterminedDaaf
        randomNonsense.Add("I talked to a tree for two hours today"); // DeterminedDaaf
        randomNonsense.Add("Now would be a good time to invest in wheelbarrow wheels"); // DeterminedDaaf
        randomNonsense.Add("Listen closely, I know where the treasure is buried, all you have to do is..."); // DeterminedDaaf
        randomNonsense.Add("I eat at least a handful of clay each day to stay grounded"); // DeterminedDaaf
        randomNonsense.Add("Are you feeling lucky? If not I may have some lucky sticks for sale at a bargain price"); // DeterminedDaaf
        randomNonsense.Add("Once time the Bombardier took my glasses"); // DeterminedDaaf


        return randomNonsense[UnityEngine.Random.RandomRangeInt(0, randomNonsense.Count)];
    }





    // No idea how half of this, the only reason it's all here is because the game was throwing a hissy fit when it wasn't.


    private void ShutUpAdjacentTruthfulls(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacents = Characters.Instance.GetAdjacentCharacters(charRef);

        foreach (Character c in adjacents)
            InterefereCharacter(charRef, c, false);

        //foreach (Character c in adjacents)
        //if (!CharacterHelper.CheckLyingAppearance(c))
        //InterefereCharacter(charRef, c);
    }
    private void ShutUpAdjacentLyings(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacents = Characters.Instance.GetAdjacentCharacters(charRef);

        foreach (Character c in adjacents)
            InterefereCharacter(charRef, c, true);

        //foreach (Character c in adjacents)
        //if (CharacterHelper.CheckLyingAppearance(c))
        //InterefereCharacter(charRef, c);
    }
    private void InterefereCharacter(Character charRef, Character ch, bool shouldLie)
    {
        if (ch.state == ECharacterState.Hidden)
        {
            Il2CppSystem.Action<ActedInfo, ETriggerPhase> handler = null;
            System.Action<ActedInfo, ETriggerPhase> action = (info, trigger) =>
            {
                InterfereCharacterOnReveal(shouldLie, info, trigger, charRef, ch);
            };
            handler = action;
            ch.onAboutToAct += handler;
            return;
        }

        if (CharacterHelper.CheckLyingAppearance(ch) != shouldLie) return;
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        selection.Add(charRef);
        ActedInfo info = new ActedInfo(GetShutUpString(ch, charRef), selection);

        ch.InterfereActed(info, 0.02f, isDelay: false, howLong: 3f);
    }

    int amountShutups = 0;
    public void InterfereCharacterOnReveal(bool shouldLie, ActedInfo info, ETriggerPhase trigger, Character charRef, Character targetCh)
    {
        if (CharacterHelper.CheckLyingAppearance(targetCh) == shouldLie)
            if (targetCh.state != ECharacterState.Hidden)
            {
                {
                    amountShutups++;
                    info.desc = GetShutUpString(targetCh, charRef);
                }

                Debug.Log($"NANI: " + amountShutups);
                CheckAchievementsAndUnlockIfAble();

                info.characters = new Il2CppSystem.Collections.Generic.List<Character>();
                info.characters.Add(charRef);
            }
    }

    //ACHIEVEMENTS
    private void CheckAchievementsAndUnlockIfAble()
    {
        if (amountShutups >= 2)
            ProjectContext.UnlockAchievement("Rambler_1_ACHIV_3167");
    }



    string GetShutUpString(Character charRef, Character target)
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new();

        returnList.Add($"#{target.id}, shut up!");
        returnList.Add($"Shut up, #{target.id}!");

        wx_SavedScripts sharedScripts = new();
        if (charRef.GetCharacterBluffIfAble().characterName == "Alchemist") returnList.Add($"I couldn't cure #{target.id}'s ramblings");
        if (charRef.GetCharacterBluffIfAble().characterName == "Architect") returnList.Add($"#{target.id}'s side is more annoying");
        if (charRef.GetCharacterBluffIfAble().characterName == "Baker") returnList.Add($"#{target.id}, shut up, the food will be ready soon");
        if (charRef.GetCharacterBluffIfAble().characterName == "Bard") returnList.Add($"I am 1 card away from my closest drama, from #{target.id}");
        if (charRef.GetCharacterBluffIfAble().characterName == "Bishop") returnList.Add($"#{target.id} is almost as annoying as the Dreamer");
        if (charRef.GetCharacterBluffIfAble().characterName == "Confessor") returnList.Add($"#{target.id}, you're making me dizzy");
        if (charRef.GetCharacterBluffIfAble().characterName == "Dreamer") returnList.Add($"#{target.id} could be: Rambler");
        if (charRef.GetCharacterBluffIfAble().characterName == "Druid") returnList.Add($"At #{target.id}, there is: Rambler");
        if (charRef.GetCharacterBluffIfAble().characterName == "Empress") returnList.Add($"Guards, remove #{target.id} from my sight at once!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Enlightened") returnList.Add($"Even the cosmos is irritated by #{target.id}");
        if (charRef.GetCharacterBluffIfAble().characterName == "Fortune Teller") returnList.Add($"Your future contains a lot of annoying words from #{target.id}");
        if (charRef.GetCharacterBluffIfAble().characterName == "Gemcrafter") returnList.Add($"I could confuse #{target.id}'s forehead for a pearl!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Hunter") returnList.Add($"Closest Rambler is 1 card away, at #{target.id}");
        if (charRef.GetCharacterBluffIfAble().characterName == "Investigator") returnList.Add($"#{target.id} is about as suspicious as Ramblers get");
        if (charRef.GetCharacterBluffIfAble().characterName == "Jester") returnList.Add($"#{target.id} wishes they were me!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Judge") returnList.Add($"Order in the court, #{target.id}!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Knight") returnList.Add($"The one thing my armour doesn't guard me against is #{target.id}'s ramblings");
        if (charRef.GetCharacterBluffIfAble().characterName == "Knitter") returnList.Add($"Yes, #{target.id}, that's nice, can you let me get back to my knitting?");
        if (charRef.GetCharacterBluffIfAble().characterName == "Lover") returnList.Add($"I'd dump #{target.id} in a heartbeat!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Medium") returnList.Add($"#{target.id} would fit right in with the ghosts!");
        if (charRef.GetCharacterBluffIfAble().characterName == "Oracle") returnList.Add($"I <i>had</i> a vision, but #{target.id} talked over it");
        if (charRef.GetCharacterBluffIfAble().characterName == "Poet")
        {
            returnList.Add($"#{target.id}, you egg!");
            returnList.Add($"What, #{target.id}, you egg! Shut up!");
        }
        if (charRef.GetCharacterBluffIfAble().characterName == "Scout") returnList.Add($"Scout is 1 card away from closest Rambler, that being the one at #{target.id}");
        if (charRef.GetCharacterBluffIfAble().characterName == "Slayer") returnList.Add($"Can I kill #{target.id}? Is that something that's allowed?");
        if (charRef.GetCharacterBluffIfAble().characterName == "Witness") returnList.Add($"If Evil did something to #{target.id}, I might be willing to turn a blind eye");


        if (charRef.GetCharacterBluffIfAble().characterName == "Bombardier") returnList.Add($"I'm going to burn #{target.id}'s house down");
        if (charRef.GetCharacterBluffIfAble().characterName == "Plague Doctor") returnList.Add($"#{target.id} appears to have a disease that causes excessive vocal droning...");
        if (charRef.GetCharacterBluffIfAble().characterName == "Rambler")
        {
            returnList.Add($"#{target.id} is just like me!");
            returnList.Add($"Looking at #{target.id} feels like looking in a mirror!");
            returnList.Add($"Wait, #{target.id} is me, bro!");
            returnList.Add($"Okay, but #{target.id} is spitting");
            returnList.Add($"#{target.id}, I remember that one");
            returnList.Add($"Get it twisted! #{target.id}'s actions impact nothing!");
            returnList.Add($"I've gotta learn about #{target.id} again");
            returnList.Add($"#{target.id}, what went wrong?!");
        }
        if (charRef.GetCharacterBluffIfAble().characterName == "Wretch") returnList.Add($"I've seen cicadas less annoying than #{target.id}...");

        return returnList[UnityEngine.Random.RandomRangeInt(0, returnList.Count)];
    }
    public w_oRamblerRevamped() : base(ClassInjector.DerivedConstructorPointer<w_oRamblerRevamped>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_oRamblerRevamped(System.IntPtr ptr) : base(ptr)
    {
    }
}