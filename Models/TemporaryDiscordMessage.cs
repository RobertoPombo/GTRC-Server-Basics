using Newtonsoft.Json;
using System.Text;

using GTRC_Basics;

namespace GTRC_Server_Basics.Models
{
    public class TemporaryDiscordMessage
    {
        private static readonly string path = GlobalValues.DataDirectory + "temporary discord bot messages.json";
        public static List<TemporaryDiscordMessage> List = [];

        public ulong MessageId { get; set; } = GlobalValues.NoDiscordId;
        public ulong ChannelId { get; set; } = GlobalValues.NoDiscordId;
        public DiscordMessageType Type { get; set; } = DiscordMessageType.Commands;
        public bool DoesOverride(TemporaryDiscordMessage message)
        {
            if ((
            message.Type == DiscordMessageType.Commands ||
            message.Type == DiscordMessageType.BoP ||
            message.Type == DiscordMessageType.Events ||
            message.Type == DiscordMessageType.Cars ||
            message.Type == DiscordMessageType.Organizations ||
            message.Type == DiscordMessageType.Rating
            ) && (
            Type == DiscordMessageType.Commands ||
            Type == DiscordMessageType.Entries ||
            Type == DiscordMessageType.NewEntries ||
            Type == DiscordMessageType.SeasonSettingsViolations ||
            Type == DiscordMessageType.BoP ||
            Type == DiscordMessageType.Events ||
            Type == DiscordMessageType.Cars ||
            Type == DiscordMessageType.Organizations ||
            Type == DiscordMessageType.Rating
            )) { return true; }

            else if ((
            message.Type == DiscordMessageType.Entries
            ) && (
            Type == DiscordMessageType.Entries
            )) { return true; }

            else if ((
            message.Type == DiscordMessageType.SeasonSettingsViolations
            ) && (
            Type == DiscordMessageType.SeasonSettingsViolations
            )) { return true; }

            return false;
        }

        public bool IsOverridable()
        {
            DiscordMessageType[] listTypes = (DiscordMessageType[])Enum.GetValues(typeof(DiscordMessageType));
            foreach (DiscordMessageType type in listTypes )
            {
                TemporaryDiscordMessage message = new() { Type = type };
                if (message.DoesOverride(this)) { return true; };
            }
            return false;
        }

        public static void LoadJson()
        {
            List.Clear();
            if (!File.Exists(path)) { File.WriteAllText(path, JsonConvert.SerializeObject(List, Formatting.Indented), Encoding.Unicode); }
            try { List = JsonConvert.DeserializeObject<List<TemporaryDiscordMessage>>(File.ReadAllText(path, Encoding.Unicode)) ?? []; }
            catch { GlobalValues.CurrentLogText = "Load temporary discord messages failed!"; }
        }

        public static void SaveJson()
        {
            string text = JsonConvert.SerializeObject(List, Formatting.Indented);
            File.WriteAllText(path, text, Encoding.Unicode);
        }
    }
}
