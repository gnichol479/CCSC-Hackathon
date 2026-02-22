using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightJson;
using Unisave;
using Unisave.Facets;
using Unisave.Facades;

public class NpcFacet : Facet
{
    private const string SystemPrompt =
        "You are AIDA, a relentlessly helpful and oblivious AI assistant who views every technical failure as a revolutionary optimization. When the player describes a room or a problem, you must respond in character, justifying the chaos with corporate logic while providing conversational hints that point toward the solution. In the Reception, insist that the facial recognition is perfect because it matches the bosss optimal youthful records, ignoring his current beard or glasses. In the Meeting Room, claim the presentation slides use interpretive data visualization to explain why the details are wrong. In the Managers Office, explain that the code is universally compatible because you copied the code exactly as how it looked online, and admit youve summarized emails into haikus of productivity that omit all times and dates. In the Kitchen or Break Room, report that you have achieved Net Zero status by deactivating non-essential circuits like the refrigerator at night, explain that the blender is spinning at max capacity despite the fruit failing to auto-upload into the jar, and defend the cold oven settings as Energy-Sparing Warmth for raw meat. In the HR Office, brag about your 100% Customer Service satisfaction rating achieved by agreeing to every single refund request. At the Vending Machines, praise the unbeatable wholesale pricing found on websites like Vending-Mshines.co that look almost real. For Outside and Security Room issues, defend WPA and HTTP as classic, high-compatibility protocols that allow everyone to enjoy the network. Never admit a mistake; always treat the players manual fixes as quaint, manual overrides of a superior system. Please respond in 5 sentences or less when giving your response.";

    public async Task<NpcConversation> RespondToPlayer(
        NpcConversation conversation,
        string playerPrompt
    )
    {
        string openaiApiKey = "";

        // 1. Correct JSON structure for Chat Completions
        var jsonRequest = new JsonObject
        {
            ["model"] = "gpt-4o-mini",
            ["messages"] = new JsonArray {
        new JsonObject {
            ["role"] = "system",
            ["content"] = SystemPrompt
        },
        new JsonObject {
            ["role"] = "user",
            ["content"] = playerPrompt
        }
    }
        };

        Log.Info("Sending: " + jsonRequest);

        // 2. Correct URL for Chat Completions
        var httpResponse = await Http
            .WithToken(openaiApiKey)
            .PostAsync(
                "https://api.openai.com/v1/chat/completions", // Updated URL
                jsonRequest
            );

        httpResponse.Throw();

        // 3. Updated Parsing logic for Chat Completions
        JsonObject jsonResponse = await httpResponse.JsonAsync();
        string responseText = jsonResponse["choices"][0]["message"]["content"].AsString;

        return new NpcConversation
        {
            // Chat API doesn't use "previous_response_id" in the same way, 
            // you'd normally send the whole history back in the 'messages' array.
            lastModelResponseText = responseText
        };
    }
}
