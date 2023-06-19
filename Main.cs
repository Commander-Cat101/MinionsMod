using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using TemplateMod.MinionMonkey.Displays.Projectiles;
using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTraveler.Displays.Projectiles;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Simulation.Towers;

[assembly: MelonModInfo(typeof(TemplateMod.Main), "Minions", "2.0.0", "Commander__Cat")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TemplateMod
{
   

namespace MinionMonkey.Displays.Projectiles
{
    public class BananaDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class HeatedBananaDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }
    }
    public class PlasmaBananaDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, Name);
        }

    }
}

namespace MinionMonkey
{
    /// <summary>
    /// The main class that adds the core tower to the game
    /// </summary>
    public class MinionMonkey : ModTower
    {
            // public override string Portrait => "Don't need to override this, using the default of CardMonkey-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of CardMonkey-Icon.png";

       public override TowerSet TowerSet => TowerSet.Magic;
       public override string BaseTower => TowerType.NinjaMonkey;
        public override int Cost => 560;

        public override int TopPathUpgrades => 0;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 0;
        public override string Description => "MINION";

        public override float PixelsPerUnit => 16f;
        public override bool Use2DModel => true;
        public override string DisplayName => "Minion";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range -= 0;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range -= 0;


            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<BananaDisplay>();
            projectile.pierce = 2;
            projectile.GetDamageModel().damage = 2;
            projectile.scale = 1;
        }
        public override string Get2DTexture(int[] tiers)
        {
            if (tiers[1] == 5)
            {
                return "MinionMonkeyX5XDisplay";
            }
            return "MinionMonkeyBaseDisplay";
        }
    }
}
    namespace MinionMonkey.Upgrades.TopPath
    {
        public class GrippingGloves : ModUpgrade<MinionMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 1;
            public override int Cost => 280;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Gains gripping gloves to allow faster firing";

            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .50f;

                }
            }
        }
        public class TwoHanded : ModUpgrade<MinionMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 2;
            public override int Cost => 730;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Double the banana, Double the destruction";

            public override void ApplyUpgrade(TowerModel tower)
            {
                tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 2, 0, 10, null, false, false);
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.pierce += 1;
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .45f;

                }
            }
        }
        public class CarvedBananas : ModUpgrade<MinionMonkey>
        {
            // public override string Portrait => "Don't need to override this, using the default of Pair-Portrait.png";
            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 3;
            public override int Cost => 2330;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Carved bananas fly in a way that they now seek";

            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var towerModel in tower.GetWeapons())
                {
                    foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                    {
                        projectile.GetDamageModel().damage += 8;
                        projectile.AddBehavior(new TrackTargetModel("Testname", 9999999, true, false, 144, false, 300, false, true));
                    }
                    foreach (var attackModel in tower.GetWeapons())
                    {
                        attackModel.Rate = .30f;

                    }
                }
            }
        }
        public class HeatedBananas : ModUpgrade<MinionMonkey>
        {

            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 4;
            public override int Cost => 6760;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "Heated bananas deal much more damage, and damage leads";

            public override void ApplyUpgrade(TowerModel tower)
            {
                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.ApplyDisplay<HeatedBananaDisplay>();
                    projectile.GetDamageModel().damage += 10;
                    tower.GetAttackModel().range += 10;
                    tower.range += 10;
                    projectile.pierce += 3;
                }
                foreach (var towerModel in tower.GetWeapons())
                {

                }
                foreach (var weaponModel in tower.GetWeapons())
                {
                    weaponModel.projectile.GetDamageModel().immuneBloonProperties = 0;
                }
            }
        }
        public class BadDave : ModUpgrade<MinionMonkey>
        {

            // public override string Icon => "Don't need to override this, using the default of Pair-Icon.png";

            public override int Path => MIDDLE;
            public override int Tier => 5;
            public override int Cost => 98970;

            // public override string DisplayName => "Don't need to override this, the default turns it into 'Pair'"

            public override string Description => "When you accidentally eat plasma bananas";

            public override void ApplyUpgrade(TowerModel tower)
            {

                foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
                {
                    projectile.ApplyDisplay<PlasmaBananaDisplay>();
                    tower.GetAttackModel().range += 30;
                    tower.range += 30;
                    projectile.GetDamageModel().damage += 50;
                    projectile.pierce += 4;
                }
                foreach (var towerModel in tower.GetWeapons())
                {

                }
                foreach (var weaponModel in tower.GetWeapons())
                {
                    tower.GetWeapon().emission = new ArcEmissionModel("ArcEmissionModel_", 4, 0, 30, null, false, false);
                }
                foreach (var attackModel in tower.GetWeapons())
                {
                    attackModel.Rate = .3f;

                }
            }

        }
    }
}
