//Used to staticly define information regarding a whole trove of Source Engine Games

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammerPP_Manager
{
    internal class SourceGameInfomation
    {
        internal enum SourceGameID
        {
            CounterStrikeSource,
            HalfLife2,
            VampireMasqueradeBloodlines,
            HalfLife2Deathmatch,
            HalfLifeSource,
            DayOfDefeatSource,
            HalfLife2LostCoast,
            HalfLifeDeathmatchSource,
            HalfLife2EpisodeOne,
            GarrysMod,
            SiNEpisodes,
            DarkMessiah,
            TheShip,
            KumaWar,
            HalfLife2Episode2,
            TeamFortress2,
            Portal,
            Dystopia,
            InsurfencyMIC,
            Left4Dead,
            Left4Dead2,
            ZenoClash,
            NeoTokyo,
            BloodyGoodTime,
            Vindictus,
            EYE,
            AlienSwarm,
            Portal2,
            NoMoreRoomInHell,
            NuclearDawn,
            Postal3,
            DinoDDay,
            DearEsther,
            CounterStrikeGlobalOffensive,
            Hybrid,
            TacticalIntervention,
            TheStanleyParable,
            CounterStrikeOnline2,
            DOTA2,
            BladeSymphony,
            Consortium,
            Contagion,
            Insurgency,
            ApertureTag,
            FistfulOfFrags,
            PortalStoriesMel,
            TheBeginnersGuide,
            Infra,
            DayOfInfamy,
            HuntDownTheFreeman,
            BlackMesa
        }

        internal enum SourceGameType
        {
            Individual, //Uses seperate Steam Folder
            SubFolder   //A part of the Half-Life 2 Steam Folder
        }

        internal struct SourceGame
        {
            internal string GameName { get; set; }
            internal SourceGameID GameID { get; set; }
            internal SourceGameType GameType { get; set; }
            internal string GameDevelopers { get; set; }
            internal string GameSteamID { get; set; }
            internal string GameFolder { get; set; }
            internal SourceGame(string GameName, SourceGameID GameID, SourceGameType GameType, string GameDevelopers, string GameSteamID, string GameFolder)
            {
                this.GameName = GameName;
                this.GameID = GameID;
                this.GameType = GameType;
                this.GameDevelopers = GameDevelopers;
                this.GameSteamID = GameSteamID;
                this.GameFolder = GameFolder;
            }
        }

        static SourceGame CounterStrikeSource = 
            new SourceGame("Counter-Strike: Source", SourceGameID.CounterStrikeSource, SourceGameType.Individual, "Valve Software", "240", "Counter-Strike Source");

        static SourceGame GarrysMod =
           new SourceGame("Garry's Mod", SourceGameID.GarrysMod, SourceGameType.Individual, "Facepunch Studios", "4000", "GarrysMod");
    }
}
