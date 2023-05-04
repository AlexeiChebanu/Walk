using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Serilog;
using WebApi.Data;
using WebApi.Mappings;
using WebApi.Models.Domain;
using WebApi.Models.DTO;
using WebApi.Repository;
using WebApi.Services;
using WebApi.Services.WalkServices;
using Xunit.Abstractions;

namespace TestWebApiXUnit
{
    public class WalksTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly IWalkRepository walkRepository;
        private readonly IWalksService walksService;
        private readonly Mock<IWalkRepository> mock;
        private readonly IFixture _fixture;

        public WalksTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            mock = new Mock<IWalkRepository>();
            this.testOutputHelper = testOutputHelper;

            walkRepository = mock.Object;

            var diagnosticContextMock = new Mock<IDiagnosticContext>();
            var loggerMock = new Mock<ILogger<WalksServices>>();

            walksService = new WalksServices(walkRepository);
        }

        #region AddWalks
        [Fact]
        public async Task AddWalkName_NullWalks_Excp()
        {
            var walksRequest = _fixture.Build<AddWalkRequestDTO>().With(t => t.Name, null as string).Create();

            Walk walk = walksRequest.ToWalk();

            mock.Setup(t => t.CreateAsync(It.IsAny<Walk>())).ReturnsAsync(walk);

            Func<Task> action = async () =>
            {
                await walksService.CreateAsync(walksRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddWalks_NullExcp()
        {
            AddWalkRequestDTO? addWalkRequestDTO = null;

            Func<Task> action = async () =>
            {
                await walksService.CreateAsync(addWalkRequestDTO);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }


        #endregion

        #region UpdateTestWalks

        [Fact]
        public async Task UpdateAsyncWalks_NullPerson()
        {
            UpdateWalkRequestDTO? updateWalkRequestDTO = null;

            Func<Task> action = async () =>
            {
                await walksService.UpdateAsync(updateWalkRequestDTO);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsyncWalks_PersonNameIsNull()
        {
            Walk resposneWalk = _fixture.Build<Walk>().With(t=>t.Name, null as string).With(t=>t.Description, "descritpion hello").Create();

            UpdateWalkRequestDTO request = resposneWalk.ToWalk();

            var action = async () =>
            {
                await walksService.UpdateAsync(request);
            };

            await action.Should().ThrowAsync<ArgumentException>();

        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteWalksById_isValid()
        {
            Walk walk = _fixture.Build<Walk>().Create();

            mock.Setup(temp => temp.DeleteAsync(It.IsAny<Guid>()));

            mock.Setup(temp => temp.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(walk);

            bool isDeleted = await walksService.DeleteAsync(walk.Id);

            isDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task DeletePerson_isEmpty()
        {
            bool isDeleted = await walksService.DeleteAsync(Guid.NewGuid());

            isDeleted.Should().BeFalse();
        }
        #endregion

        #region GetByIdAsyncTest

        [Fact]
        public async Task GetById_ToBeNull()
        {
            Guid? id = null;

            Walk walk = await walksService.GetByIdAsync(id);

            walk.Should().BeNull();
        }


        [Fact]
        public async Task GetWalkById_WithCorrectWalkId()
        {
            Walk walk = _fixture.Build<Walk>().With(t => t.Name, "HelloWorld").Create();

            mock.Setup(t => t.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(walk);

            Walk? responseWalk = await walksService.GetByIdAsync(walk.Id);

            responseWalk.Should().Be(walk);

        }
        #endregion

        #region GetAllWalks

        [Fact]
        public async Task GetAllWalks_IsEmpty()
        {
            List<Walk> walks = new List<Walk>();
            mock.Setup(t => t.GetAllAsync(null, null, null, true, 1, 1000)).ReturnsAsync(walks);

            List<Walk> responseWalk = await walksService.GetAllAsync(null, null, null, true, 1, 1000);

            responseWalk.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllWalks_IsSucsesfull()
        {
            List<Walk> walkExpectation = new List<Walk>()
            {
                _fixture.Build<Walk>().With(t=>t.Name, "HelloW").Create(),
                _fixture.Build<Walk>().With(t => t.Name, "HelloW2").Create(),
                _fixture.Build<Walk>().With(t => t.Name, "HelloW3").Create()
            };

            foreach (Walk walk in walkExpectation)
            {
                testOutputHelper.WriteLine(walk.ToString());
            }

            mock.Setup(t => t.GetAllAsync(null, null, null,true,1,1000)).ReturnsAsync(walkExpectation);

            List<Walk> walks_list_from_get = await walksService.GetAllAsync(null, null, null, true, 1, 1000);

            foreach (Walk item in walks_list_from_get)
            {
                testOutputHelper.WriteLine(item.ToString());
            }

            walks_list_from_get.Should().BeEquivalentTo(walkExpectation);
        }

        #endregion

    }
}