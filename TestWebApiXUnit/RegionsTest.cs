using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Domain;
using WebApi.Repository;
using WebApi.Services.RegionServices;
using WebApi.Services.WalkServices;
using Xunit.Abstractions;

namespace TestWebApiXUnit
{
    public class RegionsTest
    {

        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRegionRepository regionRepository;
        private readonly IRegionServices regionService;
        private readonly Mock<IRegionRepository> mock;
        private readonly IFixture _fixture;

        public RegionsTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            mock = new Mock<IRegionRepository>();
            this.testOutputHelper = testOutputHelper;

            regionRepository = mock.Object;

            var diagnosticContextMock = new Mock<IDiagnosticContext>();
            var loggerMock = new Mock<ILogger<RegionServices>>();

            regionService = new RegionServices(regionRepository);
        }

        #region AddRegion

        [Fact]
        public async Task AddRegion_Null()
        {
            AddRegionRequestDTO addRegionRequest = null;

            Func<Task> action = async () =>
            {
                await regionService.CreateAsync(addRegionRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddRegion_NotNull()
        {
            var regionRequest = _fixture.Build<AddRegionRequestDTO>().With(t => t.Name, null as string).Create();

            Region region = regionRequest.ToRegion();

            mock.Setup(t=>t.CreateAsync(It.IsAny<Region>())).ReturnsAsync(region);

            Func<Task> action = async () =>
            {
                await regionService.CreateAsync(regionRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }
        #endregion

        #region UpdateRegion

        [Fact]
        public async Task Upd_NullRegion()
        {
            UpdRegionRequest updRegion = null;

            Func<Task> action = async () =>
            {
                await regionService.UpdateAsync(updRegion);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Upd_RegionName()
        {
            Region responseRegion = _fixture.Build<Region>().With(p => p.Name, null as string).Create();

            UpdRegionRequest updRegion = responseRegion.ToRegion();

            var action = async () =>
            {
                await regionService.UpdateAsync(updRegion);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }
        #endregion

        #region Delete

        [Fact]
        public async Task Delete_isSuccesfull()
        {
            Region region = _fixture.Build<Region>().Create();

            mock.Setup(t => t.DeleteAsync(It.IsAny<Guid>()));

            mock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(region);

            bool isDeleted = await regionService.DeleteAsync(region.Id);

            isDeleted.Should().BeTrue();        
        }

        #endregion

        #region GetByIdAsyncTest

        [Fact]
        public async Task GetById_ToBeNull()
        {
            Guid? id = null;

            Region region = await regionService.GetByIdAsync(id);

            region.Should().BeNull();
        }


        [Fact]
        public async Task GetRegionById_WithCorrectWalkId()
        {
            Region region = _fixture.Build<Region>().With(t => t.Name, "HelloWorld").Create();

            mock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(region);

            Region? responseRegion = await regionService.GetByIdAsync(region.Id);

            responseRegion.Should().Be(region);

        }
        #endregion

        #region GetAllWalks

        [Fact]
        public async Task GetAllRegions_IsEmpty()
        {
            List<Region> regions = new List<Region>();
            mock.Setup(t => t.GetAllAsync()).ReturnsAsync(regions);

            List<Region> responseRegion = await regionService.GetAllAsync();

            responseRegion.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllRegions_IsSucsesfull()
        {
            List<Region> regionsExpectation = new List<Region>()
            {
                _fixture.Build<Region>().With(t=>t.Name, "HelloW").Create(),
                _fixture.Build<Region>().With(t => t.Name, "HelloW2").Create(),
                _fixture.Build<Region>().With(t => t.Name, "HelloW3").Create()
            };

            foreach (Region region in regionsExpectation)
            {
                testOutputHelper.WriteLine(region.ToString());
            }

            mock.Setup(t => t.GetAllAsync()).ReturnsAsync(regionsExpectation);

            List<Region> region_list_from_get = await regionService.GetAllAsync();

            foreach (Region item in region_list_from_get)
            {
                testOutputHelper.WriteLine(item.ToString());
            }

            region_list_from_get.Should().BeEquivalentTo(regionsExpectation);
        }

        #endregion

    }
}
