using AdvertBoard.AppServices.Advertisement.Services;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.DataAccess.EntityConfigurations.Advertisement;
using AdvertBoard.Infrastructure.Repository;
using AutoFixture.Xunit2;
using MockQueryable.Moq;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Tests.Advertisement
{
    public class AdvertisementRepositoryTests
    {
        /// <summary>
        /// Проверка получения всех объявлений.
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns></returns>
        [Fact]
        public async Task GetAll_Success()
        {
            // arrange
            var entities = new List<Domain.Advertisement>(new[]
            {
                    new Domain.Advertisement { Id = Guid.NewGuid(), Name = "Квартира", Description = "Квартира", Price = 19500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, Status = "public" },
                    new Domain.Advertisement { Id = Guid.NewGuid(), Name = "Квартира2", Description = "Квартира2", Price = 18500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, Status = "public" },

            });


            var entitiesMock = entities.BuildMock();

            var repositoryMock = new Mock<IRepository<Domain.Advertisement>>();

            CancellationToken token = new CancellationToken(false);

            repositoryMock.Setup(x => x.GetAll()).Returns(entitiesMock);


            // act

            AdvertisementRepository repository = new AdvertisementRepository(repositoryMock.Object);
            var result = await repository.GetAllAsync(10, 0, token);

            // assert

            repositoryMock.Verify(x => x.GetAll(), Times.Once);

            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);

            var resultArray = result.ToArray();
            var expectArray = entities.ToArray();

            for (var i = 0; i < resultArray.Length; i++)
            {
                var actual = resultArray[i];
                var expect = expectArray[i];

                actual.Id.ShouldBe(expect.Id);
                actual.Name.ShouldBe(expect.Name);
                actual.Price.ShouldBe(expect.Price);
                actual.CategoryId.ShouldBe(expect.CategoryId);

            }
        }



        [Fact]
        public async Task Create_Advertisement_Success()
        {
            var newId = Guid.NewGuid();

            Domain.Advertisement createdDomain = null!;

            var repositoryMock = new Mock<IRepository<Domain.Advertisement>>();

            CancellationToken token = new CancellationToken(false);
            var entity = new Domain.Advertisement
            { Id = newId, Name = "Квартира", Description = "Квартира", Price = 19500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, Status = "public" };

            repositoryMock.Setup(x => x.Add(It.IsAny<Domain.Advertisement>()))
                .Callback((Domain.Advertisement x) =>
                {
                    x.Id = newId;
                    createdDomain = x;
                });


            // act
            AdvertisementRepository repository = new AdvertisementRepository(repositoryMock.Object);
            var result = repository.Add(entity);

            // assert
            createdDomain.ShouldNotBeNull();
            repositoryMock.Verify(x => x.Add(createdDomain), Times.Once);

            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(newId);
        }

        [Fact]
        public async Task Edit_Advertisement_Success()
        {
            var newId = Guid.NewGuid();

            Domain.Advertisement createdDomain = null!;

            var repositoryMock = new Mock<IRepository<Domain.Advertisement>>();

            CancellationToken token = new CancellationToken(false);
            var entity = new Domain.Advertisement
            { Id = newId, Name = "Квартира", Description = "Квартира", Price = 19500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, Status = "public" };

            repositoryMock.Setup(x => x.Add(It.IsAny<Domain.Advertisement>()))
                .Callback((Domain.Advertisement x) =>
                {
                    x.Id = newId;
                    createdDomain = x;
                });


            // act
            var editEntity = new Domain.Advertisement
            { Id = newId, Name = "Квартира", Description = "КвартираEDIT", Price = 19500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, Status = "public" };

            AdvertisementRepository repository = new AdvertisementRepository(repositoryMock.Object);
            var resultEntity = repository.Add(entity);

            var result = await repository.EditAsync(editEntity ,token);

            // assert
            createdDomain.ShouldNotBeNull();
            repositoryMock.Verify(x => x.Add(createdDomain), Times.Once);

            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(newId);
        }

    }
}
