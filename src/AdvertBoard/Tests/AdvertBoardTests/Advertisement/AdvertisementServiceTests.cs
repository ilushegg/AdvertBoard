using AdvertBoard.AppServices.Advertisement.Services;
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
    public class AdvertisementServiceTests
    {
        /// <summary>
        /// Проверка получения всех объявлений.
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Маркёр отмены</param>
        /// <returns></returns>
        [Fact]
        public async Task GetAll_Success()
        {
            // arrange
            var entities = new List<Domain.Advertisement>(new[]
            {
                    new Domain.Advertisement { Id = Guid.NewGuid(), Name = "Квартира", Description = "Квартира", Price = 19500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, isActived = true },
                    new Domain.Advertisement { Id = Guid.NewGuid(), Name = "Квартира2", Description = "Квартира2", Price = 18500, CategoryId = Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), UserId = Guid.Parse("f45fc11c-e4eb-42c4-9dbc-81d42bf0c1ab"), LocationId = Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), DateTimeCreated = DateTime.Now, DateTimePublish = DateTime.Now, DateTimeUpdated = DateTime.Now, isActived = true },

            });


            var entitiesMock = entities.BuildMock();

            var repositoryMock = new Mock<IRepository<Domain.Advertisement>>();

            CancellationToken token = new CancellationToken(false);

            repositoryMock.Setup(x => x.GetAll()).Returns(entitiesMock);


            // act

            AdvertisementRepository repository = new AdvertisementRepository(repositoryMock.Object);
            var result = await repository.GetAll(10, 0, token);

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



      /*  [Fact]
        public async Task Create_Advertisement_Success()
        {
            var newId = Guid.NewGuid();

            Domain.Advertisement createdDomain = null!;

            var serviceMock = new Mock<IAdvertisementService>();

            CancellationToken token = new CancellationToken(false);


            serviceMock.Setup(x => x.AddAsync("Квартира", "Квартира", 19500, Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), new Domain.User(), token))
                .Callback((Domain.Advertisement x) =>
                {
                    x.Id = newId;
                    createdDomain = x;
                });


            // act
            AdvertisementService service = new AdvertisementService(repositoryMock.Object);
            var result = await service.AddAsync("Квартира", "Квартира", 19500, Guid.Parse("f95a34e1-ed2e-4092-a322-66bb313f0ed3"), Guid.Parse("f20901a1-495b-4225-af0c-6de001ba9cca"), new Domain.User(), token);

            // assert
            createdDomain.ShouldNotBeNull();
            serviceMock.Verify(x => x.AddAsync(createdDomain), Times.Once);

            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(newId);
        }*/

    }
}
