using GalacticEmpire.Domain.Models.EmpireModel;
using GalacticEmpire.Domain.Models.EmpireModel.Base;
using GalacticEmpire.Shared.Enums.Material;
using GalacticEmpire.Shared.Extensions.EnumExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Domain.Models.PlanetModel.Base
{
    public class Planet : BaseModel<int>, IBaseEffect<Empire>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string PlanetType { get; set; }
        public TimeSpan CapturingTime { get; set; }

        public PlanetProperty PlanetProperty { get; set; }

        public ICollection<EmpirePlanet> EmpirePlanets { get; set; }
        public ICollection<PlanetPriceMaterial> PlanetPriceMaterials { get; set; }


        public virtual void ApplyEffect(Empire model) 
        {
            model.MaxNumberOfPopulation += PlanetProperty.MaxPopulationCount;
            model.MaxNumberOfUnits += PlanetProperty.MaxUnitCount;
            SetEmpireMaterials(model,true);
        }

        public virtual void RemoveEffect(Empire model) 
        {
            model.MaxNumberOfPopulation -= PlanetProperty.MaxPopulationCount;
            model.MaxNumberOfUnits -= PlanetProperty.MaxUnitCount;
            SetEmpireMaterials(model, false);
        }

        private void SetEmpireMaterials(Empire empire, bool add)
        {
            // Bitcoin
            var bitcoin = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Bitcoin.GetDisplayName());

            // Quartz
            var quartz = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Quartz.GetDisplayName());

            // Food
            var food = empire.EmpireMaterials
                .FirstOrDefault(e => e.Material.Name == MaterialEnum.Food.GetDisplayName());
            
            if (add)
            {
                bitcoin.BaseProduction += PlanetProperty.BaseBitcoin;

                quartz.BaseProduction += PlanetProperty.BaseQuartz;

                food.BaseProduction += PlanetProperty.BaseFood;
            }
            else
            {
                bitcoin.BaseProduction -= PlanetProperty.BaseBitcoin;
                
                quartz.BaseProduction -= PlanetProperty.BaseQuartz;

                food.BaseProduction -= PlanetProperty.BaseFood;
            }
        }
    }
}
