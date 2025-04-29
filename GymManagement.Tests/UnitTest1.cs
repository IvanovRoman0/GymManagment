using Xunit;
using Moq;
using GymManagement.Services.Implementations;
using GymManagement.Infrastructure.Repositories;
using GymManagement.Core.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Results;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using GymManagement.Services.Mapping;
using GymManagment.Infrastructure.Repositories;

namespace GymManagement.Tests.Services
{
    public class AllServicesIntegrationTests
    {
        private readonly IMapper _mapper;

        public AllServicesIntegrationTests()
        {
            _mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<TrainerProfile>();
                cfg.AddProfile<MembershipProfile>();
                cfg.AddProfile<ClassProfile>();
                cfg.AddProfile<GymProfile>();
                cfg.AddProfile<EquipmentProfile>();
                cfg.AddProfile<PaymentProfile>();
                cfg.AddProfile<ReviewProfile>();
            }).CreateMapper();
        }

        #region Client Service Tests
        [Fact]
        public async Task Client_Create_ValidData_Success()
        {
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(false);

            var service = new ClientService(mockRepo.Object, _mapper);
            var result = await service.CreateClientAsync(
                new ClientDto { FirstName = "John", Email = "john@test.com" },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Client_GetAll_ReturnsData()
        {
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new List<Client> { new Client("Test", "User", "123", "test@test.com") });

            var service = new ClientService(mockRepo.Object, _mapper);
            var result = await service.GetAllClientAsync(CancellationToken.None);

            Assert.NotEmpty(result.Data);
        }
        #endregion

        #region Membership Service Tests
        [Fact]
        public async Task Membership_Create_NewType_Success()
        {
            var mockRepo = new Mock<IMembershipRepository>();
            mockRepo.Setup(x => x.ExistsByTypeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(false);

            var service = new MembershipService(mockRepo.Object, _mapper);
            var result = await service.CreateMembershipAsync(
                new MembershipDto { MembershipType = "Premium", Price = 100 },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region Trainer Service Tests
        [Fact]
        public async Task UpdateTrainer_ValidData_ReturnsSuccess()
        {
            var mockRepo = new Mock<ITrainerRepository>();
            var existingTrainer = new Trainer(
                "OldFirstName",
                "OldLastName",
                "123456789",
                "old@example.com",
                null
            )
            { id = 1 };

            mockRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(true);

            mockRepo.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(existingTrainer);

            mockRepo.Setup(x => x.EmailExistsAsync("new@example.com", It.IsAny<CancellationToken>()))
                   .ReturnsAsync(false);

            mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()))
                   .Returns(Task.CompletedTask);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<TrainerProfile>());
            var mapper = mapperConfig.CreateMapper();
            var service = new TrainerService(mockRepo.Object, mapper);

            var updateDto = new TrainerDto
            {
                Id = 1,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                PhoneNumber = "987654321",
                Email = "new@example.com",
                SpecializationId = 1
            };

            var result = await service.UpdateTrainerAsync(1, updateDto, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal("NewFirstName", result.Data.FirstName);
            Assert.Equal("NewLastName", result.Data.LastName);
            Assert.Equal("new@example.com", result.Data.Email);
            Assert.Equal("987654321", result.Data.PhoneNumber);
            Assert.Equal(1, result.Data.SpecializationId);
        }
        #endregion

        #region Class Service Tests
        [Fact]
        public async Task Class_Create_WithTrainer_Success()
        {
            var mockClassRepo = new Mock<IClassRepository>();
            var mockGymRepo = new Mock<IGymRepository>();
            var mockTrainerRepo = new Mock<ITrainerRepository>();

            mockGymRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);
            mockTrainerRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            var service = new ClassService(mockClassRepo.Object, mockTrainerRepo.Object, mockGymRepo.Object, _mapper);
            var result = await service.CreateClassAsync(
                new ClassDto { GymId = 1, ClassName = "Yoga", TrainerId = 1 },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region Payment Service Tests
        [Fact]
        public async Task Payment_Create_ValidData_Success()
        {
            var mockPaymentRepo = new Mock<IPaymentRepository>();
            var mockClientRepo = new Mock<IClientRepository>();

            mockClientRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            var service = new PaymentService(mockPaymentRepo.Object, mockClientRepo.Object, _mapper);
            var result = await service.CreatePaymentAsync(
                new PaymentDto { ClientId = 1, Amount = 100, PaymentType = "Cash" },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region Equipment Service Tests
        [Fact]
        public async Task Equipment_Delete_ExistingItem_Success()
        {
            var mockRepo = new Mock<IEquipmentRepository>();
            var mockGymRepo = new Mock<IGymRepository>();

            mockRepo.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new Equipment("Treadmill", 1));

            var service = new EquipmentService(mockRepo.Object, mockGymRepo.Object, _mapper);
            var result = await service.DeleteEquipmentAsync(1, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion

        #region Gym Service Tests
        [Fact]
        public async Task Gym_Update_ValidData_Success()
        {
            var mockRepo = new Mock<IGymRepository>();
            mockRepo.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new Gym("Old Gym", "Location"));
            mockRepo.Setup(x => x.NameExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(false);

            var service = new GymService(mockRepo.Object, _mapper);
            var result = await service.UpdateGymAsync(1,
                new GymDto { Id = 1, GymName = "New Gym" },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion

        // ƒополнительные тесты дл€ других сервисов...
        #region Review Service Tests
        [Fact]
        public async Task Review_Create_ForGym_Success()
        {
            var mockRepo = new Mock<IReviewRepository>();
            var mockClientRepo = new Mock<IClientRepository>();
            var mockGymRepo = new Mock<IGymRepository>();

            mockClientRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);
            mockGymRepo.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);

            var service = new ReviewService(mockRepo.Object, _mapper, mockClientRepo.Object, null, mockGymRepo.Object);
            var result = await service.CreateReviewAsync(
                new ReviewDto { ClientId = 1, GymId = 1, Raiting = 5 },
                CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
        #endregion
    }
}