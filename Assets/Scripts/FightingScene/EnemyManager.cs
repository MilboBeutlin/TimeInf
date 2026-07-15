using UnityEngine;
using System.Collections.Generic;

// Holds and provides the enemy's attack pools (focused/unfocused/devil arts) for each enemy type,
// and the chance of a devil art attack occurring.
public class EnemyManager : UnityEngine.MonoBehaviour
{
    private List<Attacks> fokusedAttackslocal = new List<Attacks> { };
    private List<Attacks> unfokusedAttackslocal = new List<Attacks> { };
    private List<Attacks> devilArts = new List<Attacks> { Attacks.TheEnd, Attacks.DemonSword, Attacks.BlackFlash };

    public List<Attacks> FokusedAttacks => fokusedAttackslocal;
    public List<Attacks> UnfokusedAttacks => unfokusedAttackslocal;
    public List<Attacks> DevilArts => devilArts;

    private static readonly Dictionary<Gegner, (List<Attacks> fokusiert, List<Attacks> unfokusiert)> attackTable = new()
{
    {
        Gegner.StorageGuard,
        (
            new() { Attacks.HorAttack, Attacks.ArThrust, Attacks.BodyThrow, Attacks.Stomp },
            new() { Attacks.MagneticBurst, Attacks.FeintAttack, Attacks.RockThrow, Attacks.EyeBeam }
        )
    },

    {
        Gegner.MonsterPainting,
        (
            new() { Attacks.InfernoStrike, Attacks.FieryHead, Attacks.FlameBody, Attacks.HeadRush, Attacks.SkullTwist, Attacks.RagingPhoenix },
            new() { Attacks.Ignition, Attacks.FurnaceOfSouls, Attacks.CruelSun, Attacks.FireLight, Attacks.FlameCannon, Attacks.MagmaShot }
        )
    },

    {
        Gegner.ShadowEnemy,
        (
            new() { Attacks.VoidEdge, Attacks.DarkTouch, Attacks.UnstoppableBlow, Attacks.TigerClaw, Attacks.PhantomStep, Attacks.Consume, Attacks.UmbralAmbush, Attacks.PhantomSpear },
            new() { Attacks.DarkSiphon, Attacks.ReignOfDarkness, Attacks.ShadeSurge, Attacks.UmbralPrison, Attacks.Nightfall, Attacks.SoulRend }
        )
    },

    {
        Gegner.Insects,
        (
            new() { Attacks.HellishBite, Attacks.QuickStrike, Attacks.NecroticVenom, Attacks.DemonMandibles, Attacks.Lunge, Attacks.Sting },
            new() { Attacks.Glare, Attacks.WebSling, Attacks.VenomCrawl, Attacks.UroborosDNA, Attacks.SoulToxin, Attacks.AcidSpew }
        )
    },

    {
        Gegner.PrisonGuard,
        (
            new() { Attacks.VolcanicSlam, Attacks.MeltingGrasp, Attacks.FlameSkewer, Attacks.MoltenCrusher, Attacks.DevilTrigger, Attacks.BlazeKick, Attacks.BurningStrike },
            new() { Attacks.InfernalSurge, Attacks.FireCircle, Attacks.Vortex, Attacks.SolarFlare, Attacks.Ignite, Attacks.BlazingDomain, Attacks.HellfireBurst, Attacks.Overheat, Attacks.LavaGeyser }
        )
    },

    {
        Gegner.MiniBoss,
        (
            new() { Attacks.IllusionarySword, Attacks.GravityThrust, Attacks.PsychicMaw, Attacks.FalseReality, Attacks.ForceCrush, Attacks.Eclipse, Attacks.TranscendentFlow, Attacks.MindbladeSlash, Attacks.PsionicClaw },
            new() { Attacks.ThoughtLance, Attacks.Psychokinesis, Attacks.Brainshock, Attacks.Willbreaker, Attacks.FracturedConsciousness, Attacks.MindCrush, Attacks.PhantasmaWave, Attacks.DrainBeam, Attacks.TelepathicScream, Attacks.NeuralOverload, Attacks.EmeraldSplash }
        )
    },

    {
        Gegner.Endboss,
        (
            new() { Attacks.GripOfTheAbyss, Attacks.DeathTouch, Attacks.RedPhantom, Attacks.ArcaneStrike, Attacks.VoidExplosion, Attacks.Annihilation, Attacks.NightmareCrack, Attacks.DevilRush, Attacks.Oblivion, Attacks.TheHollowKing, Attacks.SeveredGrace, Attacks.Sinbreaker },
            new() { Attacks.HollowEcho, Attacks.AbyssalGlare, Attacks.DimensionShift, Attacks.FrenzyShadow, Attacks.SoulFire, Attacks.DevilOrbs, Attacks.NightmareEye, Attacks.LifeDrain, Attacks.SoulEruption, Attacks.Cataclysm, Attacks.StarFire, Attacks.DemonShade, Attacks.EndlessNight, Attacks.BloodOath, Attacks.AshesOfCreation }
        )
    }
};

    //Sets enemy attacks based on the current enemy, if parameter is wrong default values are used
    public void SetEnemyAttacks(Gegner gegner)
    {
        if (attackTable.TryGetValue(gegner, out var attacks))
        {
            fokusedAttackslocal = attacks.fokusiert;
            unfokusedAttackslocal = attacks.unfokusiert;
        }
        else
        {
            fokusedAttackslocal = new() { Attacks.Punch };
            unfokusedAttackslocal = new() { Attacks.FireBall };
        }
    }

    public float GetDevilArtChance(Gegner gegner)
    {
        return gegner switch
        {
            Gegner.ShadowEnemy => 0.08f,
            Gegner.MiniBoss => 0.1f,
            Gegner.Endboss => 0.2f,
            Gegner.Insects => 0f,
            _ => 0.04f
        };
    }
    public int GetBlockadeHP(Gegner gegner)
    {
        return gegner switch
        {
            Gegner.PrisonGuard => 1,
            Gegner.ShadowEnemy => 3,
            Gegner.Endboss => 4,
            _ => 0
        };
    }

    public bool CanShuffle(Gegner gegner)
    {
        return gegner switch
        {
            Gegner.MonsterPainting => true,
            Gegner.MiniBoss => true,
            Gegner.Endboss => true,
            _ => false
        };
    }
}
