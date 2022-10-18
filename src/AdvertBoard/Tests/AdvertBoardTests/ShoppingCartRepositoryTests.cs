using AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;
using AdvertBoard.Infrastructure.Repository;
using Moq;

namespace AdvertBoardTests
{
    public class ShoppingCartRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShoppingCart_Success()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<AdvertBoard.Domain.ShoppingCart>>();
            /*repositoryMock.Setup(x => x.GetAll);*/
            CancellationToken token = new CancellationToken(false);

            ShoppingCartRepository repository = new ShoppingCartRepository(repositoryMock.Object);

            // act
            var result = await repository.GetAllAsync(token);

            // assert
            Assert.NotNull(result); 
        }
    }
}