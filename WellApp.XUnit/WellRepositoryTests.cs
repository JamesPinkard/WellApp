using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.ViewModel;
using WellApp.UI.Services;
using WellApp.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using WellApp.Domain;

namespace WellApp.XUnit
{
    public class WellRepositoryTests : GroundwaterContextTestBase
    {
        [Fact]
        public void GetAttributeValuesShouldReturnSevenCounties()
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.County).Result;

            Assert.Equal(7, result.Count);
        }

        [Theory]
        [InlineData("Glasscock")]
        [InlineData("Travis")]
        [InlineData("Nueces")]
        public void GetAttributeValuesShouldReturnNamedBindableBase(string expectedResult)
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.County).Result;

            Assert.Contains<string>(expectedResult, result.Select(b => b.Name));
        }

        [Fact]
        public void GetAttributeValuessForAquifersShouldReturnThreeAquifers()
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.Aquifer.AquiferName).Result;

            Assert.Equal(3, result.Count);
        }

        [Theory]
        [InlineData("Edwards (Balcones Fault Zone)")]
        [InlineData("Gulf Coast")]        
        public void GetAttributeValuessForAquifersShouldReturnNamedBindableBase(string expectedResult)
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.Aquifer.AquiferName).Result;

            Assert.Contains<string>(expectedResult, result.Select(b => b.Name));
        }

        [Fact]
        public void GetAttributeValuessForGmasShouldReturnFiveGMAs()
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.GMA).Result;

            Assert.Equal(5, result.Count);
        }

        [Theory]
        [InlineData("7")]
        [InlineData("13")]
        [InlineData("16")]
        public void GetAttributeValuessForGmasShouldReturnBindableBase(string expectedResult)
        {
            IAttributeTable<Well> wellRepository = new WellRepository(_context);

            var result = wellRepository.GetAttributeValuesAsync(w => w.GMA).Result;

            Assert.Contains<string>(expectedResult, result.Select(b => b.Name));
        }
    }
}
