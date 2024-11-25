using System.Collections.Generic;
using UnityEngine;

public class BubblePhraseGenerator
{
    private static List<string> wrongOrderReactions =
        new()
        {
            "Hey, this isn't what I had in mind!",
            "Uh-oh, I think there's been a mix-up!",
            "Oops, this isn't what I asked for!",
            "Hmm, this isn't quite what I ordered!",
            "Whoops! I think this is the wrong one!",
            "Wait, this doesn't look like what I wanted!",
            "Yikes, this isn't what I had in my head!",
            "Uh, is this for me? It's not what I ordered!",
        };

    private static List<string> emptyBowlReactions =
        new()
        {
            "Excuse me?! Where's the food? Am I on a diet I didn't know about?!",
            "This is… air soup?! Not what I asked for!",
            "An empty bowl?! Did a ghost eat my order?!",
            "Is this some kind of joke? Where's the rest of it?!",
            "I ordered a meal, not an invisible snack!",
            "Um, hello? Did my food run away?!",
            "Are you kidding me? Did the chef forget to cook?!",
            "What is this?! A bowl of disappointment?!",
        };

    public static string GenerateWrongOrderReaction()
    {
        return wrongOrderReactions[Random.Range(0, wrongOrderReactions.Count)];
    }

    public static string GenerateEmptyBowlReaction()
    {
        return emptyBowlReactions[Random.Range(0, emptyBowlReactions.Count)];
    }
}
