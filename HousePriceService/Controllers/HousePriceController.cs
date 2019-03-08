using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HousePriceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousePriceController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var housePriceSample1 = new HouseData() { Area = "76", BedRooms = 3, BedRoomsBsmt = 0, FullBath = 2, HalfBath = 0, Rooms = 7, ApproxSquFeet = 1300, GarageType = "Attached", GarageSpaces = 2 };
            var price = HousePricePrediction.PredictSinglePrice(housePriceSample1, @"MLNETModels\housePriceModel.zip");

            return new string[] { price };
        }
    }
}