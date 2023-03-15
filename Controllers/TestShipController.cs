using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using ShiipingAPI.Controllers;
using ShiipingAPI.Data;
using ShiipingAPI.Models;
using ShiipingAPI.RespnseModels;
using ShiipingAPI.Services;
using ShiipingAPI.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace ShiipingAPI.Tests.Controllers
{
    public class TestShipController
    {
        private readonly Mock<IShipService> shipService;
        public TestShipController()
        {
            shipService = new Mock<IShipService>();
        }

        [Fact]
        public void GetShipList_ShipList()
        {
           // arrange
            var shipList = ShipMockData.GetShips();
            shipService.Setup(x => x.GetShipList())
                .Returns(shipList);
            var shipsController = new ShipsController(shipService.Object);

            //act
            var shipResult = shipsController.GetShip();

            //assert
            Assert.NotNull(shipResult);
            Assert.Equal(ShipMockData.GetShips().Count(), shipResult.Count());
            Assert.Equal(ShipMockData.GetShips().ToString(), shipResult.ToString());
            Assert.True(shipList.Equals(shipResult));

        }

        [Fact]
        public void GetShipByID_Ship()
        {
            //arrange
            var shipList = ShipMockData.GetShips();
            var shipPortResponse = new ShipPortResponse
            {
                Id = shipList[1].Id,
                Name = shipList[1].Name,
                Description = shipList[1].Description,
                Velocity = shipList[1].Velocity,
                Latitude = shipList[1].Latitude,
                Longitude = shipList[1].Longitude,
            };
            shipService.Setup(x => x.GetShipById(2))
                .Returns(shipPortResponse);
            var shipsController = new ShipsController(shipService.Object);

            //act
            var ShipResult = shipsController.GetShip(2);

            //assert
            Assert.NotNull(ShipResult);
            Assert.Equal(shipList[1].Id, ShipResult.Id);
            Assert.True(shipList[1].Id == ShipResult.Id);
        }


        [Fact]
        public void AddShip_Ship()
        {
            //arrange
            var ShipList = ShipMockData.GetShips();
            shipService.Setup(x => x.AddShip(ShipList[2]))
                .Returns(ShipList[2]);
            var shipsController = new ShipsController(shipService.Object);

            //act
            var ShipResult = shipsController.PostShip(ShipList[2]);

            //assert
            Assert.NotNull(ShipResult);
            Assert.Equal(ShipList[2].Id, ShipResult.Id);
            Assert.True(ShipList[2].Id == ShipResult.Id);
        }

    }
}
