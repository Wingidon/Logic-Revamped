# Logic Revamped
A mod for Demon Bluff that makes some hefty changes to the puzzle-solving aspect of the game.

## Modding Notes
- It is highly recommended to use my Keyword Expansion mod (https://github.com/Wingidon/Keyword-Expansion) alongside this mod, but it's not required.
- The only known incompatibility this mod has is with Skill Cycler's Riddles mod (https://github.com/skillcycler/Riddles). If these two mods are installed at the same time, the Hypnotist and Captivator from Riddles may not work correctly when Disguised as vanilla roles.

## Credits
- Uzabi, Normandia and the rest of Demon Bluff's development team, for making this awesome game.
- Skill Cycler, for providing the permanent night cycle code and helping me with some other minor things in the project.
- Rsvdark, for playtesting the mod before release and finding a few issues that I didn't catch myself.

## Changes
The following roles have had the following changes made to them:
- Alchemist: Fixed a bug where multiple Alchemists wouldn't pick up on the same Corruption.
- Alchemist: Fixed a bug where Puppet Alchemist would Lie.
- Alchemist: Fixed a bug where Lying Alchemists would tell the truth if there were Corruptions nearby that had been cured.
- Alchemist: Reworded ability.
- Architect: Adjusted ability wording.
- Architect: Adjusted hint wording.
- Baker: Adjusted ability wording.
- Baker: Adjusted 'If Lies' wording.
- Baker: Added Hint.
- Bard: Removed Lying rule. Now just says anything that isn't the truth.
- Bard: Now chooses her Lies more convincingly based on the number of Corruptions present.
- Bard: While Lying, no longer has a hard-limit of 4. Can now mention any number that would be possible with the number of present Corruptions.
- Bishop: Can now name the Demon when there are Minions present.
- Bishop: Removed Lying rule. Now just says anything that isn't the truth.
- Bishop: When Lying, will now correctly not name an Outcast if nobody is Registering as an Outcast.
- Confessor: Adjusted ability wording.
- Confessor: Added config option to function more like Dupery's Priest.
- Dreamer: Adjusted ability wording.
- Druid: Adjusted ability wording.
- Druid: Adjusted Hint wording.
- Druid: Allowed Lying Druid to mention an incorrect Outcast if a real Outcast is within her selection.
- Empress: Removed Lying rule. Now just says anything that isn't the truth.
- Empress: Added failsafe for if there are no characters of a particular alignment.
- Enlightened: Fixed a bug where Enlightened silenced by Rambler would still yield hint arrows.
- Enlightened: Now highlights characters on the referenced side of them instead of yielding directional arrows.
- Fortune Teller: Adjusted ability wording.
- Gemcrafter: If there are no valid targets, will now say 'There are NO other Good characters' instead of pointing at herself.
- Hunter: Adjusted ability wording.
- Hunter: Removed Lying rule. Now just says anything that isn't the truth.
- Hunter: Now chooses his Lies more convincingly based on the number of Evils present.
- Investigator: Adjusted Lying logic to avoid situations where picking an Outcast claim would yield 'Villager' as a result.
- Investigator: Fixed a bug where characters in info wouldn't be sorted into ascending order.
- Knight: Removed Corruption penalty.
- Knight: Fixed a bug where Evil Healthybluffing Knight would be immortal still. Now dies correctly.
- Knitter: Adjusted ability wording.
- Knitter: Now chooses her Lies more convincingly based on the number of Evils present.
- Medium: Adjusted ability wording.
- Medium: Changed Lying rule. Now points at a character whose claim doesn't match their registration, and attempts to back that up.
- Medium: If there are no valid targets, will now say 'Something does not make sense' instead of pointing at herself.
- Oracle: Adjusted ability wording.
- Oracle: While truthful, the other character in her info does not need to be Good.
- Oracle: Removed Lying rule. Now just says anything that isn't the truth.
- Poet: Added setting to also gather info from Pick roles.
- Scout: Will now sometimes name the Wretch's registered role as his specific Evil.
- Scout: Added failsafe for if there are no Evil characters.
- Scout: Now chooses his Lies more convincingly based on the number of Evils present.
- Scout: Futureproofed and specified the way he acts if there are multiple of the same Evil role in-play.
- Slayer: Adjusted ability wording.
- Slayer: Fixed a bug where a Lying Slayer could try to use his ability on a dead character.
- Witness: No longer sees Lycanthrope transforming as affected by Evil.
- Bombardier: Fixed a bug where an Evil Bombardier would still kill you for Executing them.
- Doppelganger: Added customisation option to Disguise as an Outcast.
- Doppelganger: Adjusted ability wording.
- Drunk: Adjusted Disguise logic. Will double-up on claims with other not-in-play Disguisers like the Demon less frequently.
- Drunk: Added customisation option to Disguise as an Outcast.
- Lycanthrope: Added customisation option to sometimes Disguise as in-play roles.
- Lycanthrope: Added customisation option to Disguise as an Outcast.
- Lycanthrope: Adjusted Disguise logic. Will double-up on claims with other not-in-play Disguisers like the Demon less frequently.
- Lycanthrope: Adjusted ability wording.
- Lycanthrope: Now adds the Werewolf to the Deck.
- Plague Doctor: When Lying, will no longer point to itself or the same character twice.
- Wretch: Adjusted ability wording.
- Wretch: Rewrote Hint.
- All Evil characters: Adjusted Disguise logic.
- Chancellor: Removed incorrect hint.
- Minion: Added config option to only Disguise as not-in-play characters.
- Minion: Added config option to sometimes tell the Truth.
- Puppeteer: Adjusted ability wording.
- Puppeteer: Now adds the Puppet to the Deck.
- Puppet: Adjusted ability wording.
- Puppet: Redid Hint.
- Puppet: Can now use the Alchemist ability.
- Shaman: Adjusted ability wording.
- Twin Minion: Adjusted ability wording.
- Twin Minion: Added config option to only Disguise as in-play characters.
- Twin Minion: Added config option to sometimes tell the Truth.
- Werewolf: Adjusted ability wording.
- Werewolf: Adjusted hint wording.
- Witch: Adjusted ability wording.
- Witch: Migrated hint and ability into one.
- All Demons: Added config options to customise their villages.
- All Demons: Added config option to Disguise as Outcasts sometimes.
- Baa: Added config option to control how many Outcasts are hidden from the Deck, if any.
- Baa: Added config option to control how many Minions are hidden from the Deck, if any.
- Baa: Added config options to add fake Outcasts to the Deck.
- Baa: Added config options to add fake Minions to the Deck.
- Lilis: Added config option to adjust damage dealt.
- Lilis: Added config option to attack Revealed characters.
- Lilis: Added config option to adjust damage dealt if the previous option comes into play.
- Lilis: Added config option for whether or not to attack Evil if they're her only valid targets.
- Lilis: Added config option for whether or not to attack herself if she is her own only valid target.
- Pooka: Added config option to extend her range to the closest Villager on each side of her, instead of just adjacent Villagers.


Other changes include:
- Adjusted info wording on many different roles.
- Probably some other miscellaneous bug fixes as well.
- Probably some other changes I forgot to list out as well.
- Night Cycle is now always active thanks to code by Skill Cycler.


## Other
Config clarification: Each Demon in this mod has a respective 'exotic village compositions' toggle. A village is considered "exotic" if any of the following apply:
- The Outcast count is equal to or greater than the Villager count.
- The Evil count is equal to or greater than the Villager count.
- Villagers make up less than 40% of the village.
- Outcasts make up more than 33% of the village.
- Minions make up more than 40% of the village.
- The Outcast count is 2 or more higher than the Minion count.
- There is only 1 Evil in a village with 8 or more characters.
- The difference between the Outcast and Minion count is higher than 2.
