using AspNetFundamentalDemo.Controllers;
using AspNetFundamentalDemo.DAOs;
using AspNetFundamentalDemo.DTOs;
using AspNetFundamentalDemo.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace AspNetDemoTest
{
    
    public class ChampionControllerTest
    {
        Mock<IChampionDao> championDaoStub = new Mock<IChampionDao>();

        [Fact]
        public void GetChampion_WithNotExisted_NotFound()
        {
            // Arrange

            championDaoStub.Setup(stub => stub.GetChampion(It.IsAny<Guid>())).Returns(null as Champion);

            var controller = new ChampionController(championDaoStub.Object);

            // Act
            var result = controller.GetChampion(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
            //Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetChampion_WithExisted_Found()
        {
            // Arrange
            var expectResult = CreateRandomChampion();
            championDaoStub.Setup(stub => stub.GetChampion(It.IsAny<Guid>())).Returns(expectResult);

            var controller = new ChampionController(championDaoStub.Object);

            // Act
            var result = controller.GetChampion(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectResult, options => options.ComparingByMembers<Champion>()); // Compare properties

        }

        [Fact]
        public void GetChampions_WithExisted_Found()
        {
            // Arrange
            var expectedItems = new[]
            {
                CreateRandomChampion(), CreateRandomChampion(), CreateRandomChampion()
            };
            championDaoStub.Setup(dao => dao.GetChampions()).Returns(expectedItems);

            var controller = new ChampionController(championDaoStub.Object);

            // Act
            var actual = controller.GetChampions();

            // Assert 
            actual.Should().BeEquivalentTo(expectedItems, options => options.ComparingByMembers<Champion>());
        }

        [Fact]
        public void CreateChampion_NewItem_Success()
        {
            // Arrange
            var existingItem = CreateRandomChampion();
            championDaoStub.Setup(stub => stub.GetChampion(It.IsAny<Guid>())).Returns(existingItem);

            var controller = new ChampionController(championDaoStub.Object);

            var id = existingItem.Id;
            var itemToUpdate = new UpdateChampionDto()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Ultimate = Guid.NewGuid().ToString()
            };

            // Act
            var result = controller.UpdateChampion(itemToUpdate);

            // Assert 
            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public void DeleteChampion_NewItem_Success()
        {
            // Arrange
            var existingItem = CreateRandomChampion();
            championDaoStub.Setup(stub => stub.GetChampion(It.IsAny<Guid>())).Returns(existingItem);

            var controller = new ChampionController(championDaoStub.Object);

            // Act
            var result = controller.DeleteChampion(Guid.NewGuid());

            // Assert 
            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public void UpdateChampion_ExistedItem_Success()
        {
            // Arrange
            var itemToUpdate = new CreateChampionDto
            {
                Name = Guid.NewGuid().ToString(),
                Ultimate = Guid.NewGuid().ToString()
            };

            var controller = new ChampionController(championDaoStub.Object);

            // Act
            var createdItem = controller.CreateChampion(itemToUpdate);

            // Assert 
            var dto = (createdItem.Result as CreatedAtActionResult).Value as ChampionDto;
            itemToUpdate.Should().BeEquivalentTo(
                dto, options => options.ComparingByMembers<ChampionDto>().ExcludingMissingMembers());
        }

        private Champion CreateRandomChampion()
        {
            return new Champion
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Ultimate = Guid.NewGuid().ToString()
            };
        }
    }
}
