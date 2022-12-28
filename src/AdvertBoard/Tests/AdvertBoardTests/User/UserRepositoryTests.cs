
using AdvertBoard.DataAccess.EntityConfigurations.Advertisement;
using AdvertBoard.DataAccess.EntityConfigurations.Product;
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
    public class UserRepositoryTests
    {
        /// <summary>
        /// Проверка получения всех объявлений.
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Маркер отмены</param>
        /// <returns></returns>
        [Fact]
        public async Task GetUser_Success()
        {
            // arrange
            var newId = Guid.NewGuid();

            var entity = new Domain.User()
            {
                     Id = Guid.NewGuid(), Name = "Пользователь", Password = "qwerty", CreateDate = DateTime.Now, Email = "mail@mail.ru"
            };


            

            var repositoryMock = new Mock<IRepository<Domain.User>>();

            CancellationToken token = new CancellationToken(false);

            repositoryMock.Setup(x => x.GetByIdAsync(newId)).Returns(Task.FromResult(entity));


            // act

            UserRepository repository = new UserRepository(repositoryMock.Object);
            var result = await repository.FindById(newId, token);

            // assert

            repositoryMock.Verify(x => x.GetByIdAsync(newId), Times.Once);

            result.ShouldNotBeNull();
            

            var resultEntity = result;
            var expectEntity = entity;


            resultEntity.Id.ShouldBe(expectEntity.Id);
            resultEntity.Name.ShouldBe(expectEntity.Name);
            resultEntity.Password.ShouldBe(expectEntity.Password);
            resultEntity.CreateDate.ShouldBe(expectEntity.CreateDate);
            resultEntity.Email.ShouldBe(expectEntity.Email);

        }



        [Fact]
        public async Task Create_User_Success()
        {
            // arrange
            var newId = Guid.NewGuid();

            var entity = new Domain.User()
            {
                Id = Guid.NewGuid(),
                Name = "Пользователь",
                Password = "qwerty",
                CreateDate = DateTime.Now,
                Email = "mail@mail.ru"
            };


            Domain.User createdDomain = null!;

            var repositoryMock = new Mock<IRepository<Domain.User>>();

            CancellationToken token = new CancellationToken(false);


            repositoryMock.Setup(x => x.Add(It.IsAny<Domain.User>()))
                .Callback((Domain.User x) =>
                {
                    x.Id = newId;
                    createdDomain = x;
                });




            // act
            UserRepository repository = new UserRepository(repositoryMock.Object);
            var result = repository.Add(entity);

            // assert
            createdDomain.ShouldNotBeNull();
            repositoryMock.Verify(x => x.Add(createdDomain), Times.Once);

            result.ShouldNotBe(Guid.Empty);
            result.ShouldBe(newId);
        }

        [Fact]
        public async Task Edit_User_Success()
        {
            // arrange
            var newId = Guid.NewGuid();

            var entity = new Domain.User()
            {
                Id = newId,
                Name = "Пользователь",
                Password = "qwerty",
                CreateDate = DateTime.Now,
                Email = "mail@mail.ru"
            };

            var editEntity = new Domain.User()
            {
                Id = newId,
                Name = "ПользовательEDIT",
                Password = "qwerty",
                CreateDate = DateTime.Now,
                Email = "mail@mail.ru"
            };

            Domain.User createdDomain = null!;

            var repositoryMock = new Mock<IRepository<Domain.User>>();

            CancellationToken token = new CancellationToken(false);


            repositoryMock.Setup(x => x.Add(It.IsAny<Domain.User>()))
                .Callback((Domain.User x) =>
                {
                    x.Id = newId;
                    createdDomain = x;
                });
            repositoryMock.Setup(x => x.GetByIdAsync(newId)).Returns(Task.FromResult(editEntity));

            // act
            UserRepository repository = new UserRepository(repositoryMock.Object);
            var resultEntity = repository.Add(entity);

            

            repository.Edit(editEntity);
            var result = await repository.FindById(newId, token);

            // assert
            createdDomain.ShouldNotBeNull();
            repositoryMock.Verify(x => x.Add(createdDomain), Times.Once);

            var resEntity = result;
            var expEntity = editEntity;


            resEntity.Id.ShouldBe(resEntity.Id);
            resEntity.Name.ShouldBe(resEntity.Name);
            resEntity.Password.ShouldBe(resEntity.Password);
            resEntity.CreateDate.ShouldBe(resEntity.CreateDate);
            resEntity.Email.ShouldBe(resEntity.Email);
        }

    }
}
