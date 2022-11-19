using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;
using AdvertBoard.Domain;
using AdvertBoard.Infrastructure.Repository;
using MockQueryable.Moq;
using Moq;
using Shouldly;

namespace AdvertBoardTests
{
    public class ShoppingCartRepositoryTests
    {
        /*[Fact]
        public async Task TryGetAll_ShoppingCart_Success()
        {
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            // arrange
            var entities = new List<ShoppingCart>(new[]
            {
                new ShoppingCart { Id = id1, Amount = 1, Quantity = 1, Product = new Product { Name = "milk" }, Price = 99},
                new ShoppingCart { Id = id2, Amount = 2, Quantity = 2, Product = new Product { Name = "bread" }, Price = 30}
            });

            var entitiesMock = entities.BuildMock();

            var repositoryMock = new Mock<IRepository<ShoppingCart>>();
            repositoryMock.Setup(x => x.GetAll()).Returns(entitiesMock);

            // act
            CancellationToken token = new CancellationToken(false);
            ShoppingCartRepository repository = new ShoppingCartRepository(repositoryMock.Object);
            var result = await repository.GetAllAsync(token);

            // assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);

            var resultArray = result.ToArray();

            for (var i = 0; i < resultArray.Length; i++)
            {
                var actual = resultArray[i];
                var expect = entities[i];

                actual.Id.ShouldBe(expect.Id);
                actual.ProductName.ShouldBe(expect.Product.Name);
                actual.Quantity.ShouldBe(expect.Quantity);
                actual.Amount.ShouldBe(expect.Amount);
                actual.Price.ShouldBe(expect.Price);

            }
*//*
            foreach (var cartDto in result)
            {
                cartDto.Id.ShouldNotBe(Guid.Empty);
                cartDto.ProductName.ShouldNotBeNullOrEmpty();
                cartDto.Quantity.ShouldNotBe(0);
                cartDto.Amount.ShouldNotBe(0);
                cartDto.Price.ShouldNotBe(0);
            }*//*
        }


        [Fact]
        public async Task GetAll_ShoppingCart_Success()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<AdvertBoard.Domain.ShoppingCart>>();
            *//*repositoryMock.Setup(x => x.GetAll);*//*
            CancellationToken token = new CancellationToken(false);

            ShoppingCartRepository repository = new ShoppingCartRepository(repositoryMock.Object);

            // act
            var result = await repository.GetAllAsync(token);

            // assert
            Assert.NotNull(result); 
        }*/
    }
}